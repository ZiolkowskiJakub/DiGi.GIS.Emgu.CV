using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiGi.GIS.Emgu.CV.Classes;
using System.Linq;
using DiGi.Core;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Modify
    {
        public static IEnumerable<GuidReference>? CalculateOrtoDatasComparisons(this IEnumerable<Building2D>? building2Ds, string? path, OrtoDatasComparisonOptions? ortoDatasComparisonOptions)
        {
            if (building2Ds == null || string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            string? directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                return null;
            }

            ortoDatasComparisonOptions ??= new OrtoDatasComparisonOptions();

            directory = ortoDatasComparisonOptions.Directory(directory);
            if (!System.IO.Directory.Exists(directory))
            {
                System.IO.DirectoryInfo directoryInfo = System.IO.Directory.CreateDirectory(directory);
                if (directoryInfo == null)
                {
                    return null;
                }
            }

            IEnumerable<Building2D> building2Ds_Temp = building2Ds;
            if (!ortoDatasComparisonOptions.OverrideExisting)
            {
                Dictionary<GuidReference, OrtoDatasComparison>? dictionary_Temp = Query.OrtoDatasComparisonDictionary(directory, building2Ds);
                if (dictionary_Temp != null && dictionary_Temp.Count != 0)
                {
                    List<Building2D> building2Ds_Temp_Temp = [.. building2Ds_Temp];
                    foreach (Building2D building2D in building2Ds_Temp)
                    {
                        if(building2D is null)
                        {
                            continue;
                        }

                        GuidReference guidReference = new (building2D);
                        if (dictionary_Temp.ContainsKey(guidReference))
                        {
                            building2Ds_Temp_Temp.Remove(building2D);
                        }
                    }

                    building2Ds_Temp = building2Ds_Temp_Temp;
                }
            }

            if (building2Ds_Temp == null || building2Ds_Temp.Count() == 0)
            {
                return null;
            }

            Dictionary<GuidReference, OrtoDatas>? dictionary_OrtoDatas = GIS.Query.OrtoDatasDictionary(directory, building2Ds_Temp);
            if(dictionary_OrtoDatas == null || dictionary_OrtoDatas.Count == 0)
            {
                return null;
            }

            Dictionary<GuidReference, OrtoDatasComparison?> dictionary_OrtoDatasComparison = [];
            foreach(GuidReference guidReference in dictionary_OrtoDatas.Keys)
            {
                dictionary_OrtoDatasComparison[guidReference] = null;
            }

            Parallel.For(0, dictionary_OrtoDatasComparison.Count, Core.Create.ParallelOptions(), i => 
            {
                GuidReference guidReference = dictionary_OrtoDatasComparison.Keys.ElementAt(i);

                OrtoDatas ortoDatas = dictionary_OrtoDatas[guidReference];

                Building2D? building2D = building2Ds_Temp.Find(x => new GuidReference(x) == guidReference);

                dictionary_OrtoDatasComparison[guidReference] = Create.OrtoDatasComparison(building2D, ortoDatas);
            });

            using (OrtoDatasComparisonFile ortoDatasComparisonFile = new (path))
            {
                ortoDatasComparisonFile.Open();
                foreach(OrtoDatasComparison? ortoDatasComparison in dictionary_OrtoDatasComparison.Values)
                {
                    ortoDatasComparisonFile.AddValue(ortoDatasComparison);
                }
                ortoDatasComparisonFile.Save();
            }

            return dictionary_OrtoDatasComparison.Keys;
        }
    }
}

