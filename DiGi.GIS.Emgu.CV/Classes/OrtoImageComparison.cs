using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoImageComparison : Core.Classes.SerializableObject, Interfaces.IGISObject
    {
        [JsonInclude, JsonPropertyName("AverageColorSimilarity")]
        private readonly float averageColorSimilarity;

        [JsonInclude, JsonPropertyName("ColorDistributionShift")]
        private readonly float colorDistributionShift;

        [JsonInclude, JsonPropertyName("DateTime")]
        private readonly DateTime dateTime;

        [JsonInclude, JsonPropertyName("GrayHistogramFactor")]
        private readonly float grayHistogramFactor;

        [JsonInclude, JsonPropertyName("HammingDistance")]
        private readonly int hammingDistace;

        [JsonInclude, JsonPropertyName("HistogramCorrelation")]
        private readonly float histogramCorrelation;

        [JsonInclude, JsonPropertyName("MeanLaplacianFactor")]
        private readonly float meanLaplacianFactor;

        [JsonInclude, JsonPropertyName("OpticalFlowAverageMagnitude")]
        private readonly float opticalFlowAverageMagnitude;

        [JsonInclude, JsonPropertyName("ORBFeatureMatchingFactor")]
        private readonly float oRBFeatureMatchingFactor;

        [JsonInclude, JsonPropertyName("ShapeComparisonFactor")]
        private readonly float shapeComparisonFactor;

        [JsonInclude, JsonPropertyName("StandardDeviationLaplacianFactor")]
        private readonly float standardDeviationLaplacianFactor;

        [JsonInclude, JsonPropertyName("StructuralSimilarityIndex_AbsoluteDifference")]
        private readonly float structuralSimilarityIndex_AbsoluteDifference;

        [JsonInclude, JsonPropertyName("StructuralSimilarityIndex_MatchTemplate")]
        private readonly float structuralSimilarityIndex_MatchTemplate;
        
        public OrtoImageComparison(
            DateTime dateTime,
            int hammingDistace,
            float colorDistributionShift,
            float grayHistogramFactor,
            float averageColorSimilarity,
            float histogramCorrelation,
            float shapeComparisonFactor,
            float structuralSimilarityIndex_AbsoluteDifference,
            float structuralSimilarityIndex_MatchTemplate,
            float meanLaplacianFactor,
            float standardDeviationLaplacianFactor,
            float opticalFlowAverageMagnitude,
            float oRBFeatureMatchingFactor)
        {
            this.dateTime = dateTime;
            this.hammingDistace = hammingDistace;
            this.colorDistributionShift = colorDistributionShift;
            this.grayHistogramFactor = grayHistogramFactor;
            this.averageColorSimilarity = averageColorSimilarity;
            this.histogramCorrelation = histogramCorrelation;
            this.shapeComparisonFactor = shapeComparisonFactor;
            this.structuralSimilarityIndex_AbsoluteDifference = structuralSimilarityIndex_AbsoluteDifference;
            this.structuralSimilarityIndex_MatchTemplate = structuralSimilarityIndex_MatchTemplate;
            this.meanLaplacianFactor = meanLaplacianFactor;
            this.standardDeviationLaplacianFactor = standardDeviationLaplacianFactor;
            this.opticalFlowAverageMagnitude = opticalFlowAverageMagnitude;
            this.oRBFeatureMatchingFactor = oRBFeatureMatchingFactor;
        }

        public OrtoImageComparison(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        public OrtoImageComparison(OrtoImageComparison ortoImageComparison)
            : base(ortoImageComparison)
        {
            if (ortoImageComparison != null)
            {
                dateTime = ortoImageComparison.dateTime;
                hammingDistace = ortoImageComparison.hammingDistace;
                colorDistributionShift = ortoImageComparison.colorDistributionShift;
                grayHistogramFactor = ortoImageComparison.grayHistogramFactor;
                averageColorSimilarity = ortoImageComparison.averageColorSimilarity;
                histogramCorrelation = ortoImageComparison.histogramCorrelation;
                shapeComparisonFactor = ortoImageComparison.shapeComparisonFactor;
                structuralSimilarityIndex_AbsoluteDifference = ortoImageComparison.structuralSimilarityIndex_AbsoluteDifference;
                structuralSimilarityIndex_MatchTemplate = ortoImageComparison.structuralSimilarityIndex_MatchTemplate;
                meanLaplacianFactor = ortoImageComparison.meanLaplacianFactor;
                standardDeviationLaplacianFactor = ortoImageComparison.standardDeviationLaplacianFactor;
                opticalFlowAverageMagnitude = ortoImageComparison.opticalFlowAverageMagnitude;
                oRBFeatureMatchingFactor = ortoImageComparison.oRBFeatureMatchingFactor;
            }
        }

        [JsonIgnore]
        public float AverageColorSimilarity
        {
            get
            {
                return averageColorSimilarity;
            }
        }

        [JsonIgnore]
        public float ColorDistributionShift
        {
            get
            {
                return colorDistributionShift;
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
        public float GrayHistogramFactor
        {
            get
            {
                return grayHistogramFactor;
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
        public float HistogramCorrelation
        {
            get
            {
                return histogramCorrelation;
            }
        }

        [JsonIgnore]
        public float MeanLaplacianFactor
        {
            get
            {
                return meanLaplacianFactor;
            }
        }

        [JsonIgnore]
        public float OpticalFlowAverageMagnitude
        {
            get
            {
                return opticalFlowAverageMagnitude;
            }
        }

        [JsonIgnore]
        public float ORBFeatureMatchingFactor
        {
            get
            {
                return oRBFeatureMatchingFactor;
            }
        }

        [JsonIgnore]
        public float ShapeComparisonFactor
        {
            get
            {
                return shapeComparisonFactor;
            }
        }

        [JsonIgnore]
        public float StandardDeviationLaplacianFactor
        {
            get
            {
                return standardDeviationLaplacianFactor;
            }
        }

        [JsonIgnore]
        public float StructuralSimilarityIndex_AbsoluteDifference
        {
            get
            {
                return structuralSimilarityIndex_AbsoluteDifference;
            }
        }

        [JsonIgnore]
        public float StructuralSimilarityIndex_MatchTemplate
        {
            get
            {
                return structuralSimilarityIndex_MatchTemplate;
            }
        }
    }
}