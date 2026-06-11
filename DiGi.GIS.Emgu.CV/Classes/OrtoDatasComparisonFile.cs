using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Interfaces;
using System.Text.Json.Nodes;

namespace DiGi.GIS.Emgu.CV.Classes
{
    /// <summary>
    /// Represents a storage file for orthophoto data comparison information, providing functionality to persist and retrieve <see cref="OrtoDatasComparison" /> objects.
    /// </summary>
    public class OrtoDatasComparisonFile : Core.IO.File.Classes.StorageFile<OrtoDatasComparison>, IGISObject
    {
        /// <summary>
        /// Generates a unique reference for the specified orthophoto data comparison object.
        /// </summary>
        /// <param name="ortoDatasComparison">The orthophoto data comparison object to generate a reference for.</param>
        /// <returns>A <see cref="UniqueReference" /> associated with the provided object, or null if the object is null.</returns>
        public override UniqueReference? GetUniqueReference(OrtoDatasComparison? ortoDatasComparison)
        {
            return GetUniqueReference(ortoDatasComparison?.Reference);
        }

        /// <summary>
        /// Generates a unique reference based on the provided string identifier for orthophoto data.
        /// </summary>
        /// <param name="reference">The string reference identifier.</param>
        /// <returns>A <see cref="UniqueReference" /> if the reference is not null; otherwise, null.</returns>
        public static UniqueReference? GetUniqueReference(string? reference)
        {
            if (reference == null)
            {
                return null;
            }

            return new UniqueIdReference(typeof(OrtoDatas), reference);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasComparisonFile" /> class by copying an existing instance.
        /// </summary>
        /// <param name="ortoDatasComparisonFile">The source storage file to copy from.</param>
        public OrtoDatasComparisonFile(OrtoDatasComparisonFile? ortoDatasComparisonFile)
            : base(ortoDatasComparisonFile)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasComparisonFile" /> class from a JSON object.
        /// </summary>
        /// <param name="jsonObject">The JSON object containing the storage file data.</param>
        public OrtoDatasComparisonFile(JsonObject? jsonObject)
            : base(jsonObject)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrtoDatasComparisonFile" /> class from the specified file path.
        /// </summary>
        /// <param name="path">The path to the storage file.</param>
        public OrtoDatasComparisonFile(string? path)
            : base(path)
        {
        }
    }
}