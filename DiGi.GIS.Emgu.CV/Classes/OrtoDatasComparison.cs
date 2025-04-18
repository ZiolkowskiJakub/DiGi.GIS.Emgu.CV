using DiGi.Core.Classes;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoDatasComparison : GuidObject, Interfaces.IGISObject
    {
        [JsonInclude, JsonPropertyName("Reference")]
        private string reference;

        [JsonIgnore]
        private SortedDictionary<DateTime, OrtoDataComparison> sortedDictionary = new SortedDictionary<DateTime, OrtoDataComparison>();

        public OrtoDatasComparison(
            string reference, 
            IEnumerable<OrtoDataComparison> ortoDataComparisons) 
        {
            this.reference = reference;
            OrtoDataComparisons = ortoDataComparisons;
        }

        public OrtoDatasComparison(JsonObject jsonObject)
            :base(jsonObject)
        {

        }

        public OrtoDatasComparison(OrtoDatasComparison ortoDatasComparison)
            : base(ortoDatasComparison)
        {

        }

        [JsonInclude, JsonPropertyName("OrtoDataComparisons")]
        public IEnumerable<OrtoDataComparison> OrtoDataComparisons
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

        [JsonIgnore]
        public string Reference
        {
            get
            {
                return reference;
            }
        }

        public Range<int> GetYears()
        {
            if(sortedDictionary == null)
            {
                return null;
            }

            List<int> years = new List<int>();
            foreach(DateTime dateTime in sortedDictionary.Keys)
            {
                years.Add(dateTime.Year);
            }

            return new Range<int>(years);
        }

        public OrtoDataComparison GetOrtoDataComparison(DateTime dateTime)
        {
            if (!Core.Query.TryGetLowerValue(sortedDictionary, dateTime, out OrtoDataComparison result))
            {
                return null;
            }

            return result;
        }
    }
}
