#region License
// Advanced DataGridView
//
// Copyright (c), 2020 Vladimir Bershadsky <vladimir@galileng.com>
// Based on Copyright (c), 2014 Davide Gironi <davide.gironi@gmail.com>
// Original work Copyright (c), 2013 Zuby <zuby@me.com>
//
// Please refer to LICENSE file for licensing information.
#endregion


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

        #region // Constructor




        #endregion 


        #region // Public properties
        public FilterType ActiveFilterType => throw new NotImplementedException();
        public SortType ActiveSortType => throw new NotImplementedException();
        public Type DataType => throw new NotImplementedException();
        public string FilterString => throw new NotImplementedException();
        public string SortString => throw new NotImplementedException();
        public bool IsFilterChecklistEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsFilterCustomEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsFilterDateAndTimeEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsFilterEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsFilterNOTINLogicEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsSortEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool DoesTextFilterRemoveNodesOnSearch { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public override RightToLeft RightToLeft { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        #endregion

        #region // Public events
        public event EventHandler FilterChanged;
        public event EventHandler SortChanged;
        #endregion


        #region // Public Filter and Sort enable/disable methods
        
        // Enable or disable all Filter capabilities
        public void SetFilterEnabled(bool enabled)
        {
            throw new NotImplementedException();
        }

        // Enable or disable Sorting capabilities
        public void SetSortEnabled(bool enabled)
        {
            throw new NotImplementedException();
        }

        // Enable or disable usage of CheckList Filter
        public void SetFilterChecklistEnabled(bool enabled)
        {
            throw new NotImplementedException();
        }

        // Enable or disable Custom Filter capabilities
        public void SetFilterCustomEnabled(bool enabled)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region // Set preloaded mode

        public void SetLoadedMode(bool enabled)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region // Show menu

        public void Show(Control control, int x, int y, bool _restoreFilter)
        {
            throw new NotImplementedException();
        }

        public void Show(Control control, int x, int y, IEnumerable<DataGridViewCell> vals)
        {
            throw new NotImplementedException();
        }

        #endregion


        #region // Public Sort methods

        public void CleanSort()
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

        #endregion


        #region // Public Filter methods
        public void CleanFilter()
        {
            throw new NotImplementedException();
        }
        public void SetChecklistTextFilterRemoveNodesOnSearchMode(bool enabled)
        {
            throw new NotImplementedException();
        }


        #endregion



        #region // ContextMenu Events 

        protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
        {
            base.OnClosed(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
        }

        #endregion


        #region // Sort actions


        #endregion



        #region // Filter actions


        #endregion
    }
}
