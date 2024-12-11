using DiGi.GIS.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoDatasComparisonOptions : ExtractOptions
    {
        [JsonInclude, JsonPropertyName("OrtoDatasOptions")]
        public OrtoDatasOptions OrtoDatasOptions { get; set; } = new OrtoDatasOptions();

        public OrtoDatasComparisonOptions()
            : base()
        {
        }

        public OrtoDatasComparisonOptions(OrtoDatasComparisonOptions ortoDatasComparisonOptions)
            : base(ortoDatasComparisonOptions)
        {

        }

        public OrtoDatasComparisonOptions(JsonObject jsonObject)
            : base(jsonObject)
        {

        }
    }
}

