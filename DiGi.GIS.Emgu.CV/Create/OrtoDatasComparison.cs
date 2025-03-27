using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Planar.Interfaces;
using DiGi.GIS.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using DiGi.GIS.Enums;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Create
    {
        public static OrtoDatasComparison OrtoDatasComparison(this GISModel gISModel, Building2D building2D, string directory, Core.Classes.Range<int> years)
        {
            if (string.IsNullOrWhiteSpace(directory) || building2D == null || years == null || years.Length <= 0)
            {
                return null;
            }

            OrtoDatas ortoDatas = GIS.Query.OrtoDatas(building2D, directory);
            if(ortoDatas == null)
            {
                return null;
            }

            return OrtoDatasComparison(gISModel, building2D, ortoDatas, years);
        }

        public static OrtoDatasComparison OrtoDatasComparison(this GISModel gISModel, Building2D building2D, OrtoDatas ortoDatas, Core.Classes.Range<int> years)
        {
            if (ortoDatas == null)
            {
                return null;
            }

            PolygonalFace2D polygonalFace2D = building2D.PolygonalFace2D;
            if (polygonalFace2D == null)
            {
                return null;
            }

            IPolygonal2D externalEdge = polygonalFace2D.ExternalEdge;
            if (externalEdge == null)
            {
                return null;
            }

            List<IPolygonal2D> polygonal2Ds;

            polygonal2Ds = Geometry.Planar.Query.Offset(externalEdge, 2);
            if (polygonal2Ds == null || polygonal2Ds.Count == 0)
            {
                polygonal2Ds = new List<IPolygonal2D> { externalEdge };
            }

            polygonal2Ds.Sort((x, y) => y.GetArea().CompareTo(x.GetArea()));

            IPolygonal2D externalEdge_Offset = polygonal2Ds[0];

            List<Tuple<OrtoData, Mat[]>> tuples = new List<Tuple<OrtoData, Mat[]>>();
            string[] names = new string[] { "BB", "P", "PO" };
            foreach (OrtoData ortoData in ortoDatas)
            {
                Mat[] mats = new Mat[3];

                Mat mat_BB = ortoData.Mat();
                mats[0] = mat_BB;

                Mat mat_P = DiGi.Emgu.CV.Query.Fill(mat_BB, ortoData.ToOrto(externalEdge), Color.Black, true);
                mats[1] = mat_P;

                Mat mat_PO = DiGi.Emgu.CV.Query.Fill(mat_BB, ortoData.ToOrto(externalEdge_Offset), Color.Black, true);
                mats[2] = mat_PO;

                tuples.Add(new Tuple<OrtoData, Mat[]>(ortoData, mats));
            }

            List<OrtoDataComparison> ortoDataComparisons = new List<OrtoDataComparison>();
            Parallel.For(0, tuples.Count, i =>
            {
                Tuple<OrtoData, Mat[]> tuple_1 = tuples[i];

                List<OrtoImageComparisonGroup> ortoImageComparisonGroups = new List<OrtoImageComparisonGroup>();
                for (int k = 0; k < names.Length; k++)
                {
                    List<OrtoImageComparison> ortoImageComparisons = new List<OrtoImageComparison>();
                    for (int j = 0; j < tuples.Count; j++)
                    {
                        Tuple<OrtoData, Mat[]> tuple_2 = tuples[j];

                        Mat mat_1 = tuple_1.Item2[k];
                        Mat mat_2 = tuple_2.Item2[k];

                        int hammingDistace = DiGi.Emgu.CV.Query.HammingDistance(mat_1, mat_2);
                        double grayHistogramsFactor = DiGi.Emgu.CV.Query.GrayHistogramFactor(mat_1, mat_2);
                        double averageColorSimilarity = DiGi.Emgu.CV.Query.AverageColorSimilarity(mat_1, mat_2);
                        double histogramCorrelation = DiGi.Emgu.CV.Query.HistogramCorrelation(mat_1, mat_2, false);
                        double shapeComparisonFactor = DiGi.Emgu.CV.Query.ShapeComparisonFactor(mat_1, mat_2);
                        double structuralSimilarityIndex_AbsoluteDifference = DiGi.Emgu.CV.Query.StructuralSimilarityIndex_AbsoluteDifference(mat_1, mat_2);
                        double structuralSimilarityIndex_MatchTemplate = DiGi.Emgu.CV.Query.StructuralSimilarityIndex_MatchTemplate(mat_1, mat_2);
                        double colorDistributionShift = DiGi.Emgu.CV.Query.ColorDistributionShift(mat_1, mat_2);
                        double opticalFlowAverageMagnitude = DiGi.Emgu.CV.Query.OpticalFlowAverageMagnitude(mat_1, mat_2);
                        double oRBFeatureMatchingFactor = DiGi.Emgu.CV.Query.ORBFeatureMatchingFactor(mat_1, mat_2);

                        DiGi.Emgu.CV.Query.LaplacianFactors(mat_1, mat_2, out double meanLaplacianFactor, out double standardDeviationLaplacianFactor);

                        OrtoImageComparison ortoImageComparison = new OrtoImageComparison(
                            tuple_2.Item1.DateTime,
                            hammingDistace,
                            colorDistributionShift,
                            grayHistogramsFactor,
                            averageColorSimilarity,
                            histogramCorrelation,
                            shapeComparisonFactor,
                            structuralSimilarityIndex_AbsoluteDifference,
                            structuralSimilarityIndex_MatchTemplate,
                            meanLaplacianFactor,
                            standardDeviationLaplacianFactor,
                            opticalFlowAverageMagnitude,
                            oRBFeatureMatchingFactor);

                        ortoImageComparisons.Add(ortoImageComparison);
                    }

                    OrtoImageComparisonGroup ortoImageComparisonGroup = new OrtoImageComparisonGroup(names[k], ortoImageComparisons);
                    ortoImageComparisonGroups.Add(ortoImageComparisonGroup);
                }

                OrtoDataComparison ortoDataComparison = new OrtoDataComparison(tuple_1.Item1.DateTime, ortoImageComparisonGroups);
                ortoDataComparisons.Add(ortoDataComparison);
            });

            string reference = building2D.Reference;
            BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
            BuildingPhase? buidlingPhase = building2D.BuildingPhase;
            ushort storeys = building2D.Storeys;
            double area = polygonalFace2D.GetArea();
            Point2D location = polygonalFace2D.GetInternalPoint();
            string voivodeshipName = null;
            string countyName = null;
            string municipalityName = null;
            string subdivisionName = null;
            uint? subdivisionCalculatedOccupancy = null;
            double? subdivisionCalculatedOccupancyArea = null;

            if (gISModel != null)
            {
                List<AdministrativeAreal2D> administrativeAreal2Ds = gISModel.AdministrativeAreal2Ds<AdministrativeAreal2D>(building2D);
                if(administrativeAreal2Ds != null)
                {
                    List<AdministrativeDivision> administrativeDivisions = administrativeAreal2Ds.OfType<AdministrativeDivision>().ToList();
                    if (administrativeDivisions != null)
                    {
                        AdministrativeDivision administrativeDivision = null;

                        administrativeDivision = administrativeDivisions.Find(x => x.AdministrativeDivisionType == AdministrativeDivisionType.voivodeship);
                        if (administrativeDivision != null)
                        {
                            voivodeshipName = administrativeDivision.Name;
                        }

                        administrativeDivision = administrativeDivisions.Find(x => x.AdministrativeDivisionType == AdministrativeDivisionType.county);
                        if (administrativeDivision != null)
                        {
                            countyName = administrativeDivision.Name;
                        }

                        administrativeDivision = administrativeDivisions.Find(x => x.AdministrativeDivisionType == AdministrativeDivisionType.municipality);
                        if (administrativeDivision != null)
                        {
                            municipalityName = administrativeDivision.Name;
                        }
                    }

                    AdministrativeSubdivision administrativeSubdivision = administrativeAreal2Ds.OfType<AdministrativeSubdivision>().FirstOrDefault();
                    if(administrativeSubdivision != null)
                    {
                        subdivisionName = administrativeSubdivision.Name;

                        if(gISModel.TryGetRelatedObjects<OccupancyCalculationResult, AdministrativeAreal2DOccupancyCalculationResultRelation>(administrativeSubdivision, out List<OccupancyCalculationResult> occupancyCalculationResults) && occupancyCalculationResults != null)
                        {
                            OccupancyCalculationResult occupancyCalculationResult = occupancyCalculationResults.FirstOrDefault();
                            if (occupancyCalculationResult != null)
                            {
                                subdivisionCalculatedOccupancy = occupancyCalculationResult.Occupancy;
                                subdivisionCalculatedOccupancyArea = occupancyCalculationResult.OccupancyArea;
                            }
                        }
                        
                    }
                }
            }

            OrtoDatasComparison result = new OrtoDatasComparison(
                reference,
                buildingGeneralFunction,
                buidlingPhase,
                storeys,
                area,
                location,
                voivodeshipName,
                countyName,
                municipalityName,
                subdivisionName,
                subdivisionCalculatedOccupancy,
                subdivisionCalculatedOccupancyArea,
                ortoDataComparisons);

            return result;
        }
    }
}

