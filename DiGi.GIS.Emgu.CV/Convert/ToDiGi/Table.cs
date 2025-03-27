using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Convert
    {
        public static Table ToDiGi_Table(this IEnumerable<OrtoDatasComparison> ortoDatasComparisons, IEnumerable<YearBuiltData> yearBuiltDatas = null)
        {
            if(ortoDatasComparisons == null)
            {
                return null;
            }

            Table result = new Table();

            result.AddRange(ortoDatasComparisons, yearBuiltDatas);

            return result;
        }
    }
}
