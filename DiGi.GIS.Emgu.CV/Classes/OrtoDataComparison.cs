using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    /// <summary>
    /// Represents a comparison of orthophoto data, containing the date of comparison and associated image comparison groups.
    /// </summary>
    public class OrtoDataComparison : Core.Classes.SerializableObject, Interfaces.IGISObject
    {
        [JsonInclude, JsonPropertyName("DateTime")]
        private readonly DateTime dateTime;

        [JsonIgnore]
        private readonly Dictionary<string, OrtoImageComparisonGroup> dictionary = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDataComparison"/> class.
        /// </summary>
        /// <param name="dateTime">The date and time when the comparison was performed.</param>
        /// <param name="ortoImageComparisonGroups">A collection of orthophoto image comparison groups.</param>
        public OrtoDataComparison(DateTime dateTime, IEnumerable<OrtoImageComparisonGroup> ortoImageComparisonGroups)
        {
            this.dateTime = dateTime;
            OrtoImageComparisonGroups = ortoImageComparisonGroups;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDataComparison"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the comparison data.</param>
        public OrtoDataComparison(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDataComparison"/> class by copying another <see cref="OrtoDataComparison"/> instance.
        /// </summary>
        /// <param name="ortoDataComparison">The source comparison object to copy from.</param>
        public OrtoDataComparison(OrtoDataComparison ortoDataComparison)
            : base(ortoDataComparison)
        {
            if (ortoDataComparison != null)
            {
                dateTime = ortoDataComparison.dateTime;
                OrtoImageComparisonGroups = ortoDataComparison.OrtoImageComparisonGroups;
            }
        }

        /// <summary>
        /// Gets the date and time when the orthophoto data comparison was performed.
        /// </summary>
        [JsonIgnore]
        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
        }

        /// <summary>
        /// Gets or sets the collection of orthophoto image comparison groups associated with this data comparison.
        /// </summary>
        [JsonInclude, JsonPropertyName("OrtoImageComparisonGroups")]
        public IEnumerable<OrtoImageComparisonGroup>? OrtoImageComparisonGroups
        {
            get
            {
                return dictionary?.Values;
            }

            private set
            {
                dictionary.Clear();
                if (value != null)
                {
                    foreach (OrtoImageComparisonGroup ortoImageComparisonGroup in value)
                    {
                        if (ortoImageComparisonGroup?.Name is null)
                        {
                            continue;
                        }

                        dictionary[ortoImageComparisonGroup.Name] = ortoImageComparisonGroup;
                    }
                }
            }
        }
    }
}