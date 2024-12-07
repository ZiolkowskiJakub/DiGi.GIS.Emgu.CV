using DiGi.GIS.Classes;
using Emgu.CV;
using System;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Create
    {
        public static Mat Mat(this OrtoDatas ortoDatas, DateTime dateTime)
        {
            if(ortoDatas == null)
            {
                return null;
            }

            return Mat(ortoDatas.GetOrtoData(dateTime));
        }

        public static Mat Mat(this OrtoData ortoData)
        {
            byte[] bytes = ortoData?.Bytes;
            if (bytes == null)
            {
                return null;
            }

            Mat result = new Mat();
            CvInvoke.Imdecode(bytes, global::Emgu.CV.CvEnum.ImreadModes.Color, result);

            return result;
        }
    }
}

