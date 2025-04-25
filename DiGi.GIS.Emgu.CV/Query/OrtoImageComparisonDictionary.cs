using DiGi.GIS.Emgu.CV.Classes;
using System;
using System.Collections.Generic;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Query
    {
        public static Dictionary<string, OrtoImageComparison> OrtoImageComparisonDictionary(this OrtoDatasComparison ortoDatasComparison, DateTime dateTime_1, DateTime dateTime_2)
        {
            if (ortoDatasComparison == null)
            {
                return null;
            }

            Dictionary<string, OrtoImageComparison> result = new Dictionary<string, OrtoImageComparison>();

            OrtoDataComparison ortoDataComparison;

            ortoDataComparison = ortoDatasComparison.GetOrtoDataComparison(dateTime_1);
            if(ortoDataComparison != null)
            {
                IEnumerable<OrtoImageComparisonGroup> ortoImageComparisonGroups = ortoDataComparison.OrtoImageComparisonGroups;
                if(ortoImageComparisonGroups != null)
                {
                    foreach(OrtoImageComparisonGroup ortoImageComparisonGroup in ortoImageComparisonGroups)
                    {
                        if(ortoImageComparisonGroup?.Name == null)
                        {
                            continue;
                        }

                        OrtoImageComparison ortoImageComparison = ortoImageComparisonGroup.GetOrtoImageComparison(dateTime_2);
                        if(ortoImageComparison != null)
                        {
                            result[ortoImageComparisonGroup.Name] = ortoImageComparison;
                        }
                    }
                }
            }

            if(result.Count == 0)
            {
                ortoDataComparison = ortoDatasComparison.GetOrtoDataComparison(dateTime_2);
                if (ortoDataComparison != null)
                {
                    IEnumerable<OrtoImageComparisonGroup> ortoImageComparisonGroups = ortoDataComparison.OrtoImageComparisonGroups;
                    if (ortoImageComparisonGroups != null)
                    {
                        foreach (OrtoImageComparisonGroup ortoImageComparisonGroup in ortoImageComparisonGroups)
                        {
                            if (ortoImageComparisonGroup?.Name == null)
                            {
                                continue;
                            }

                            OrtoImageComparison ortoImageComparison = ortoImageComparisonGroup.GetOrtoImageComparison(dateTime_1);
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

