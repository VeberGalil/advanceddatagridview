using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Zuby.ADGV
{
    internal interface ISortFilterMenu
    {
        FilterType ActiveFilterType { get; }
        SortType ActiveSortType { get; }
        Type DataType { get; }
        bool DoesTextFilterRemoveNodesOnSearch { get; set; }
        string FilterString { get; }
        bool IsFilterChecklistEnabled { get; set; }
        bool IsFilterCustomEnabled { get; set; }
        bool IsFilterDateAndTimeEnabled { get; set; }
        bool IsFilterEnabled { get; set; }
        bool IsFilterNOTINLogicEnabled { get; set; }
        bool IsSortEnabled { get; set; }
        string SortString { get; }

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
    }
}