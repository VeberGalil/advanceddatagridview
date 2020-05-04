#region License
// Advanced DataGridView
//
// Copyright (c), 2020 Vladimir Bershadsky <vladimir@galileng.com>
// Copyright (c), 2014 Davide Gironi <davide.gironi@gmail.com>
// Original work Copyright (c), 2013 Zuby <zuby@me.com>
//
// Please refer to LICENSE file for licensing information.
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Zuby.ADGV
{
    internal class SortFilterMenuFactory
    {
        public static ISortFilterMenu CreateDefaultMenu(Type columnDataType)
        {
            bool old = false;
            ISortFilterMenu filterMenu;
            if (old)
                filterMenu = new MenuStrip(columnDataType);
            else
                filterMenu = new SortFilterMenuStrip(columnDataType);
            return filterMenu;
        }
    }

    internal interface ISortFilterMenu
    {
        FilterType ActiveFilterType { get; }
        SortType ActiveSortType { get; }
        bool DoesTextFilterRemoveNodesOnSearch { get; set; }
        string FilterString { get; }
        bool IsFilterChecklistEnabled { get; set; }
        bool IsFilterCustomEnabled { get; set; }
        bool IsFilterDateAndTimeEnabled { get; set; }
        bool IsFilterEnabled { get; set; }
        bool IsFilterNOTINLogicEnabled { get; set; }
        bool IsSortEnabled { get; set; }
        string SortString { get; }
        RightToLeft RightToLeft { get; set; }

        event EventHandler FilterChanged;
        event EventHandler SortChanged;

        void CleanFilter();
        void CleanSort();
        void SetChecklistTextFilterRemoveNodesOnSearchMode(bool enabled);
        void SetFilterChecklistEnabled(bool enabled);
        void SetFilterCustomEnabled(bool enabled);
        void SetFilterEnabled(bool enabled);
        void SetLoadedMode(bool enabled);
        void SetSortEnabled(bool enabled);
        void Show(Control control, int x, int y, bool _restoreFilter);
        void Show(Control control, int x, int y, IEnumerable<DataGridViewCell> vals);
        void SortAsc();
        void SortDesc();
        void LoadFilter(IEnumerable<DataGridViewCell> valueCells, string filter);
    }
}