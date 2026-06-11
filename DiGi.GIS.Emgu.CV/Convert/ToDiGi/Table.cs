using DiGi.Core.IO.Table.Classes;
using DiGi.GIS.Classes;
using DiGi.GIS.Emgu.CV.Classes;
using System.Collections.Generic;

namespace DiGi.GIS.Emgu.CV
{
    public static partial class Convert
    {
        /// <summary>
        /// Converts a collection of orthodata comparisons and optional year built data into a <see cref="Table"/>.
        /// </summary>
        /// <param name="ortoDatasComparisons">The collection of orthodata comparison data to convert.</param>
        /// <param name="yearBuiltDatas">An optional collection of year built data to include in the table.</param>
        /// <returns>A <see cref="Table"/> instance containing the converted data, or <c>null</c> if the source orthodata comparisons are null.</returns>
        public static Table? ToDiGi_Table(this IEnumerable<OrtoDatasComparison>? ortoDatasComparisons, IEnumerable<YearBuiltData>? yearBuiltDatas = null)
        {
            if (ortoDatasComparisons == null)
            {
                return null;
            }

            Table result = new();

            result.AddRange(ortoDatasComparisons, yearBuiltDatas);

            return result;
        }
    }
}