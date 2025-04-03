using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using System.Collections.Generic;
using System.Linq;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Query
    {
        public static Dictionary<string, OrtoDatasComparison> OrtoDatasComparisonDictionary(string directory, IEnumerable<string> references)
        {
            if (string.IsNullOrWhiteSpace(directory) || !System.IO.Directory.Exists(directory) || references == null)
            {
                return null;
            }

            HashSet<UniqueReference> uniqueReferences = new HashSet<UniqueReference>();
            foreach (string reference in references)
            {
                UniqueReference uniqueReference = OrtoDatasComparisonFile.GetUniqueReference(reference);
                if (uniqueReference == null)
                {
                    continue;
                }

                uniqueReferences.Add(uniqueReference);
            }

            Dictionary<string, OrtoDatasComparison> result = new Dictionary<string, OrtoDatasComparison>();

            if (uniqueReferences.Count == 0)
            {
                return result;
            }

            string[] paths = System.IO.Directory.GetFiles(directory, string.Format("*.{0}", Constans.FileExtension.OrtoDatasComparisonFile));
            if (paths == null || paths.Length == 0)
            {
                return result;
            }

            foreach (string path in paths)
            {
                using (OrtoDatasComparisonFile ortoDatasComparisonFile = new OrtoDatasComparisonFile(path))
                {
                    List<OrtoDatasComparison> ortoDatasComparisonList = ortoDatasComparisonFile.GetValues(uniqueReferences)?.ToList();
                    if (ortoDatasComparisonList == null || ortoDatasComparisonList.Count == 0)
                    {
                        continue;
                    }

                    for (int i = ortoDatasComparisonList.Count - 1; i >= 0; i--)
                    {
                        if (ortoDatasComparisonList[i] == null)
                        {
                            continue;
                        }

                        UniqueReference uniqueReference = uniqueReferences.ElementAt(i);

                        result[uniqueReference.ToString()] = ortoDatasComparisonList[i];
                        uniqueReferences.Remove(uniqueReference);

                        if (uniqueReferences.Count == 0)
                        {
                            return result;
                        }
                    }

                }
            }

            return result;
        }

        public static Dictionary<GuidReference, OrtoDatasComparison> OrtoDatasComparisonDictionary(string directory, IEnumerable<Building2D> building2Ds)
        {
            if (string.IsNullOrWhiteSpace(directory) || !System.IO.Directory.Exists(directory) || building2Ds == null)
            {
                return null;
            }

            Dictionary<string, GuidReference> dictionary = new Dictionary<string, GuidReference>();
            foreach(Building2D building2D in building2Ds)
            {
                string reference = building2D?.Reference;
                if(reference == null)
                {
                    continue;
                }

                dictionary[reference] = new GuidReference(building2D);
            }

            Dictionary<string, OrtoDatasComparison> ortoDatasComparisonDictionary = OrtoDatasComparisonDictionary(directory, dictionary.Keys);
            if(ortoDatasComparisonDictionary == null)
            {
                return null;
            }

            Dictionary<GuidReference, OrtoDatasComparison> result = new Dictionary<GuidReference, OrtoDatasComparison>();
            foreach(KeyValuePair<string, GuidReference> keyValuePair in dictionary)
            {
                string reference = keyValuePair.Key;

                if(ortoDatasComparisonDictionary.TryGetValue(reference, out OrtoDatasComparison ortoDatasComparison))
                {
                    result[dictionary[reference]] = ortoDatasComparison;
                }
            }

            return result;
        }
    }
}

