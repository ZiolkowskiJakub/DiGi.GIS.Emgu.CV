using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Emgu.CV.Classes;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Modify
    {
        public static UniqueReference AddValue(this OrtoDatasComparisonFile ortoDatasComparisonFile, GISModelFile gISModelFile, Building2D builidng2D, OrtoDatasComparisonOptions ortoDatasComparisonOptions = null)
        {
            if (ortoDatasComparisonFile == null || builidng2D == null)
            {
                return null;
            }

            if(ortoDatasComparisonOptions == null)
            {
                ortoDatasComparisonOptions = new OrtoDatasComparisonOptions();
            }

            OrtoDatasComparison ortoDatasComparison = Create.OrtoDatasComparison(gISModelFile, builidng2D, ortoDatasComparisonOptions.OrtoDatasOptions.Years);
            if(ortoDatasComparison == null)
            {
                return null;
            }

            UniqueReference result = ortoDatasComparisonFile.AddValue(ortoDatasComparison);
            return result;
        }
    }
}
