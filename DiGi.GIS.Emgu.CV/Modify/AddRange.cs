using DiGi.Core.Classes;
using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using System.Collections.Generic;
using System;
using DiGi.GIS.Classes;
using DiGi.GIS.Interfaces;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Modify
    {
        public static bool AddRange(this Table table, IEnumerable<OrtoDatasComparison> ortoDatasComparisons, IEnumerable<YearBuiltData> yearBuiltDatas = null)
        {
            if (ortoDatasComparisons == null || table == null)
            {
                return false;
            }

            HashSet<int> years = new HashSet<int>();
            foreach (OrtoDatasComparison ortoDatasComparison in ortoDatasComparisons)
            {
                Range<int> range_Years_Temp = ortoDatasComparison?.GetYears();
                if (range_Years_Temp == null)
                {
                    continue;
                }

                years.Add(range_Years_Temp.Min);
                years.Add(range_Years_Temp.Max);
            }

            Range<int> range_Years = new Range<int>(years);

            foreach (OrtoDatasComparison ortoDatasComparison in ortoDatasComparisons)
            {
                for (int i = range_Years.Min; i <= range_Years.Max; i++)
                {
                    DateTime dateTime_1 = new DateTime(i, 1, 1);

                    Dictionary<string, object> dictionary = new Dictionary<string, object>();
                    dictionary[nameof(ortoDatasComparison.Reference)] = ortoDatasComparison.Reference;
                    dictionary[nameof(dateTime_1.Year)] = i;

                    int? yearBuilt = null;
                    if(yearBuiltDatas != null)
                    {
                        foreach(YearBuiltData yearBuiltData in yearBuiltDatas)
                        {
                            if(ortoDatasComparison.Reference != yearBuiltData?.Reference)
                            {
                                continue;
                            }

                            yearBuilt = yearBuiltData.GetUserYearBuilt()?.Year;
                            break;
                        }
                    }

                    dictionary["YearBuilt"] = yearBuilt;

                    OrtoDataComparison ortoDataComparison = ortoDatasComparison.GetOrtoDataComparison(dateTime_1);
                    if (ortoDataComparison == null)
                    {
                        continue;
                    }

                    foreach (OrtoImageComparisonGroup ortoImageComparisonGroup in ortoDataComparison.OrtoImageComparisonGroups)
                    {
                        string name = ortoImageComparisonGroup.Name;

                        for (int j = range_Years.Min; j <= range_Years.Max; j++)
                        {
                            DateTime dateTime_2 = new DateTime(j, 1, 1);

                            OrtoImageComparison ortoImageComparison = ortoImageComparisonGroup.GetOrtoImageComparison(dateTime_2);
                            if (ortoImageComparison == null)
                            {
                                continue;
                            }

                            string prefix = string.Format("{0}_{1}", name, j);

                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.AverageColorSimilarity))] = double.IsNaN(ortoImageComparison.AverageColorSimilarity) ? 0 : ortoImageComparison.AverageColorSimilarity;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.ColorDistributionShift))] = double.IsNaN(ortoImageComparison.ColorDistributionShift) ? 0 : ortoImageComparison.ColorDistributionShift;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.GrayHistogramsFactor))] = double.IsNaN(ortoImageComparison.GrayHistogramsFactor) ? 0 : ortoImageComparison.GrayHistogramsFactor;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.HammingDistance))] = ortoImageComparison.HammingDistance;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.HistogramCorrelation))] = double.IsNaN(ortoImageComparison.HistogramCorrelation) ? 0 : ortoImageComparison.HistogramCorrelation;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.ShapeComparisonFactor))] = double.IsNaN(ortoImageComparison.ShapeComparisonFactor) ? 0 : ortoImageComparison.ShapeComparisonFactor;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.StructuralSimilarityIndex_AbsoluteDifference))] = double.IsNaN(ortoImageComparison.StructuralSimilarityIndex_AbsoluteDifference) ? 0 : ortoImageComparison.StructuralSimilarityIndex_AbsoluteDifference;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.StructuralSimilarityIndex_MatchTemplate))] = double.IsNaN(ortoImageComparison.StructuralSimilarityIndex_MatchTemplate) ? 0 : ortoImageComparison.StructuralSimilarityIndex_MatchTemplate;

                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.MeanLaplacianFactor))] = double.IsNaN(ortoImageComparison.MeanLaplacianFactor) ? 0 : ortoImageComparison.MeanLaplacianFactor;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.StandardDeviationLaplacianFactor))] = double.IsNaN(ortoImageComparison.StandardDeviationLaplacianFactor) ? 0 : ortoImageComparison.StandardDeviationLaplacianFactor;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.OpticalFlowAverageMagnitude))] = double.IsNaN(ortoImageComparison.OpticalFlowAverageMagnitude) ? 0 : ortoImageComparison.OpticalFlowAverageMagnitude;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.ORBFeatureMatchingFactor))] = double.IsNaN(ortoImageComparison.ORBFeatureMatchingFactor) ? 0 : ortoImageComparison.ORBFeatureMatchingFactor;
                        }
                    }

                    table.AddRow(dictionary);
                }
            }

            return true;
        }
    }
}
