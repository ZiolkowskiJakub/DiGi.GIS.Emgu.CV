using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using DiGi.GIS.Enums;
using Emgu.CV;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Create
    {
        public static List<OrtoDataComparison> OrtoDataComparisons(this GISModelFile gISModelFile, Building2D building2D, Core.Classes.Range<int> years)
        {
            if (gISModelFile == null || building2D == null || years == null || years.Length <= 0)
            {
                return null;
            }

            OrtoDatas ortoDatas = gISModelFile.OrtoDatas(building2D);
            if(ortoDatas == null)
            {
                return null;
            }


            Dictionary<int, Mat> dictionary = new Dictionary<int, Mat>();
            for (int i = years.Max; i >= years.Min; i--)
            {
                dictionary[i] = ortoDatas.Mat(new System.DateTime(i, 1, 1));
            }

            string reference = building2D.Reference;
            BuildingGeneralFunction? buildingGeneralFunction = building2D.BuildingGeneralFunction;
            BuildingPhase? buidlingPhase = building2D.BuildingPhase;
            ushort storeys = building2D.Storeys;

            double? area = null;
            double? x = null;
            double? y = null;
            
            PolygonalFace2D polygonalFace2D = building2D.PolygonalFace2D;
            if (polygonalFace2D != null)
            {
                area = polygonalFace2D.GetArea();
                Point2D point2D = polygonalFace2D.GetInternalPoint();
                if(point2D != null)
                {
                    x = point2D.X;
                    y = point2D.Y;
                }
            }

            List<OrtoDataComparison> result = new List<OrtoDataComparison>();
            for (int i = years.Max; i >= years.Min; i--)
            {
                Mat mat_1 = dictionary[i]; 
                if(mat_1 == null)
                {
                    continue;
                }

                OrtoDataComparison ortoDataComparison = new OrtoDataComparison();
                ortoDataComparison.SetValue("Reference", reference);
                ortoDataComparison.SetValue("BuildingGeneralFunction", buildingGeneralFunction?.ToString());
                ortoDataComparison.SetValue("BuildingPhase", buidlingPhase?.ToString());
                ortoDataComparison.SetValue("Storeys", storeys);

                ortoDataComparison.SetValue("Area", area);
                ortoDataComparison.SetValue("X", x);
                ortoDataComparison.SetValue("Y", y);

                for (int j = years.Max; j >= years.Min; j--)
                {
                    Mat mat_2 = dictionary[j];
                    if(mat_2 == null)
                    {
                        continue;
                    }

                    int hammingDistace = DiGi.Emgu.CV.Query.HammingDistance(mat_1, mat_2);
                    double colorHistogramsFactor = DiGi.Emgu.CV.Query.ColorHistogramsFactor(mat_1, mat_2);
                    double oRBFeatureMatchingFactor = DiGi.Emgu.CV.Query.ORBFeatureMatchingFactor(mat_1, mat_2);
                    double averageColorSimilarity = DiGi.Emgu.CV.Query.AverageColorSimilarity(mat_1, mat_2);
                    double histogramCorrelation = DiGi.Emgu.CV.Query.HistogramCorrelation(mat_1, mat_2, false);
                    double histogramCorrelation_Accumulated = DiGi.Emgu.CV.Query.HistogramCorrelation(mat_1, mat_2, true);
                    double shapeComparisonFactor = DiGi.Emgu.CV.Query.ShapeComparisonFactor(mat_1, mat_2);
                    double structuralSimilarityIndex = DiGi.Emgu.CV.Query.StructuralSimilarityIndex(mat_1, mat_2);

                    ortoDataComparison.SetValue(string.Format("{0}_{1}_{2}", "HammingDistance", i, j), hammingDistace);
                    ortoDataComparison.SetValue(string.Format("{0}_{1}_{2}", "ColorHistogramsFactor", i, j), colorHistogramsFactor);
                    ortoDataComparison.SetValue(string.Format("{0}_{1}_{2}", "ORBFeatureMatchingFactor", i, j), oRBFeatureMatchingFactor);
                    ortoDataComparison.SetValue(string.Format("{0}_{1}_{2}", "AverageColorSimilarity", i, j), averageColorSimilarity);
                    ortoDataComparison.SetValue(string.Format("{0}_{1}_{2}", "HistogramCorrelation", i, j), histogramCorrelation);
                    ortoDataComparison.SetValue(string.Format("{0}_{1}_{2}", "ShapeComparisonFactor", i, j), shapeComparisonFactor);
                    ortoDataComparison.SetValue(string.Format("{0}_{1}_{2}", "StructuralSimilarityIndex", i, j), structuralSimilarityIndex);
                }

                result.Add(ortoDataComparison);
            }

            return result;
        }
    }
}

