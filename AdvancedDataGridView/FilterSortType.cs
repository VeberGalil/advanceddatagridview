using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zuby.ADGV
{
    #region public enum

    /// <summary>
    /// Filter type
    /// </summary>
    internal enum FilterType : byte
    {
        None = 0,
        Custom = 1,
        CheckList = 2,
        Loaded = 3
    }


    /// <summary>
    /// Sort type
    /// </summary>
    internal enum SortType : byte
    {
        None = 0,
        Asc = 1,
        Desc = 2
    }

    #endregion
}
