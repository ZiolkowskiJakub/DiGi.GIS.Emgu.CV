using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using System.Collections.Generic;
using System.Threading.Tasks;
using DiGi.GIS.Emgu.CV.Classes;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Modify
    {
        public static async Task<HashSet<GuidReference>> CalculateOrtoDatasComparisons(this GISModelFile gISModelFile, IEnumerable<Building2D> building2Ds, OrtoDatasComparisonOptions ortoDatasComparisonOptions, bool overrideExisting = false)
        {
            if (gISModelFile == null || building2Ds == null)
            {
                return null;
            }

            GISModel gISModel = gISModelFile.Value;
            if (gISModel == null)
            {
                return null;
            }

            string path_Model = gISModelFile.Path;
            if (string.IsNullOrWhiteSpace(path_Model))
            {
                return null;
            }

            string fileName = System.IO.Path.GetFileNameWithoutExtension(path_Model);

            string directory = System.IO.Path.GetDirectoryName(path_Model);
            if (!System.IO.Directory.Exists(directory))
            {
                return null;
            }

            HashSet<System.Guid> guids = new HashSet<System.Guid>();
            foreach (Building2D building2D in building2Ds)
            {
                if (building2D == null)
                {
                    continue;
                }

                guids.Add(building2D.Guid);
            }

            List<Building2D> building2Ds_All = gISModel.GetObjects<Building2D>(x => guids.Contains(x.Guid));
            if (building2Ds_All == null || building2Ds_All.Count == 0)
            {
                return null;
            }

            string path_OrtoDatasComparison = System.IO.Path.Combine(directory, string.Format("{0}.{1}", fileName, Constans.FileExtension.OrtoDatasComparisonFile));

            HashSet<GuidReference> result = new HashSet<GuidReference>();
            using (OrtoDatasComparisonFile ortoDatasComparisonFile = new OrtoDatasComparisonFile(path_OrtoDatasComparison))
            {
                ortoDatasComparisonFile.Open();

                string path_Relative = Core.IO.Query.RelativePath(path_Model, path_OrtoDatasComparison);

                foreach (Building2D building2D in building2Ds_All)
                {
                    if (!overrideExisting && gISModel.TryGetRelatedObject(building2D, out OrtoDatasComparisonResult ortoDatasComparisonResult) && ortoDatasComparisonResult != null)
                    {
                        continue;
                    }

                    UniqueReference uniqueReference = await ortoDatasComparisonFile.AddValue(building2D, ortoDatasComparisonOptions);
                    if (uniqueReference == null)
                    {
                        continue;
                    }

                    result.Add(new GuidReference(building2D));

                    ortoDatasComparisonResult = new OrtoDatasComparisonResult(new InstanceRelatedExternalReference(path_Relative, uniqueReference));

                    gISModel.Update(building2D, ortoDatasComparisonResult);
                }

                ortoDatasComparisonFile.Save();
            }

            if(result != null && result.Count != 0)
            {
                gISModelFile.Value = gISModel;
                gISModelFile.Save();
            }

            return result;
        }
    }
}

