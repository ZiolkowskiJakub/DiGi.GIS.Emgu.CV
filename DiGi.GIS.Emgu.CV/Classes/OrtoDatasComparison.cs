using DiGi.Core.Classes;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    /// <summary>
    /// Represents a collection of orthodata comparisons associated with a specific reference, 
    /// providing functionality to manage and retrieve comparison data based on dates.
    /// </summary>
    public class OrtoDatasComparison : GuidObject, Interfaces.IGISObject
    {
        [JsonInclude, JsonPropertyName("Reference")]
        private readonly string? reference;

        [JsonIgnore]
        private readonly SortedDictionary<DateTime, OrtoDataComparison> sortedDictionary = [];

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasComparison" /> class.
        /// </summary>
        /// <param name="reference">The reference identifier for the comparison data.</param>
        /// <param name="ortoDataComparisons">A collection of orthodata comparisons to be included in this set.</param>
        public OrtoDatasComparison(
            string? reference,
            IEnumerable<OrtoDataComparison>? ortoDataComparisons)
        {
            this.reference = reference;
            OrtoDataComparisons = ortoDataComparisons ?? [];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasComparison" /> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the data to initialize the instance.</param>
        public OrtoDatasComparison(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasComparison" /> class by copying an existing instance.
        /// </summary>
        /// <param name="ortoDatasComparison">The source <see cref="OrtoDatasComparison" /> instance to copy from.</param>
        public OrtoDatasComparison(OrtoDatasComparison? ortoDatasComparison)
            : base(ortoDatasComparison)
        {
        }

        /// <summary>
        /// Gets or sets the collection of orthodata comparisons. 
        /// The data is internally stored in a sorted dictionary to maintain chronological order.
        /// </summary>
        [JsonInclude, JsonPropertyName("OrtoDataComparisons")]
        public IEnumerable<OrtoDataComparison>? OrtoDataComparisons
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
                    foreach (OrtoDataComparison ortoDataComparison in value)
                    {
                        if (ortoDataComparison == null)
                        {
                            continue;
                        }

                        sortedDictionary[ortoDataComparison.DateTime] = ortoDataComparison;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the reference identifier associated with these orthodata comparisons.
        /// </summary>
        [JsonIgnore]
        public string? Reference
        {
            get
            {
                return reference;
            }
        }

        /// <summary>
        /// Extracts the unique years from all available orthodata comparisons and returns them as a range.
        /// </summary>
        /// <returns>A <see cref="Range{T}" /> of integers representing the years, or null if the internal data store is null.</returns>
        public Range<int>? GetYears()
        {
            if (sortedDictionary == null)
            {
                return null;
            }

            List<int> years = [];
            foreach (DateTime dateTime in sortedDictionary.Keys)
            {
                years.Add(dateTime.Year);
            }

            return new Range<int>(years);
        }

        /// <summary>
        /// Retrieves the orthodata comparison corresponding to the specified date, 
        /// or the closest preceding value if an exact match is not found.
        /// </summary>
        /// <param name="dateTime">The date used to search for a comparison.</param>
        /// <returns>The matching <see cref="OrtoDataComparison" /> instance, or null if no suitable record exists.</returns>
        public OrtoDataComparison? GetOrtoDataComparison(DateTime dateTime)
        {
            if (!Core.Query.TryGetLowerValue(sortedDictionary, dateTime, out OrtoDataComparison? result))
            {
                return null;
            }

            return result;
        }
    }
}