using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoDataComparison : Core.Classes.SerializableObject, Interfaces.IGISObject
    {
        [JsonInclude, JsonPropertyName("DateTime")]
        private DateTime dateTime;

        [JsonIgnore]
        private Dictionary<string, OrtoImageComparisonGroup> dictionary = new Dictionary<string, OrtoImageComparisonGroup>();

        public OrtoDataComparison(DateTime dateTime, IEnumerable<OrtoImageComparisonGroup> ortoImageComparisonGroups)
        {
            this.dateTime = dateTime;
            OrtoImageComparisonGroups = ortoImageComparisonGroups;
        }

        public OrtoDataComparison(JsonObject jsonObject)
            :base(jsonObject)
        {

        }

        public OrtoDataComparison(OrtoDataComparison ortoDataComparison)
            : base(ortoDataComparison)
        {
            if(ortoDataComparison != null)
            {
                dateTime = ortoDataComparison.dateTime;
                OrtoImageComparisonGroups = ortoDataComparison.OrtoImageComparisonGroups;
            }
        }

        [JsonIgnore]
        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
        }

        [JsonInclude, JsonPropertyName("OrtoImageComparisonGroups")]
        public IEnumerable<OrtoImageComparisonGroup> OrtoImageComparisonGroups
        {
            get
            {
                return dictionary?.Values;
            }

            private set
            {
                dictionary.Clear();
                if(value != null)
                {
                    foreach(OrtoImageComparisonGroup ortoImageComparisonGroup in value)
                    {
                        if(ortoImageComparisonGroup == null)
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