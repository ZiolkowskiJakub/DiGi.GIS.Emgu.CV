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

            byte[] bytes = ortoDatas.GetBytes(dateTime);
            if(bytes == null)
            {
                return null;
            }

            Mat result = new Mat();
            CvInvoke.Imdecode(bytes, global::Emgu.CV.CvEnum.ImreadModes.Color, result);

            return result;
        }
    }
}

