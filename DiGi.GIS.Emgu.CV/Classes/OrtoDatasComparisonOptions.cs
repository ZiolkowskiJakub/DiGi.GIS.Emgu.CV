using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoDatasComparisonOptions : ExtractOptions
    {
        /// <summary>
        /// Override existing values
        /// </summary>
        [JsonInclude, JsonPropertyName("OverrideExisting")]
        public bool OverrideExisting { get; set; } = false;

        [JsonInclude, JsonPropertyName("Years")]
        public Range<int> Years { get; set; } = new Range<int>(2008, DateTime.Now.Year);

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

