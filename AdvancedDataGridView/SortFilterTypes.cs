#region License
// Advanced DataGridView
//
// Copyright (c), 2020 Vladimir Bershadsky <vladimir@galileng.com>
// Copyright (c), 2014 Davide Gironi <davide.gironi@gmail.com>
// Original work Copyright (c), 2013 Zuby <zuby@me.com>
//
// Please refer to LICENSE file for licensing information.
#endregion

namespace Zuby.ADGV
{
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
}
