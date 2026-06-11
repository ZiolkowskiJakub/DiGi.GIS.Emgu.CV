using System;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace DiGi.GIS.Emgu.CV.Classes
{
    /// <summary>
    /// Represents the results of a comparison between two ortho-images, containing various computer vision metrics 
    /// such as color similarity, histogram correlation, and structural indices.
    /// </summary>
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
        
        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoImageComparison"/> class with specified comparison metrics.
        /// </summary>
        /// <param name="dateTime">The date and time when the comparison was performed.</param>
        /// <param name="hammingDistace">The Hamming distance between matrices based on their average gray masks.</param>
        /// <param name="colorDistributionShift">The Euclidean distance between the mean colors of the two images.</param>
        /// <param name="grayHistogramFactor">The correlation factor between the grayscale histograms of the two image matrices.</param>
        /// <param name="averageColorSimilarity">The Euclidean distance between the average colors of the two images.</param>
        /// <param name="histogramCorrelation">The correlation value between the hue histograms of the two images.</param>
        /// <param name="shapeComparisonFactor">The sum of absolute differences between the Hu Moments of the primary contours.</param>
        /// <param name="structuralSimilarityIndex_AbsoluteDifference">The similarity index based on the absolute difference of grayscale representations.</param>
        /// <param name="structuralSimilarityIndex_MatchTemplate">The similarity index using template matching with normalized cross-correlation.</param>
        /// <param name="meanLaplacianFactor">The calculated mean factor of the Laplacian.</param>
        /// <param name="standardDeviationLaplacianFactor">The calculated standard deviation factor of the Laplacian.</param>
        /// <param name="opticalFlowAverageMagnitude">The average magnitude of optical flow vectors between two images.</param>
        /// <param name="oRBFeatureMatchingFactor">The average distance of the ORB feature matches; a lower value indicates a better match.</param>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoImageComparison"/> class from a <see cref="JsonObject"/>.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the comparison data.</param>
        public OrtoImageComparison(JsonObject jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoImageComparison"/> class by copying an existing instance.
        /// </summary>
        /// <param name="ortoImageComparison">The source <see cref="OrtoImageComparison"/> instance to copy from.</param>
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

        /// <summary>
        /// Gets the Euclidean distance between the average colors of the two images.
        /// </summary>
        [JsonIgnore]
        public float AverageColorSimilarity
        {
            get
            {
                return averageColorSimilarity;
            }
        }

        /// <summary>
        /// Gets the Euclidean distance between the mean colors of the two images.
        /// </summary>
        [JsonIgnore]
        public float ColorDistributionShift
        {
            get
            {
                return colorDistributionShift;
            }
        }

        /// <summary>
        /// Gets the date and time when the image comparison was performed.
        /// </summary>
        [JsonIgnore]
        public DateTime DateTime
        {
            get
            {
                return dateTime;
            }
        }

        /// <summary>
        /// Gets the correlation factor between the grayscale histograms of the two images.
        /// </summary>
        [JsonIgnore]
        public float GrayHistogramFactor
        {
            get
            {
                return grayHistogramFactor;
            }
        }

        /// <summary>
        /// Gets the Hamming distance between matrices based on their average gray masks.
        /// </summary>
        [JsonIgnore]
        public int HammingDistance
        {
            get
            {
                return hammingDistace;
            }
        }

        /// <summary>
        /// Gets the correlation value between the hue histograms of the two images.
        /// </summary>
        [JsonIgnore]
        public float HistogramCorrelation
        {
            get
            {
                return histogramCorrelation;
            }
        }

        /// <summary>
        /// Gets the calculated mean factor of the Laplacian.
        /// </summary>
        [JsonIgnore]
        public float MeanLaplacianFactor
        {
            get
            {
                return meanLaplacianFactor;
            }
        }

        /// <summary>
        /// Gets the average magnitude of optical flow vectors between two images.
        /// </summary>
        [JsonIgnore]
        public float OpticalFlowAverageMagnitude
        {
            get
            {
                return opticalFlowAverageMagnitude;
            }
        }

        /// <summary>
        /// Gets the average distance of the ORB feature matches; a lower value indicates a better match.
        /// </summary>
        [JsonIgnore]
        public float ORBFeatureMatchingFactor
        {
            get
            {
                return oRBFeatureMatchingFactor;
            }
        }

        /// <summary>
        /// Gets the sum of absolute differences between the Hu Moments of the primary contours.
        /// </summary>
        [JsonIgnore]
        public float ShapeComparisonFactor
        {
            get
            {
                return shapeComparisonFactor;
            }
        }

        /// <summary>
        /// Gets the calculated standard deviation factor of the Laplacian.
        /// </summary>
        [JsonIgnore]
        public float StandardDeviationLaplacianFactor
        {
            get
            {
                return standardDeviationLaplacianFactor;
            }
        }

        /// <summary>
        /// Gets the similarity index based on the absolute difference of grayscale representations.
        /// </summary>
        [JsonIgnore]
        public float StructuralSimilarityIndex_AbsoluteDifference
        {
            get
            {
                return structuralSimilarityIndex_AbsoluteDifference;
            }
        }

        /// <summary>
        /// Gets the similarity index using template matching with normalized cross-correlation.
        /// </summary>
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