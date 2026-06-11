using DiGi.GIS.Classes;
using Emgu.CV;
using System;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Create
    {
        /// <summary>
        /// Creates an Emgu CV Mat from the ortho data associated with a specific date and time.
        /// </summary>
        /// <param name="ortoDatas">The collection of ortho data.</param>
        /// <param name="dateTime">The date and time for which to retrieve the ortho data.</param>
        /// <returns>A <see cref="global::Emgu.CV.Mat"/> instance if successful; otherwise, null.</returns>
        public static Mat? Mat(this OrtoDatas? ortoDatas, DateTime dateTime)
        {
            if (ortoDatas == null)
            {
                return null;
            }

            return Mat(ortoDatas.GetOrtoData(dateTime));
        }

        /// <summary>
        /// Creates an Emgu CV Mat from a specific ortho data object by decoding its byte array.
        /// </summary>
        /// <param name="ortoData">The ortho data containing the image bytes.</param>
        /// <returns>A <see cref="global::Emgu.CV.Mat"/> instance if successful; otherwise, null.</returns>
        public static Mat? Mat(this OrtoData? ortoData)
        {
            byte[]? bytes = ortoData?.Bytes;
            if (bytes == null)
            {
                return null;
            }

            Mat result = new();
            CvInvoke.Imdecode(bytes, global::Emgu.CV.CvEnum.ImreadModes.Unchanged, result);

            return result;
        }
    }
}