using DiGi.GIS.Classes;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoDatasComparisonOptions : ExtractOptions
    {
        /// <summary>
        /// OrtoDatasOptions
        /// </summary>
        [JsonInclude, JsonPropertyName("OrtoDatasOptions")]
        public OrtoDatasOptions OrtoDatasOptions { get; set; } = new OrtoDatasOptions();

        /// <summary>
        /// Override existing values
        /// </summary>
        [JsonInclude, JsonPropertyName("OverrideExisting")]
        public bool OverrideExisting { get; set; } = false;

        /// <summary>
        /// Building2Ds count per save
        /// </summary>
        [JsonInclude, JsonPropertyName("Count")]
        public int Count { get; set; } = 10;

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

