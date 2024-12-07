using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoDatasComparisonFile : Core.IO.File.Classes.StorageFile<OrtoDatasComparison>, IGISObject
    {
        public OrtoDatasComparisonFile(OrtoDatasComparisonFile ortoDatasComparisonFile)
            : base(ortoDatasComparisonFile)
        {

        }

        public OrtoDatasComparisonFile(JsonObject jsonObject)
            :base(jsonObject)
        {

        }

        public OrtoDatasComparisonFile(string path)
            : base(path) 
        {
            
        }
    }
}