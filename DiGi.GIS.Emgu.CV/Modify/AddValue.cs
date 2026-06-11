using DiGi.Core.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Emgu.CV.Classes;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Modify
    {
        /// <summary>
        /// Adds a value to the specified <see cref="OrtoDatasComparisonFile"/> based on building data and provided options.
        /// </summary>
        /// <param name="ortoDatasComparisonFile">The target comparison file where the generated OrtoDatas comparison value will be added.</param>
        /// <param name="builidng2D">The 2D building object used as a source for creating the comparison data.</param>
        /// <param name="ortoDatasComparisonOptions">Optional settings to configure how the comparison data is generated.</param>
        /// <returns>A unique reference of the added value, or null if the input parameters were null or the creation process failed.</returns>
        public static UniqueReference? AddValue(this OrtoDatasComparisonFile? ortoDatasComparisonFile, Building2D? builidng2D, OrtoDatasComparisonOptions? ortoDatasComparisonOptions = null)
        {
            if (ortoDatasComparisonFile == null || builidng2D == null)
            {
                return null;
            }

            string? path = ortoDatasComparisonFile.Path;
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }

            string directory = System.IO.Path.GetDirectoryName(path);
            if (!System.IO.Directory.Exists(directory))
            {
                return null;
            }

            ortoDatasComparisonOptions ??= new OrtoDatasComparisonOptions();

            OrtoDatasComparison? ortoDatasComparison = Create.OrtoDatasComparison(builidng2D, directory, ortoDatasComparisonOptions.Years);
            if (ortoDatasComparison == null)
            {
                return null;
            }

            UniqueReference? result = ortoDatasComparisonFile.AddValue(ortoDatasComparison);
            return result;
        }
    }
}