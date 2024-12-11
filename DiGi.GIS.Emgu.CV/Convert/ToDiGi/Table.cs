using DiGi.Core.Classes;
using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Convert
    {
        public static Table ToDiGi_Table(this IEnumerable<OrtoDatasComparison> ortoDatasComparisons)
        {
            if(ortoDatasComparisons == null)
            {
                return null;
            }

            Table result = new Table();

            HashSet<int> years = new HashSet<int>();
            foreach(OrtoDatasComparison ortoDatasComparison in ortoDatasComparisons)
            {
                Range<int> range_Years_Temp = ortoDatasComparison?.GetYears();
                if(range_Years_Temp == null)
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
                    dictionary[nameof(ortoDatasComparison.BuildingGeneralFunction)] = ortoDatasComparison.BuildingGeneralFunction;
                    dictionary[nameof(ortoDatasComparison.BuildingPhase)] = ortoDatasComparison.BuildingPhase;
                    dictionary[nameof(ortoDatasComparison.Area)] = ortoDatasComparison.Area;
                    dictionary[nameof(ortoDatasComparison.Storeys)] = ortoDatasComparison.Storeys;
                    dictionary[string.Format("{0} {1}", nameof(ortoDatasComparison.Location), nameof(ortoDatasComparison.Location.X))] = ortoDatasComparison.Location.X;
                    dictionary[string.Format("{0} {1}", nameof(ortoDatasComparison.Location), nameof(ortoDatasComparison.Location.Y))] = ortoDatasComparison.Location.Y;
                    dictionary[nameof(dateTime_1.Year)] = i;

                    OrtoDataComparison ortoDataComparison = ortoDatasComparison.GetOrtoDataComparison(dateTime_1);
                    if(ortoDataComparison == null)
                    {
                        continue;
                    }

                    foreach(OrtoImageComparisonGroup ortoImageComparisonGroup in ortoDataComparison.OrtoImageComparisonGroups)
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
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.ColorHistogramsFactor))] = double.IsNaN(ortoImageComparison.ColorHistogramsFactor) ? 0 : ortoImageComparison.ColorHistogramsFactor;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.GrayHistogramsFactor))] = double.IsNaN(ortoImageComparison.GrayHistogramsFactor) ? 0 : ortoImageComparison.GrayHistogramsFactor;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.HammingDistance))] = ortoImageComparison.HammingDistance;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.HistogramCorrelation))] = double.IsNaN(ortoImageComparison.HistogramCorrelation) ? 0 : ortoImageComparison.HistogramCorrelation;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.ShapeComparisonFactor))] = double.IsNaN(ortoImageComparison.ShapeComparisonFactor) ? 0 : ortoImageComparison.ShapeComparisonFactor;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.StructuralSimilarityIndex_AbsoluteDifference))] = double.IsNaN(ortoImageComparison.StructuralSimilarityIndex_AbsoluteDifference) ? 0 : ortoImageComparison.StructuralSimilarityIndex_AbsoluteDifference;
                            dictionary[string.Format("{0}_{1}", prefix, nameof(ortoImageComparison.StructuralSimilarityIndex_MatchTemplate))] = double.IsNaN(ortoImageComparison.StructuralSimilarityIndex_MatchTemplate) ? 0 : ortoImageComparison.StructuralSimilarityIndex_MatchTemplate;
                        }
                    }

                    result.AddRow(dictionary);
                }
            }

            return result;
        }
    }
}
