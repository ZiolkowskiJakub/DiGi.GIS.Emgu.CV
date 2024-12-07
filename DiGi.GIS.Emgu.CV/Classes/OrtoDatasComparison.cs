using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoDatasComparison : Core.Classes.SerializableObject, Interfaces.IGISObject
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
        private Dictionary<DateTime, OrtoDataComparison> dictionary = new Dictionary<DateTime, OrtoDataComparison>();

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
                return dictionary.Values;
            }

            private set
            {
                dictionary.Clear();
                if (value != null)
                {
                    foreach (OrtoDataComparison ortoDataComparison in value)
                    {
                        if (ortoDataComparison == null)
                        {
                            continue;
                        }

                        dictionary[ortoDataComparison.DateTime] = ortoDataComparison;
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
    }
}
