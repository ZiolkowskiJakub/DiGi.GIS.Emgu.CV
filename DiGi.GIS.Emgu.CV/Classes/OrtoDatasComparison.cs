using DiGi.Core.Classes;
using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Enums;
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

        [JsonInclude, JsonPropertyName("BuildingGeneralFunction")]
        private BuildingGeneralFunction? buildingGeneralFunction;

        [JsonInclude, JsonPropertyName("BuildingPhase")]
        private BuildingPhase? buildingPhase;

        [JsonInclude, JsonPropertyName("Storeys")]
        private ushort storeys;

        [JsonInclude, JsonPropertyName("Area")]
        private double area;

        [JsonInclude, JsonPropertyName("Location")]
        private Point2D location;

        [JsonIgnore]
        private SortedDictionary<DateTime, OrtoDataComparison> sortedDictionary = new SortedDictionary<DateTime, OrtoDataComparison>();

        public OrtoDatasComparison(
            string reference, 
            BuildingGeneralFunction? buildingGeneralFunction,
            BuildingPhase? buildingPhase,
            ushort storeys,
            double area,
            Point2D location,
            IEnumerable<OrtoDataComparison> ortoDataComparisons) 
        {
            this.reference = reference;
            this.buildingGeneralFunction = buildingGeneralFunction;
            this.buildingPhase = buildingPhase;
            this.storeys = storeys;
            this.area = area;
            this.location = location;
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
                return sortedDictionary.Values;
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

        [JsonIgnore]
        public BuildingGeneralFunction? BuildingGeneralFunction
        {
            get
            {
                return buildingGeneralFunction;
            }
        }

        [JsonIgnore]
        public BuildingPhase? BuildingPhase
        {
            get
            {
                return buildingPhase;
            }
        }

        [JsonIgnore]
        public ushort Storeys
        {
            get
            {
                return storeys;
            }
        }

        [JsonIgnore]
        public double Area
        {
            get
            {
                return area;
            }
        }

        [JsonIgnore]
        public Point2D Location
        {
            get
            {
                return location;
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
