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
        public static IEnumerable<GuidReference> CalculateOrtoDatasComparisons(this GISModel gISModel, IEnumerable<Building2D> building2Ds, string path, OrtoDatasComparisonOptions ortoDatasComparisonOptions)
        {
            if (building2Ds == null || string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            string directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                return null;
            }

            if(ortoDatasComparisonOptions == null)
            {
                ortoDatasComparisonOptions = new OrtoDatasComparisonOptions();
            }

            IEnumerable<Building2D> building2Ds_Temp = building2Ds;
            if (!ortoDatasComparisonOptions.OverrideExisting)
            {
                Dictionary<GuidReference, OrtoDatasComparison> dictionary_Temp = Query.OrtoDatasComparisonDictionary(directory, building2Ds);
                if (dictionary_Temp != null && dictionary_Temp.Count != 0)
                {
                    List<Building2D> building2Ds_Temp_Temp = new List<Building2D>(building2Ds_Temp);
                    foreach (Building2D building2D in building2Ds_Temp)
                    {
                        GuidReference guidReference = building2D == null ? null : new GuidReference(building2D);
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

            Dictionary<GuidReference, OrtoDatas> dictionary_OrtoDatas = GIS.Query.OrtoDatasDictionary(directory, building2Ds_Temp);
            if(dictionary_OrtoDatas == null || dictionary_OrtoDatas.Count == 0)
            {
                return null;
            }

            Dictionary<GuidReference, OrtoDatasComparison> dictionary_OrtoDatasComparison = new Dictionary<GuidReference, OrtoDatasComparison>();
            foreach(GuidReference guidReference in dictionary_OrtoDatas.Keys)
            {
                dictionary_OrtoDatasComparison[guidReference] = null;
            }

            Parallel.For(0, dictionary_OrtoDatasComparison.Count, GIS.Query.DefaultParallelOptions(), i => 
            {
                GuidReference guidReference = dictionary_OrtoDatasComparison.Keys.ElementAt(i);

                OrtoDatas ortoDatas = dictionary_OrtoDatas[guidReference];

                Building2D building2D = building2Ds_Temp.Find(x => new GuidReference(x) == guidReference);

                dictionary_OrtoDatasComparison[guidReference] = Create.OrtoDatasComparison(building2D, ortoDatas, ortoDatasComparisonOptions.Years);
            });

            using (OrtoDatasComparisonFile ortoDatasComparisonFile = new OrtoDatasComparisonFile(path))
            {
                ortoDatasComparisonFile.Open();
                foreach(OrtoDatasComparison ortoDatasComparison in dictionary_OrtoDatasComparison.Values)
                {
                    ortoDatasComparisonFile.AddValue(ortoDatasComparison);
                }
                ortoDatasComparisonFile.Save();
            }

            return dictionary_OrtoDatasComparison.Keys;
        }
    }
}

