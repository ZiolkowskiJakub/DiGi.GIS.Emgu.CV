using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    /// <summary>
    /// Provides options for comparing orthodata, extending the base directory name specifications.
    /// </summary>
    public class OrtoDatasComparisonOptions : DirectoryNamesOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether existing values should be overridden.
        /// </summary>
        [JsonInclude, JsonPropertyName("OverrideExisting")]
        public bool OverrideExisting { get; set; } = false;

        /// <summary>
        /// Gets or sets the range of years to be considered during the orthodata comparison process.
        /// </summary>
        [JsonInclude, JsonPropertyName("Years")]
        public Range<int> Years { get; set; } = new Range<int>(2008, DateTime.Now.Year);

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasComparisonOptions" /> class.
        /// </summary>
        public OrtoDatasComparisonOptions()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasComparisonOptions" /> class based on an existing <see cref="OrtoDatasComparisonOptions" /> instance.
        /// </summary>
        /// <param name="ortoDatasComparisonOptions">The source options to copy from.</param>
        public OrtoDatasComparisonOptions(OrtoDatasComparisonOptions ortoDatasComparisonOptions)
            : base(ortoDatasComparisonOptions)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasComparisonOptions" /> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the options data.</param>
        public OrtoDatasComparisonOptions(JsonObject jsonObject)
            : base(jsonObject)
        {
        }
    }
}