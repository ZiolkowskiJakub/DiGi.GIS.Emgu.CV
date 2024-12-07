using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoImageComparison : Core.Classes.SerializableObject, Interfaces.IGISObject
    {
        [JsonInclude, JsonPropertyName("AverageColorSimilarity")]
        private double averageColorSimilarity;

        [JsonInclude, JsonPropertyName("ColorHistogramsFactor")]
        private double colorHistogramsFactor;

        [JsonInclude, JsonPropertyName("DateTime")]
        private DateTime dateTime;

        [JsonInclude, JsonPropertyName("GrayHistogramsFactor")]
        private double grayHistogramsFactor;

        [JsonInclude, JsonPropertyName("HammingDistance")]
        private int hammingDistace;
        
        [JsonInclude, JsonPropertyName("HistogramCorrelation")]
        private double histogramCorrelation;

        [JsonInclude, JsonPropertyName("ShapeComparisonFactor")]
        private double shapeComparisonFactor;

        [JsonInclude, JsonPropertyName("StructuralSimilarityIndex_AbsoluteDifference")]
        private double structuralSimilarityIndex_AbsoluteDifference;

        [JsonInclude, JsonPropertyName("StructuralSimilarityIndex_MatchTemplate")]
        private double structuralSimilarityIndex_MatchTemplate;

        public OrtoImageComparison(
            DateTime dateTime, 
            int hammingDistace, 
            double colorHistogramsFactor, 
            double grayHistogramsFactor, 
            double averageColorSimilarity,
            double histogramCorrelation,
            double shapeComparisonFactor,
            double structuralSimilarityIndex_AbsoluteDifference,
            double structuralSimilarityIndex_MatchTemplate) 
        {
            this.dateTime = dateTime;
            this.hammingDistace = hammingDistace;
            this.colorHistogramsFactor = colorHistogramsFactor;
            this.grayHistogramsFactor= grayHistogramsFactor;
            this.averageColorSimilarity = averageColorSimilarity;
            this.histogramCorrelation = histogramCorrelation;
            this.shapeComparisonFactor = shapeComparisonFactor;
            this.structuralSimilarityIndex_AbsoluteDifference = structuralSimilarityIndex_AbsoluteDifference;
            this.structuralSimilarityIndex_MatchTemplate = structuralSimilarityIndex_MatchTemplate;
        }

        public OrtoImageComparison(JsonObject jsonObject)
            : base(jsonObject)
        {

        }

        public OrtoImageComparison(OrtoImageComparison ortoImageComparison)
            : base(ortoImageComparison)
        {
            if(ortoImageComparison != null)
            {
                dateTime = ortoImageComparison.dateTime;
                hammingDistace = ortoImageComparison.hammingDistace;
                colorHistogramsFactor = ortoImageComparison.colorHistogramsFactor;
                grayHistogramsFactor = ortoImageComparison.grayHistogramsFactor;
                averageColorSimilarity = ortoImageComparison.averageColorSimilarity;
                histogramCorrelation = ortoImageComparison.histogramCorrelation;
                shapeComparisonFactor = ortoImageComparison.shapeComparisonFactor;
                structuralSimilarityIndex_AbsoluteDifference = ortoImageComparison.structuralSimilarityIndex_AbsoluteDifference;
                structuralSimilarityIndex_MatchTemplate = ortoImageComparison.structuralSimilarityIndex_MatchTemplate;
            }
        }

        [JsonIgnore]
        public double AverageColorSimilarity
        {
            get
            {
                return averageColorSimilarity;
            }
        }

        [JsonIgnore]
        public double ColorHistogramsFactor
        {
            get
            {
                return colorHistogramsFactor;
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

        [JsonIgnore]
        public double GrayHistogramsFactor
        {
            get
            {
                return grayHistogramsFactor;
            }
        }

        [JsonIgnore]
        public int HammingDistance
        {
            get
            {
                return hammingDistace;
            }
        }
        
        [JsonIgnore]
        public double HistogramCorrelation
        {
            get
            {
                return histogramCorrelation;
            }
        }

        [JsonIgnore]
        public double ShapeComparisonFactor
        {
            get
            {
                return shapeComparisonFactor;
            }
        }

        [JsonIgnore]
        public double StructuralSimilarityIndex_AbsoluteDifference
        {
            get
            {
                return structuralSimilarityIndex_AbsoluteDifference;
            }
        }

        [JsonIgnore]
        public double StructuralSimilarityIndex_MatchTemplate
        {
            get
            {
                return structuralSimilarityIndex_MatchTemplate;
            }
        }
    }
}
