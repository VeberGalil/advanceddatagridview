using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zuby.ADGV
{
    internal class SortFilterMenuStrip : ContextMenuStrip, ISortFilterMenu
    {
        public FilterType ActiveFilterType => throw new NotImplementedException();

        public SortType ActiveSortType => throw new NotImplementedException();

        public Type DataType => throw new NotImplementedException();

        public bool DoesTextFilterRemoveNodesOnSearch { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string FilterString => throw new NotImplementedException();

        public bool IsFilterChecklistEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsFilterCustomEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsFilterDateAndTimeEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsFilterEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsFilterNOTINLogicEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsSortEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override RightToLeft RightToLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string SortString => throw new NotImplementedException();

        public event EventHandler FilterChanged;
        public event EventHandler SortChanged;

        public void CleanFilter()
        {
            throw new NotImplementedException();
        }

        public void CleanSort()
        {
            throw new NotImplementedException();
        }

        public void SetChecklistTextFilterRemoveNodesOnSearchMode(bool enabled)
        {
            throw new NotImplementedException();
        }

        public void SetFilterChecklistEnabled(bool enabled)
        {
            throw new NotImplementedException();
        }

        public void SetFilterCustomEnabled(bool enabled)
        {
            throw new NotImplementedException();
        }

        public void SetFilterEnabled(bool enabled)
        {
            throw new NotImplementedException();
        }

        public void SetLoadedMode(bool enabled)
        {
            throw new NotImplementedException();
        }

        public void SetSortEnabled(bool enabled)
        {
            throw new NotImplementedException();
        }

        public void Show(Control control, int x, int y, bool _restoreFilter)
        {
            throw new NotImplementedException();
        }

        public void Show(Control control, int x, int y, IEnumerable<DataGridViewCell> vals)
        {
            throw new NotImplementedException();
        }

        public void SortAsc()
        {
            throw new NotImplementedException();
        }

        public void SortDesc()
        {
            throw new NotImplementedException();
        }
    }
}
