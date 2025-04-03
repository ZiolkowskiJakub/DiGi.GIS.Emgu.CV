using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    public class OrtoImageComparison : Core.Classes.SerializableObject, Interfaces.IGISObject
    {
        [JsonInclude, JsonPropertyName("AverageColorSimilarity")]
        private float averageColorSimilarity;

        [JsonInclude, JsonPropertyName("ColorDistributionShift")]
        private float colorDistributionShift;

        [JsonInclude, JsonPropertyName("DateTime")]
        private DateTime dateTime;

        [JsonInclude, JsonPropertyName("GrayHistogramsFactor")]
        private float grayHistogramsFactor;

        [JsonInclude, JsonPropertyName("HammingDistance")]
        private int hammingDistace;
        
        [JsonInclude, JsonPropertyName("HistogramCorrelation")]
        private float histogramCorrelation;

        [JsonInclude, JsonPropertyName("ShapeComparisonFactor")]
        private float shapeComparisonFactor;

        [JsonInclude, JsonPropertyName("StructuralSimilarityIndex_AbsoluteDifference")]
        private float structuralSimilarityIndex_AbsoluteDifference;

        [JsonInclude, JsonPropertyName("StructuralSimilarityIndex_MatchTemplate")]
        private float structuralSimilarityIndex_MatchTemplate;

        [JsonInclude, JsonPropertyName("MeanLaplacianFactor")]
        private float meanLaplacianFactor;

        [JsonInclude, JsonPropertyName("StandardDeviationLaplacianFactor")]
        private float standardDeviationLaplacianFactor;

        [JsonInclude, JsonPropertyName("OpticalFlowAverageMagnitude")]
        private float opticalFlowAverageMagnitude;

        [JsonInclude, JsonPropertyName("ORBFeatureMatchingFactor")]
        private float oRBFeatureMatchingFactor;

        public OrtoImageComparison(
            DateTime dateTime, 
            int hammingDistace,
            float colorDistributionShift,
            float grayHistogramsFactor,
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
            this.grayHistogramsFactor= grayHistogramsFactor;
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
            if(ortoImageComparison != null)
            {
                dateTime = ortoImageComparison.dateTime;
                hammingDistace = ortoImageComparison.hammingDistace;
                colorDistributionShift = ortoImageComparison.colorDistributionShift;
                grayHistogramsFactor = ortoImageComparison.grayHistogramsFactor;
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
        public float GrayHistogramsFactor
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
        public float HistogramCorrelation
        {
            get
            {
                return histogramCorrelation;
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

        [JsonIgnore]
        public float MeanLaplacianFactor
        {
            get
            {
                return meanLaplacianFactor;
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
    }
}
