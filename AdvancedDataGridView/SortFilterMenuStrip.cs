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
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Zuby.ADGV
{
    internal class SortFilterMenuStrip : ContextMenuStrip, ISortFilterMenu
    {

        #region // Constructor 
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="dataType">Type of data in ADGV column that uses this menu</param>
        public SortFilterMenuStrip(Type dataType): base()
        {
            _dataType = dataType;
            // Initialize UI
            InitializeComponent();
            //
            // Set translations and icons to sort/filter menu items
            //
            cancelSortMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVClearSort.ToString()];
            cancelFilterMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVClearFilter.ToString()];
            customFilterMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVAddCustomFilter.ToString()];
            if (_dataType == typeof(DateTime) || _dataType == typeof(TimeSpan))
            {
                sortAscMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortDateTimeAsc.ToString()];
                sortDescMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortDateTimeDesc.ToString()];
                sortAscMenuItem.Image = Properties.Resources.MenuStrip_OrderASCnum;
                sortDescMenuItem.Image = Properties.Resources.MenuStrip_OrderDESCnum;
                customFilterLastFiltersListMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVCustomFilter.ToString()];
            }
            else if (_dataType == typeof(bool))
            {
                sortAscMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortBoolAsc.ToString()];
                sortDescMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortBoolDesc.ToString()];
                sortAscMenuItem.Image = Properties.Resources.MenuStrip_OrderASCbool;
                sortDescMenuItem.Image = Properties.Resources.MenuStrip_OrderDESCbool;
                customFilterLastFiltersListMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVCustomFilter.ToString()];
            }
            else if (_dataType == typeof(Int32) || _dataType == typeof(Int64) || _dataType == typeof(Int16) ||
                _dataType == typeof(UInt32) || _dataType == typeof(UInt64) || _dataType == typeof(UInt16) ||
                _dataType == typeof(Byte) || _dataType == typeof(SByte) || _dataType == typeof(Decimal) ||
                _dataType == typeof(Single) || _dataType == typeof(Double))
            {
                sortAscMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortNumAsc.ToString()];
                sortDescMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortNumDesc.ToString()];
                sortAscMenuItem.Image = Properties.Resources.MenuStrip_OrderASCnum;
                sortDescMenuItem.Image = Properties.Resources.MenuStrip_OrderDESCnum;
                customFilterLastFiltersListMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVCustomFilter.ToString()];
            }
            else
            {
                sortAscMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortTextAsc.ToString()];
                sortDescMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortTextDesc.ToString()];
                sortAscMenuItem.Image = Properties.Resources.MenuStrip_OrderASCtxt;
                sortDescMenuItem.Image = Properties.Resources.MenuStrip_OrderDESCtxt;
                customFilterLastFiltersListMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVCustomFilter.ToString()];
            }
            // Last custom filters
            customFilterLastFiltersListMenuItem.Enabled = _dataType != typeof(bool);
            customFilterLastFiltersListMenuItem.Checked = ActiveFilterType == FilterType.Custom;
            // Enable/disable context search in filter list
            if (_dataType == typeof(DateTime) || _dataType == typeof(TimeSpan) || _dataType == typeof(bool))
                filterSelectionListPanel.FilterContextSearchEnabled = false;
            // Set default NOT IN logic
            IsFilterNOTINLogicEnabled = false;
            // Set enablers to default values
            IsSortEnabled = true;
            IsFilterEnabled = true;
            IsFilterChecklistEnabled = true;
            IsFilterDateAndTimeEnabled = true;

        }

        private ToolStripMenuItem sortAscMenuItem;
        private ToolStripMenuItem sortDescMenuItem;
        private ToolStripMenuItem cancelSortMenuItem;
        private ToolStripSeparator toolStripSeparator1MenuItem;
        private ToolStripMenuItem cancelFilterMenuItem;
        private ToolStripMenuItem customFilterLastFiltersListMenuItem;
        private ToolStripMenuItem customFilterMenuItem;
        private ToolStripSeparator toolStripSeparator2MenuItem;
        private ToolStripMenuItem customFilterLastFilter1MenuItem;
        private ToolStripMenuItem customFilterLastFilter2MenuItem;
        private ToolStripMenuItem customFilterLastFilter3MenuItem;
        private ToolStripMenuItem customFilterLastFilter4MenuItem;
        private ToolStripMenuItem customFilterLastFilter5MenuItem;
        private ToolStripSeparator toolStripSeparator3MenuItem;
        private FilterSelectionListPanel filterSelectionListPanel;
        private SortFilterMenuStripResizer sortFilterMenuStripResizer;

        /// <summary>
        /// Standard UI builder routine
        /// </summary>
        private void InitializeComponent()
        {
            sortAscMenuItem = new ToolStripMenuItem();
            sortDescMenuItem = new ToolStripMenuItem();
            cancelSortMenuItem = new ToolStripMenuItem();
            toolStripSeparator1MenuItem = new ToolStripSeparator();
            cancelFilterMenuItem = new ToolStripMenuItem();
            customFilterLastFiltersListMenuItem = new ToolStripMenuItem();
            customFilterMenuItem = new ToolStripMenuItem();
            toolStripSeparator2MenuItem = new ToolStripSeparator();
            customFilterLastFilter1MenuItem = new ToolStripMenuItem();
            customFilterLastFilter2MenuItem = new ToolStripMenuItem();
            customFilterLastFilter3MenuItem = new ToolStripMenuItem();
            customFilterLastFilter4MenuItem = new ToolStripMenuItem();
            customFilterLastFilter5MenuItem = new ToolStripMenuItem();
            toolStripSeparator3MenuItem = new ToolStripSeparator();
            filterSelectionListPanel = new FilterSelectionListPanel();
            sortFilterMenuStripResizer = new SortFilterMenuStripResizer();
            SuspendLayout();
            //
            // SortFilterMenuStrip
            //
            this.BackColor = SystemColors.ControlLightLight;
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);
            this.Size = new Size(287, 370);
            this.Items.AddRange(new ToolStripItem[] {
                sortAscMenuItem,
                sortDescMenuItem,
                cancelSortMenuItem,
                toolStripSeparator1MenuItem,
                cancelFilterMenuItem,
                customFilterLastFiltersListMenuItem,
                toolStripSeparator3MenuItem,
                filterSelectionListPanel,
                sortFilterMenuStripResizer
            });
            //
            // sortASCMenuItem
            //
            sortAscMenuItem.Name = "sortASCMenuItem";
            sortAscMenuItem.Size = new Size(this.Width - 1, 22);
            sortAscMenuItem.Click += SortAscMenuItem_Click;
            sortAscMenuItem.MouseEnter += ContextMenuItem_MouseEnter;
            sortAscMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            //
            // sortDESCMenuItem
            //
            sortDescMenuItem.Name = "sortDESCMenuItem";
            sortDescMenuItem.Size = new Size(Width - 1, 22);
            sortDescMenuItem.Click += SortDescMenuItem_Click;
            sortDescMenuItem.MouseEnter += ContextMenuItem_MouseEnter;
            sortDescMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            //
            // cancelSortMenuItem
            //
            cancelSortMenuItem.Name = "cancelSortMenuItem";
            cancelSortMenuItem.Enabled = false;
            cancelSortMenuItem.Size = new Size(Width - 1, 22);
            cancelSortMenuItem.Click += CancelSortMenuItem_Click;
            cancelSortMenuItem.MouseEnter += ContextMenuItem_MouseEnter;
            //
            // toolStripSeparator1MenuItem
            //
            this.toolStripSeparator1MenuItem.Name = "toolStripSeparator1MenuItem";
            this.toolStripSeparator1MenuItem.Size = new Size(Width - 4, 6);
            //
            // cancelFilterMenuItem
            //
            cancelFilterMenuItem.Name = "cancelFilterMenuItem";
            cancelFilterMenuItem.Enabled = false;
            cancelFilterMenuItem.Size = new Size(Width - 1, 22);
            cancelFilterMenuItem.Click += CancelFilterMenuItem_Click;
            cancelFilterMenuItem.MouseEnter += ContextMenuItem_MouseEnter;
            //
            // customFilterLastFiltersListMenuItem
            //
            customFilterLastFiltersListMenuItem.Name = "customFilterLastFiltersListMenuItem";
            customFilterLastFiltersListMenuItem.Size = new Size(Width - 1, 22);
            customFilterLastFiltersListMenuItem.Image = Properties.Resources.ColumnHeader_Filtered;
            customFilterLastFiltersListMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            customFilterLastFiltersListMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                customFilterMenuItem,
                toolStripSeparator2MenuItem,
                customFilterLastFilter1MenuItem,
                customFilterLastFilter2MenuItem,
                customFilterLastFilter3MenuItem,
                customFilterLastFilter4MenuItem,
                customFilterLastFilter5MenuItem
            });
            customFilterLastFiltersListMenuItem.Paint += CustomFilterLastFiltersListMenuItem_Paint;
            customFilterLastFiltersListMenuItem.MouseEnter += ContextMenuItem_MouseEnter;
            //
            // customFilterMenuItem
            //
            customFilterMenuItem.Name = "customFilterMenuItem";
            customFilterMenuItem.Size = new Size(152, 22);
            customFilterMenuItem.Click += CustomFilterMenuItem_Click;
            //
            // toolStripMenuItem2
            //
            toolStripSeparator2MenuItem.Name = "toolStripSeparator2MenuItem";
            toolStripSeparator2MenuItem.Size = new Size(149, 6);
            toolStripSeparator2MenuItem.Visible = false;
            //
            // customFilterLastFilter1MenuItem
            //
            customFilterLastFilter1MenuItem.Name = "customFilterLastFilter1MenuItem";
            customFilterLastFilter1MenuItem.Size = new Size(152, 22);
            customFilterLastFilter1MenuItem.Tag = "0";
            customFilterLastFilter1MenuItem.Text = null;
            customFilterLastFilter1MenuItem.Visible = false;
            customFilterLastFilter1MenuItem.VisibleChanged += CustomFilterLastFilter1MenuItem_VisibleChanged;
            customFilterLastFilter1MenuItem.Click += CustomFilterLastFilterMenuItem_Click;
            customFilterLastFilter1MenuItem.TextChanged += CustomFilterLastFilterMenuItem_TextChanged;
            //
            // customFilterLastFilter2MenuItem
            //
            this.customFilterLastFilter2MenuItem.Name = "customFilterLastFilter2MenuItem";
            this.customFilterLastFilter2MenuItem.Size = new Size(152, 22);
            this.customFilterLastFilter2MenuItem.Tag = "1";
            this.customFilterLastFilter2MenuItem.Text = null;
            this.customFilterLastFilter2MenuItem.Visible = false;
            this.customFilterLastFilter2MenuItem.Click += CustomFilterLastFilterMenuItem_Click;
            this.customFilterLastFilter2MenuItem.TextChanged += CustomFilterLastFilterMenuItem_TextChanged;
            //
            // customFilterLastFilter3MenuItem
            //
            customFilterLastFilter3MenuItem.Name = "customFilterLastFilter3MenuItem";
            customFilterLastFilter3MenuItem.Size = new Size(152, 22);
            customFilterLastFilter3MenuItem.Tag = "2";
            customFilterLastFilter3MenuItem.Text = null;
            customFilterLastFilter3MenuItem.Visible = false;
            customFilterLastFilter3MenuItem.Click += CustomFilterLastFilterMenuItem_Click;
            customFilterLastFilter3MenuItem.TextChanged += CustomFilterLastFilterMenuItem_TextChanged;
            //
            // customFilterLastFilter3MenuItem
            //
            customFilterLastFilter4MenuItem.Name = "customFilterLastFilter4MenuItem";
            customFilterLastFilter4MenuItem.Size = new Size(152, 22);
            customFilterLastFilter4MenuItem.Tag = "3";
            customFilterLastFilter4MenuItem.Text = null;
            customFilterLastFilter4MenuItem.Visible = false;
            customFilterLastFilter4MenuItem.Click += CustomFilterLastFilterMenuItem_Click;
            customFilterLastFilter4MenuItem.TextChanged += CustomFilterLastFilterMenuItem_TextChanged;
            //
            // customFilterLastFilter5MenuItem
            //
            customFilterLastFilter5MenuItem.Name = "customFilterLastFilter5MenuItem";
            customFilterLastFilter5MenuItem.Size = new Size(152, 22);
            customFilterLastFilter5MenuItem.Tag = "4";
            customFilterLastFilter5MenuItem.Text = null;
            customFilterLastFilter5MenuItem.Visible = false;
            customFilterLastFilter5MenuItem.Click += CustomFilterLastFilterMenuItem_Click;
            customFilterLastFilter5MenuItem.TextChanged += CustomFilterLastFilterMenuItem_TextChanged;
            //
            // toolStripMenuItem3
            //
            toolStripSeparator3MenuItem.Name = "toolStripSeparator3MenuItem";
            toolStripSeparator3MenuItem.Size = new Size(Width - 4, 6);
            //
            // filterSelectionListPanel
            //
            filterSelectionListPanel.Name = "filterSelectionListPanel";
            filterSelectionListPanel.Size = new Size(Width - 1, 200);
            filterSelectionListPanel.FilterSelected += FilterSelectionListPanel_FilterSelected;
            filterSelectionListPanel.FilterSelectionCancelled += FilterSelectionListPanel_FilterSelectionCancelled;
            //
            // sortFilterMenuStripResizer
            //
            sortFilterMenuStripResizer.Name = "sortFilterMenuStripResizer";
            sortFilterMenuStripResizer.Size = new Size(10, 10);
            sortFilterMenuStripResizer.ResizeMenu += SortFilterMenuStripResizer_ResizeMenu;
            //
            //
            //
            this.ResumeLayout(false);
            this.PerformLayout();
        }


        #endregion

        #region // Private/protected data fields
        // Type of data in ADGV column that uses this menu
        private readonly Type _dataType;


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



        #region // Inner ContextMenu Events 

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

        private void SortAscMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SortDescMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CancelSortMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void ContextMenuItem_MouseEnter(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                if (item.Enabled) 
                    item.Select();
            }
        }


        #endregion



        #region // Filter actions

        private void CancelFilterMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CustomFilterLastFiltersListMenuItem_Paint(object sender, PaintEventArgs e)
        {
            throw new NotImplementedException();
        }
        private void CustomFilterMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void CustomFilterLastFilter1MenuItem_VisibleChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
        private void CustomFilterLastFilterMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CustomFilterLastFilterMenuItem_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FilterSelectionListPanel_FilterSelected(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void FilterSelectionListPanel_FilterSelectionCancelled(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region // Resize menu

        private void SortFilterMenuStripResizer_ResizeMenu(object sender, SortFilterMenuStripResizeEventArgs re)
        {
            filterSelectionListPanel.Size = 
                new Size(filterSelectionListPanel.Width + re.WeightChange, filterSelectionListPanel.Height + re.HeightChange);
        }

        #endregion 
    }
}
