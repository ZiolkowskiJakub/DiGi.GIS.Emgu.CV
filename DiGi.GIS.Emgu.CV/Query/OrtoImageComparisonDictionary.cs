using DiGi.GIS.Emgu.CV.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Query
    {
        /// <summary>
        /// Retrieves a dictionary of ortho-image comparisons between two specified date times from the provided ortho data comparison set. 
        /// The method attempts to find matches by first using <paramref name="dateTime_1"/> as the primary data source and <paramref name="dateTime_2"/> as the comparison target; 
        /// if no results are found, it reverses the roles of the two dates.
        /// </summary>
        /// <param name="ortoDatasComparison">The collection of ortho data comparisons to query.</param>
        /// <param name="dateTime_1">The first date time used for the comparison.</param>
        /// <param name="dateTime_2">The second date time used for the comparison.</param>
        /// <returns>A dictionary where keys are group names and values are the corresponding <see cref="OrtoImageComparison"/> objects, or <c>null</c> if <paramref name="ortoDatasComparison"/> is null.</returns>
        public static Dictionary<string, OrtoImageComparison>? OrtoImageComparisonDictionary(this OrtoDatasComparison? ortoDatasComparison, DateTime dateTime_1, DateTime dateTime_2)
        {
            if (ortoDatasComparison == null)
            {
                return null;
            }

            Dictionary<string, OrtoImageComparison> result = [];

            OrtoDataComparison? ortoDataComparison;

            ortoDataComparison = ortoDatasComparison.GetOrtoDataComparison(dateTime_1);
            if (ortoDataComparison != null)
            {
                IEnumerable<OrtoImageComparisonGroup>? ortoImageComparisonGroups = ortoDataComparison.OrtoImageComparisonGroups;
                if (ortoImageComparisonGroups != null)
                {
                    foreach (OrtoImageComparisonGroup ortoImageComparisonGroup in ortoImageComparisonGroups)
                    {
                        if (ortoImageComparisonGroup?.Name == null)
                        {
                            continue;
                        }

                        OrtoImageComparison? ortoImageComparison = ortoImageComparisonGroup.GetOrtoImageComparison(dateTime_2);
                        if (ortoImageComparison != null)
                        {
                            result[ortoImageComparisonGroup.Name] = ortoImageComparison;
                        }
                    }
                }
            }

            if (result.Count == 0)
            {
                ortoDataComparison = ortoDatasComparison.GetOrtoDataComparison(dateTime_2);
                if (ortoDataComparison != null)
                {
                    IEnumerable<OrtoImageComparisonGroup>? ortoImageComparisonGroups = ortoDataComparison.OrtoImageComparisonGroups;
                    if (ortoImageComparisonGroups != null)
                    {
                        foreach (OrtoImageComparisonGroup ortoImageComparisonGroup in ortoImageComparisonGroups)
                        {
                            if (ortoImageComparisonGroup?.Name == null)
                            {
                                continue;
                            }

                            OrtoImageComparison? ortoImageComparison = ortoImageComparisonGroup.GetOrtoImageComparison(dateTime_1);
                            if (ortoImageComparison != null)
                            {
                                result[ortoImageComparisonGroup.Name] = ortoImageComparison;
                            }
                        }
                    }
                }
            }

            return result;
        }
    }
}