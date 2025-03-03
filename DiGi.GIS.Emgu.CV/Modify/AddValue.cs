using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Emgu.CV.Classes;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Modify
    {
        public static UniqueReference AddValue(this OrtoDatasComparisonFile ortoDatasComparisonFile, Building2D builidng2D, GISModel gISModel, OrtoDatasComparisonOptions ortoDatasComparisonOptions = null)
        {
            if (ortoDatasComparisonFile == null || builidng2D == null)
            {
                return null;
            }

            string path = ortoDatasComparisonFile.Path;
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            string directory = System.IO.Path.GetDirectoryName(path);
            if(!System.IO.Directory.Exists(directory))
            {
                return null;
            }

            if (ortoDatasComparisonOptions == null)
            {
                ortoDatasComparisonOptions = new OrtoDatasComparisonOptions();
            }

            OrtoDatasComparison ortoDatasComparison = Create.OrtoDatasComparison(gISModel, builidng2D, directory,ortoDatasComparisonOptions.Years);
            if(ortoDatasComparison == null)
            {
                return null;
            }

            UniqueReference result = ortoDatasComparisonFile.AddValue(ortoDatasComparison);
            return result;
        }
    }
}
