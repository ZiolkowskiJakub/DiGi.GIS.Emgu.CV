using DiGi.Geometry.Planar.Classes;
using DiGi.Geometry.Planar.Interfaces;
using DiGi.GIS.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Create
    {
        /// <summary>
        /// Performs a comparison of orthodata associated with a building within a specified directory and year range.
        /// </summary>
        /// <param name="building2D">The <see cref="Building2D"/> instance to analyze.</param>
        /// <param name="directory">The file system directory where the orthodata files are stored.</param>
        /// <param name="years">The <see cref="Core.Classes.Range{T}"/> of years used to filter the orthodata.</param>
        /// <returns>An <see cref="Classes.OrtoDatasComparison"/> object containing the comparison results, or <see langword="null"/> if any input is null, the directory is empty, or no data is found.</returns>
        public static OrtoDatasComparison? OrtoDatasComparison(this Building2D? building2D, string? directory, Core.Classes.Range<int>? years)
        {
            if (string.IsNullOrWhiteSpace(directory) || building2D == null || years == null || years.Length <= 0)
            {
                return null;
            }

            OrtoDatas? ortoDatas = GIS.Query.OrtoDatas(building2D, directory);
            if (ortoDatas == null)
            {
                return null;
            }

            return OrtoDatasComparison(building2D, ortoDatas);
        }

        /// <summary>
        /// Performs a detailed comparison of orthodata images for a specific building using various image processing metrics.
        /// </summary>
        /// <param name="building2D">The <see cref="Building2D"/> instance providing the geometric context for cropping and masking.</param>
        /// <param name="ortoDatas">The collection of <see cref="OrtoDatas"/> to be compared.</param>
        /// <returns>An <see cref="Classes.OrtoDatasComparison"/> object containing the calculated metrics, or <see langword="null"/> if the building or orthodata is null, or if required geometry is missing.</returns>
        public static OrtoDatasComparison? OrtoDatasComparison(this Building2D? building2D, OrtoDatas? ortoDatas)
        {
            if (ortoDatas == null)
            {
                return null;
            }

            PolygonalFace2D? polygonalFace2D = building2D?.PolygonalFace2D;
            if (polygonalFace2D == null)
            {
                return null;
            }

            IPolygonal2D? externalEdge = polygonalFace2D.ExternalEdge;
            if (externalEdge == null)
            {
                return null;
            }

            List<IPolygonal2D>? polygonal2Ds;

            polygonal2Ds = Geometry.Planar.Query.Offset(externalEdge, 2);
            if (polygonal2Ds == null || polygonal2Ds.Count == 0)
            {
                polygonal2Ds = [externalEdge];
            }

            polygonal2Ds.Sort((x, y) => y.GetArea().CompareTo(x.GetArea()));

            IPolygonal2D externalEdge_Offset = polygonal2Ds[0];

            List<Tuple<OrtoData, Mat?[]>> tuples = [];
            string[] names = ["BB", "P", "PO"];

            foreach (OrtoData ortoData in ortoDatas)
            {
                Mat?[] mats = new Mat[3];

                Mat? mat_BB = ortoData.Mat();
                mats[0] = mat_BB;

                Mat? mat_P = DiGi.Emgu.CV.Query.Fill(mat_BB, ortoData.ToOrto(externalEdge), Color.Black, true);
                mats[1] = mat_P;

                Mat? mat_PO = DiGi.Emgu.CV.Query.Fill(mat_BB, ortoData.ToOrto(externalEdge_Offset), Color.Black, true);
                mats[2] = mat_PO;

                tuples.Add(new Tuple<OrtoData, Mat?[]>(ortoData, mats));
            }

            tuples.Reverse();

            List<OrtoDataComparison> ortoDataComparisons = [];
            //Parallel.For(0, tuples.Count, i =>
            for (int i = 0; i < tuples.Count; i++)
            {
                Tuple<OrtoData, Mat?[]> tuple_1 = tuples[i];

                List<OrtoImageComparisonGroup> ortoImageComparisonGroups = [];
                for (int k = 0; k < names.Length; k++)
                {
                    List<OrtoImageComparison> ortoImageComparisons = [];
                    for (int j = i + 1; j < tuples.Count; j++)
                    {
                        Tuple<OrtoData, Mat?[]> tuple_2 = tuples[j];

                        Mat? mat_1 = tuple_1.Item2[k];
                        Mat? mat_2 = tuple_2.Item2[k];

                        int hammingDistace = DiGi.Emgu.CV.Query.HammingDistance(mat_1, mat_2);
                        double grayHistogramFactor = DiGi.Emgu.CV.Query.GrayHistogramFactor(mat_1, mat_2);
                        double averageColorSimilarity = DiGi.Emgu.CV.Query.AverageColorSimilarity(mat_1, mat_2);
                        double histogramCorrelation = DiGi.Emgu.CV.Query.HistogramCorrelation(mat_1, mat_2, false);
                        double shapeComparisonFactor = DiGi.Emgu.CV.Query.ShapeComparisonFactor(mat_1, mat_2);
                        double structuralSimilarityIndex_AbsoluteDifference = DiGi.Emgu.CV.Query.StructuralSimilarityIndex_AbsoluteDifference(mat_1, mat_2);
                        double structuralSimilarityIndex_MatchTemplate = DiGi.Emgu.CV.Query.StructuralSimilarityIndex_MatchTemplate(mat_1, mat_2);
                        double colorDistributionShift = DiGi.Emgu.CV.Query.ColorDistributionShift(mat_1, mat_2);
                        double opticalFlowAverageMagnitude = DiGi.Emgu.CV.Query.OpticalFlowAverageMagnitude(mat_1, mat_2);
                        double oRBFeatureMatchingFactor = DiGi.Emgu.CV.Query.ORBFeatureMatchingFactor(mat_1, mat_2);

                        DiGi.Emgu.CV.Query.LaplacianFactors(mat_1, mat_2, out double meanLaplacianFactor, out double standardDeviationLaplacianFactor);

                        OrtoImageComparison ortoImageComparison = new(
                            tuple_2.Item1.DateTime,
                            hammingDistace,
                            System.Convert.ToSingle(colorDistributionShift),
                            System.Convert.ToSingle(grayHistogramFactor),
                            System.Convert.ToSingle(averageColorSimilarity),
                            System.Convert.ToSingle(histogramCorrelation),
                            System.Convert.ToSingle(shapeComparisonFactor),
                            System.Convert.ToSingle(structuralSimilarityIndex_AbsoluteDifference),
                            System.Convert.ToSingle(structuralSimilarityIndex_MatchTemplate),
                            System.Convert.ToSingle(meanLaplacianFactor),
                            System.Convert.ToSingle(standardDeviationLaplacianFactor),
                            System.Convert.ToSingle(opticalFlowAverageMagnitude),
                            System.Convert.ToSingle(oRBFeatureMatchingFactor));

                        ortoImageComparisons.Add(ortoImageComparison);
                    }

                    OrtoImageComparisonGroup ortoImageComparisonGroup = new(names[k], ortoImageComparisons);
                    ortoImageComparisonGroups.Add(ortoImageComparisonGroup);
                }

                OrtoDataComparison ortoDataComparison = new(tuple_1.Item1.DateTime, ortoImageComparisonGroups);
                ortoDataComparisons.Add(ortoDataComparison);
            }
            //);

            for (int i = 0; i < tuples.Count; i++)
            {
                Tuple<OrtoData, Mat?[]> tuple = tuples[i];

                for (int j = 0; j < tuple.Item2.Length; j++)
                {
                    tuple.Item2[j]?.Dispose();
                }
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            string? reference = building2D?.Reference;

            OrtoDatasComparison result = new(
                reference,
                ortoDataComparisons);

            return result;
        }
    }
}