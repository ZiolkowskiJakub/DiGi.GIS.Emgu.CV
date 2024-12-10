using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using System.Text.Json.Nodes;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoDatasComparisonResult : Building2DExternalReferenceUniqueResult
    {
        public OrtoDatasComparisonResult(InstanceRelatedExternalReference reference)
            :base(reference)
        {

        }

        public OrtoDatasComparisonResult(JsonObject jsonObject)
            : base(jsonObject)
        {

        }

        public OrtoDatasComparisonResult(OrtoDatasComparisonResult ortoDatasComparisonResult)
            : base(ortoDatasComparisonResult)
        {

        }
    }
}
