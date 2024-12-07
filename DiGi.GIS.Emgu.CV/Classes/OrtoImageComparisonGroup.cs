using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoImageComparisonGroup : Core.Classes.SerializableObject, Interfaces.IGISObject
    {
        [JsonInclude, JsonPropertyName("Name")]
        private string name;

        [JsonIgnore]
        private Dictionary<DateTime, OrtoImageComparison> dictionary = new Dictionary<DateTime, OrtoImageComparison>();

        public OrtoImageComparisonGroup(string name, IEnumerable<OrtoImageComparison> ortoImageComparisons)
        {
            this.name = name;
            OrtoImageComparisons = ortoImageComparisons;
        }

        public OrtoImageComparisonGroup(JsonObject jsonObject)
            :base(jsonObject)
        {

        }

        public OrtoImageComparisonGroup(OrtoImageComparisonGroup ortoImageComparisonGroup)
            : base(ortoImageComparisonGroup)
        {
            if(ortoImageComparisonGroup != null)
            {
                name = ortoImageComparisonGroup.name;
                OrtoImageComparisons = ortoImageComparisonGroup.OrtoImageComparisons;
            }
        }

        [JsonIgnore]
        public string Name
        {
            get
            {
                return name;
            }
        }

        [JsonInclude, JsonPropertyName("OrtoImageComparisons")]
        public IEnumerable<OrtoImageComparison> OrtoImageComparisons
        {
            get
            {
                return dictionary.Values;
            }

            private set
            {
                dictionary.Clear();
                if(value != null)
                {
                    foreach(OrtoImageComparison ortoImageComparison in value)
                    {
                        if(ortoImageComparison == null)
                        {
                            continue;
                        }

                        dictionary[ortoImageComparison.DateTime] = ortoImageComparison;
                    }
                }
            }
        }
    }
}