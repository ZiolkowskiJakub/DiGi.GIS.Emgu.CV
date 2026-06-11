using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    /// <summary>
    /// Represents a group of orthophoto image comparisons, typically organized by date and time.
    /// </summary>
    public class OrtoImageComparisonGroup : Core.Classes.SerializableObject, Interfaces.IGISObject
    {
        [JsonInclude, JsonPropertyName("Name")]
        private readonly string? name;

        [JsonIgnore]
        private readonly SortedDictionary<DateTime, OrtoImageComparison> sortedDictionary = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoImageComparisonGroup"/> class.
        /// </summary>
        /// <param name="name">The descriptive name of the comparison group.</param>
        /// <param name="ortoImageComparisons">A collection of orthophoto image comparisons to be added to the group.</param>
        public OrtoImageComparisonGroup(string? name, IEnumerable<OrtoImageComparison>? ortoImageComparisons)
        {
            this.name = name;
            OrtoImageComparisons = ortoImageComparisons ?? [];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoImageComparisonGroup"/> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The <see cref="JsonObject"/> containing the serialized data for the group.</param>
        public OrtoImageComparisonGroup(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoImageComparisonGroup"/> class by copying an existing group.
        /// </summary>
        /// <param name="ortoImageComparisonGroup">The source <see cref="OrtoImageComparisonGroup"/> to copy from.</param>
        public OrtoImageComparisonGroup(OrtoImageComparisonGroup? ortoImageComparisonGroup)
            : base(ortoImageComparisonGroup)
        {
            if (ortoImageComparisonGroup != null)
            {
                name = ortoImageComparisonGroup.name;
                OrtoImageComparisons = ortoImageComparisonGroup.OrtoImageComparisons;
            }
        }

        /// <summary>
        /// Gets the name of the orthophoto image comparison group.
        /// </summary>
        [JsonIgnore]
        public string? Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Gets or sets the collection of orthophoto image comparisons associated with this group.
        /// The internal storage ensures that these are sorted by their date and time.
        /// </summary>
        [JsonInclude, JsonPropertyName("OrtoImageComparisons")]
        public IEnumerable<OrtoImageComparison>? OrtoImageComparisons
        {
            get
            {
                return sortedDictionary?.Values;
            }

            private set
            {
                sortedDictionary.Clear();
                if (value != null)
                {
                    foreach (OrtoImageComparison ortoImageComparison in value)
                    {
                        if (ortoImageComparison == null)
                        {
                            continue;
                        }

                        sortedDictionary[ortoImageComparison.DateTime] = ortoImageComparison;
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves the orthophoto image comparison for the specified date and time, 
        /// or the closest preceding entry if an exact match is not found.
        /// </summary>
        /// <param name="dateTime">The target date and time to search for.</param>
        /// <returns>The matching <see cref="OrtoImageComparison"/>, or <see langword="null"/> if no suitable comparison exists.</returns>
        public OrtoImageComparison? GetOrtoImageComparison(DateTime dateTime)
        {
            if (!Core.Query.TryGetLowerValue(sortedDictionary, dateTime, out OrtoImageComparison? result))
            {
                return null;
            }

            return result;
        }
    }
}