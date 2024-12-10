using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using System.Threading.Tasks;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Modify
    {
        public static async Task<UniqueReference> AddValue(this OrtoDatasComparisonFile ortoDatasComparisonFile, Building2D builidng2D, OrtoDatasComparisonOptions ortoDatasComparisonOptions = null)
        {
            if (ortoDatasComparisonFile == null || builidng2D == null)
            {
                return null;
            }

            if(ortoDatasComparisonOptions == null)
            {
                ortoDatasComparisonOptions = new OrtoDatasComparisonOptions();
            }

            OrtoDatasComparison ortoDatasComparison = null;//await Create.OrtoDatas(builidng2D, ortoDatasOptions.Years, ortoDatasOptions.Offset, ortoDatasOptions.Width, ortoDatasOptions.Reduce);
            if(ortoDatasComparison == null)
            {
                return null;
            }

            UniqueReference result = ortoDatasComparisonFile.AddValue(ortoDatasComparison);
            return result;
        }
    }
}
