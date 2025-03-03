using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoDatasComparisonFile : Core.IO.File.Classes.StorageFile<OrtoDatasComparison>, IGISObject
    {
        public override UniqueReference GetUniqueReference(OrtoDatasComparison ortoDatasComparison)
        {
            return GetUniqueReference(ortoDatasComparison?.Reference);
        }

        public static UniqueReference GetUniqueReference(string reference)
        {
            if (reference == null)
            {
                return null;
            }

            return new UniqueIdReference(typeof(OrtoDatas), reference);
        }

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