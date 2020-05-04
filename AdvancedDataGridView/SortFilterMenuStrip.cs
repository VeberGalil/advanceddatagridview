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
using System.ComponentModel;
using System.Diagnostics;

namespace Zuby.ADGV
{
    internal class SortFilterMenuStrip : ContextMenuStrip, ISortFilterMenu
    {

        #region // Constructor 
        /// <summary>
        /// Public constructor
        /// </summary>
        /// <param name="dataType">Type of data in ADGV column that uses this menu</param>
        public SortFilterMenuStrip(Type dataType) : base()
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
            }
            else if (_dataType == typeof(bool))
            {
                sortAscMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortBoolAsc.ToString()];
                sortDescMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortBoolDesc.ToString()];
                sortAscMenuItem.Image = Properties.Resources.MenuStrip_OrderASCbool;
                sortDescMenuItem.Image = Properties.Resources.MenuStrip_OrderDESCbool;
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
            }
            else
            {
                sortAscMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortTextAsc.ToString()];
                sortDescMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVSortTextDesc.ToString()];
                sortAscMenuItem.Image = Properties.Resources.MenuStrip_OrderASCtxt;
                sortDescMenuItem.Image = Properties.Resources.MenuStrip_OrderDESCtxt;
            }
            // Last custom filters
            customFilterLastFiltersListMenuItem.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVCustomFilter.ToString()];
            customFilterLastFiltersListMenuItem.Enabled = _dataType != typeof(bool);
            customFilterLastFiltersListMenuItem.Checked = (this.ActiveFilterType == FilterType.Custom);
            // Enable/disable context search in filter list
            if (_dataType == typeof(DateTime) || _dataType == typeof(TimeSpan) || _dataType == typeof(bool))
                filterSelectionListPanel.FilterContextSearchEnabled = false;
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
            this.MinimumSize = new Size(265, 330);
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
            sortAscMenuItem.Size = new Size(286, 22);
            sortAscMenuItem.Click += SortAscMenuItem_Click;
            sortAscMenuItem.MouseEnter += ContextMenuItem_MouseEnter;
            sortAscMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            //
            // sortDESCMenuItem
            //
            sortDescMenuItem.Name = "sortDESCMenuItem";
            sortDescMenuItem.Size = new Size(286, 22);
            sortDescMenuItem.Click += SortDescMenuItem_Click;
            sortDescMenuItem.MouseEnter += ContextMenuItem_MouseEnter;
            sortDescMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            //
            // cancelSortMenuItem
            //
            cancelSortMenuItem.Name = "cancelSortMenuItem";
            cancelSortMenuItem.Enabled = false;
            cancelSortMenuItem.Size = new Size(286, 22);
            cancelSortMenuItem.Click += CancelSortMenuItem_Click;
            cancelSortMenuItem.MouseEnter += ContextMenuItem_MouseEnter;
            //
            // toolStripSeparator1MenuItem
            //
            this.toolStripSeparator1MenuItem.Name = "toolStripSeparator1MenuItem";
            this.toolStripSeparator1MenuItem.Size = new Size(283, 6);
            //
            // cancelFilterMenuItem
            //
            cancelFilterMenuItem.Name = "cancelFilterMenuItem";
            cancelFilterMenuItem.Enabled = false;
            cancelFilterMenuItem.Size = new Size(286, 22);
            cancelFilterMenuItem.Click += CancelFilterMenuItem_Click;
            cancelFilterMenuItem.MouseEnter += ContextMenuItem_MouseEnter;
            //
            // customFilterLastFiltersListMenuItem
            //
            customFilterLastFiltersListMenuItem.Name = "customFilterLastFiltersListMenuItem";
            customFilterLastFiltersListMenuItem.Size = new Size(286, 22);
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
            toolStripSeparator3MenuItem.Size = new Size(283, 6);
            //
            // filterSelectionListPanel
            //
            filterSelectionListPanel.Name = "filterSelectionListPanel";
            filterSelectionListPanel.Size = new Size(286, 200);
            filterSelectionListPanel.FilterSelected += FilterSelectionListPanel_FilterSelected;
            filterSelectionListPanel.MouseLeave += FilterSelectionListPanel_MouseLeave;
            //
            // sortFilterMenuStripResizer
            //
            sortFilterMenuStripResizer.Name = "sortFilterMenuStripResizer";
            sortFilterMenuStripResizer.Margin = new Padding(242, 0, 0, 0);
            sortFilterMenuStripResizer.ResizeMenu += SortFilterMenuStripResizer_ResizeMenu;
            //
            //
            //
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void FilterSelectionListPanel_MouseLeave(object sender, EventArgs e)
        {
            Focus();
        }

        #endregion

        #region // Private/protected data fields

        // Type of data in ADGV column that uses this menu
        private readonly Type _dataType;
        // Selected sort order for hosting column in form of DataSet ORDER BY string 
        private string _sortString = string.Empty;
        // Accumulated filter for hosting column in form of DataSet WHERE string
        private string _filterString = string.Empty;

        #endregion

        #region // Public properties
        /// <summary>
        /// Get current SortType of SortFilterMenuStrip
        /// </summary>
        public SortType ActiveSortType { get; private set; } = SortType.None;

        /// <summary>
        /// Get selected sort order for hosting column in form of ORDER BY string 
        /// </summary>
        public string SortString
        {
            get => _sortString;
            private set
            {
                _sortString = String.IsNullOrWhiteSpace(value) ? string.Empty : value;
                cancelSortMenuItem.Enabled = _sortString.Length > 0;
            }
        }

        /// <summary>
        /// Are sorting capabilities enabled in menu
        /// </summary>
        public bool IsSortEnabled { get; set; } = true;

        /// <summary>
        /// Get current FilterType of SortFilterMenuStrip
        /// </summary>
        public FilterType ActiveFilterType { get; private set; } = FilterType.None;

        /// <summary>
        /// Get accumulated filter for hosting column in form of WHERE string
        /// </summary>
        public string FilterString
        {
            get => _filterString;
            private set
            {
                _filterString = String.IsNullOrWhiteSpace(value) ? string.Empty : value;
                cancelFilterMenuItem.Enabled = _filterString.Length > 0;
            }
        }

        /// <summary>
        /// Are filter capabilities enabled in menu
        /// </summary>
        public bool IsFilterEnabled { get; set; } = true;

        /// <summary>
        /// Is filter value checklist enabled
        /// </summary>
        public bool IsFilterChecklistEnabled 
        {
            get => filterSelectionListPanel.FilterChecklistEnabled;
            set => filterSelectionListPanel.FilterChecklistEnabled = value;
        } 

        /// <summary>
        /// Are custom filers enabled in menu
        /// </summary>
        public bool IsFilterCustomEnabled { get; set; } = false;

        /// <summary>
        /// Will date/time values appear in checklist as year-month-day-hour-minute-second hierarchy
        /// </summary>
        public bool IsFilterDateAndTimeEnabled 
        {
            get => filterSelectionListPanel.FilterDateAndTimeEnabled;
            set => filterSelectionListPanel.FilterDateAndTimeEnabled = value;
        } 

        /// <summary>
        /// Is filter selection exclusive (i.e., selected items are filtered out, like DataSet WHERE NOT IN)
        /// </summary>
        public bool IsFilterNOTINLogicEnabled 
        {
            get => filterSelectionListPanel.IsFilterNOTINLogicEnabled;
            set => filterSelectionListPanel.IsFilterNOTINLogicEnabled = value;
        }

        /// <summary>
        /// If true, context search in filter hides unmatched items
        /// </summary>
        public bool DoesTextFilterRemoveNodesOnSearch 
        {
            get => filterSelectionListPanel.DoesTextFilterRemoveNodesOnSearch;
            set => filterSelectionListPanel.DoesTextFilterRemoveNodesOnSearch = value;
        }
        #endregion

        #region // Public events
        public event EventHandler FilterChanged;
        public event EventHandler SortChanged;
        #endregion

        #region // Public Filter and Sort enable/disable methods

        /// <summary>
        /// Enabled or disable Sorting capabilities
        /// </summary>
        /// <param name="enabled"></param>
        public void SetSortEnabled(bool enabled)
        {
            if (!IsSortEnabled)
                enabled = false;
            sortAscMenuItem.Enabled = enabled;
            sortDescMenuItem.Enabled = enabled;
            cancelSortMenuItem.Enabled = enabled;
        }

        /// <summary>
        /// Enable or disable Filter capabilities
        /// </summary>
        /// <param name="enabled"></param>
        public void SetFilterEnabled(bool enabled)
        {
            if (!IsFilterEnabled)
                enabled = false;

            this.cancelFilterMenuItem.Enabled = enabled;
            if (enabled)
                customFilterLastFiltersListMenuItem.Enabled = _dataType != typeof(bool);
            else
                customFilterLastFiltersListMenuItem.Enabled = false;
            filterSelectionListPanel.SetFilterEnabled(enabled);
        }

        /// <summary>
        /// Enable or disable Filter value checkList 
        /// </summary>
        /// <param name="enabled"></param>
        public void SetFilterChecklistEnabled(bool enabled)
        {
            if (!IsFilterEnabled)
                enabled = false;
            // enable/disable checklist controls
            filterSelectionListPanel.SetFilterChecklistEnabled(enabled);
        }

        /// <summary>
        /// Enable or disable Filter custom capabilities
        /// </summary>
        /// <param name="enabled"></param>
        public void SetFilterCustomEnabled(bool enabled)
        {
            if (!IsFilterEnabled)
                enabled = false;

            IsFilterCustomEnabled = enabled;
            customFilterMenuItem.Enabled = enabled;

            if (!enabled)
            {
                UnCheckCustomFilters();
            }
        }

        #endregion

        #region // Set preloaded mode
        /// <summary>
        /// Enable or disable preloaded filter mode
        /// </summary>
        /// <param name="enabled"></param>
        public void SetLoadedMode(bool enabled)
        {
            customFilterMenuItem.Enabled = !enabled;
            cancelFilterMenuItem.Enabled = enabled;
            if (enabled)
            {
                ActiveFilterType = FilterType.Loaded;
                //
                _sortString = string.Empty;
                _filterString = string.Empty;
                // Clear custom filters
                customFilterLastFiltersListMenuItem.Checked = false;
                for (int i = 2; i < customFilterLastFiltersListMenuItem.DropDownItems.Count - 1; i++)
                {
                    (customFilterLastFiltersListMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked = false;
                }
                //
                filterSelectionListPanel.SetLoadedMode();
                //
                SetSortEnabled(false);
                SetFilterEnabled(false);
            }
            else
            {
                ActiveFilterType = FilterType.None;
                //
                SetSortEnabled(true);
                SetFilterEnabled(true);
            }
        }
        #endregion

        #region // Show menu

        /// <summary>
        /// Show menu with or without restoring previous filter selection
        /// </summary>
        /// <param name="control"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="restoreFilter"></param>
        public void Show(Control control, int x, int y, bool restoreFilter)
        {
            filterSelectionListPanel.RestoreFilter(restoreFilter);
            base.Show(control, x, y);
        }

        /// <summary>
        /// Show the menu, populating filter checklist with column values
        /// </summary>
        /// <param name="control"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="vals"></param>
        public void Show(Control control, int x, int y, IEnumerable<DataGridViewCell> vals)
        {
            filterSelectionListPanel.LoadChecklist(vals, _dataType, this.ActiveFilterType);
            base.Show(control, x, y);
        }

        #endregion

        #region // Public Sort methods

        /// <summary>
        /// Clean the Sorting
        /// </summary>
        public void CleanSort()
        {
            sortAscMenuItem.Checked = false;
            sortDescMenuItem.Checked = false;
            this.ActiveSortType = SortType.None;
            this.SortString = string.Empty;
        }

        /// <summary>
        /// Sort ASC
        /// </summary>
        public void SortAsc()
        {
            SortAscMenuItem_Click(this, new EventArgs());
        }

        /// <summary>
        /// Sort DESC
        /// </summary>
        public void SortDesc()
        {
            SortDescMenuItem_Click(this, new EventArgs());
        }

        #endregion

        #region // Public Filter methods
        /// <summary>
        /// Clean the Filter
        /// </summary>
        public void CleanFilter()
        {
            // Reset current filter type and filter string
            ActiveFilterType = FilterType.None;
            FilterString = string.Empty;

            // Clean custom filters
            for (int i = 2; i < customFilterLastFiltersListMenuItem.DropDownItems.Count - 1; i++)
            {
                (customFilterLastFiltersListMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked = false;
            }
            customFilterLastFiltersListMenuItem.Checked = false;

            // Clean filter in checklist
            filterSelectionListPanel.CleanFilter();
        }

        /// <summary>
        /// Set the text filter on checklist remove node mode
        /// </summary>
        /// <param name="enabled"></param>
        public void SetChecklistTextFilterRemoveNodesOnSearchMode(bool enabled)
        {
            if (DoesTextFilterRemoveNodesOnSearch != enabled)
            {
                DoesTextFilterRemoveNodesOnSearch = enabled;
                CleanFilter();
            }
        }

        /// <summary>
        /// Load predefined filter
        /// </summary>
        /// <param name="filter"></param>
        public void LoadFilter(IEnumerable<DataGridViewCell> valueCells, string filter)
        {
            throw new NotImplementedException();
            // decide whether use custom filter of mark in checklist
            // note: ADGV is not designed to handle both


            // Something like:
            //filterSelectionListPanel.LoadChecklist(valueCells, _dataType, this.ActiveFilterType);

        }
        #endregion

        #region // Inner ContextMenu Events 

        protected override void OnOpening(CancelEventArgs e)
        {
            if (RightToLeft == RightToLeft.Yes)
            {
                sortFilterMenuStripResizer.Margin = new Padding(Width - 46, 1, 0, 0);
            }
            else
            {
                sortFilterMenuStripResizer.Margin = new Padding(Width - 45, 0, 0, 0);
            }
            base.OnOpening(e);
        }

        protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
        {
            base.OnClosed(e);
            // Cancel resize, if in progress
            sortFilterMenuStripResizer.CancelResize();
            // Clear filter data
            filterSelectionListPanel.CancelFilter();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (!ContainsFocus)
                Close();
        }

        private void ContextMenuItem_MouseEnter(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem item)
            {
                if (item.Enabled)
                    item.Select();
            }
        }

        // Show / hide separator after Add Custom Filter menu item
        private void CustomFilterLastFilter1MenuItem_VisibleChanged(object sender, EventArgs e)
        {
            toolStripSeparator2MenuItem.Visible = !customFilterLastFilter1MenuItem.Visible;
            (sender as ToolStripMenuItem).VisibleChanged -= CustomFilterLastFilter1MenuItem_VisibleChanged;
        }

        // Make custom filter menu item available
        private void CustomFilterLastFilterMenuItem_TextChanged(object sender, EventArgs e)
        {
            (sender as ToolStripMenuItem).Available = true;
            (sender as ToolStripMenuItem).TextChanged -= CustomFilterLastFilterMenuItem_TextChanged;
        }

        #endregion

        #region // Sort actions
        /// <summary>
        /// 'Sort ASC' click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortAscMenuItem_Click(object sender, EventArgs e)
        {
            // Ignore image nodes
            if (_dataType == typeof(Bitmap))
                return;

            sortAscMenuItem.Checked = true;
            sortDescMenuItem.Checked = false;
            this.ActiveSortType = SortType.Asc;

            // Get Sort string
            string oldsort = SortString;
            this.SortString = "[{0}] ASC";

            // Fire SortChanged event
            if (oldsort != SortString && SortChanged != null)
                SortChanged(this, new EventArgs());
        }

        /// <summary>
        /// 'Sort DESC' click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SortDescMenuItem_Click(object sender, EventArgs e)
        {
            // ignore image nodes
            if (_dataType == typeof(Bitmap))
                return;

            sortAscMenuItem.Checked = false;
            sortDescMenuItem.Checked = true;
            this.ActiveSortType = SortType.Desc;

            // Get sort String
            string oldsort = SortString;
            this.SortString = "[{0}] DESC";

            // Fire SortChanged event
            if (oldsort != SortString && SortChanged != null)
                SortChanged(this, new EventArgs());
        }

        /// <summary>
        /// Cancel Sort event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelSortMenuItem_Click(object sender, EventArgs e)
        {
            string oldsort = SortString;
            //clean Sort
            CleanSort();
            //fire Sort changed
            if (oldsort != SortString && SortChanged != null)
                SortChanged(this, new EventArgs());
        }

        #endregion

        #region // Filter actions

        /// <summary>
        /// Cancel Filter event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelFilterMenuItem_Click(object sender, EventArgs e)
        {
            string oldfilter = FilterString;

            //clean Filter
            CleanFilter();

            //fire Filter changed
            if (oldfilter != FilterString && FilterChanged != null)
                FilterChanged(this, new EventArgs());
        }


        /// <summary>
        /// Add Custom Filter event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomFilterMenuItem_Click(object sender, EventArgs e)
        {
            //ignore image nodes
            if (_dataType == typeof(Bitmap))
                return;

            //open a new Custom filter window
            FormCustomFilter flt = new FormCustomFilter(_dataType, IsFilterDateAndTimeEnabled)
            {
                RightToLeft = this.RightToLeft,
                RightToLeftLayout = (this.RightToLeft == RightToLeft.Yes)
            };

            if (flt.ShowDialog() == DialogResult.OK)
            {
                //add the new Filter presets

                string filterString = flt.FilterString;
                string viewFilterString = flt.FilterStringDescription;

                int index = -1;

                for (int i = 2; i < customFilterLastFiltersListMenuItem.DropDownItems.Count; i++)
                {
                    if (customFilterLastFiltersListMenuItem.DropDown.Items[i].Available)
                    {
                        if (customFilterLastFiltersListMenuItem.DropDownItems[i].Text == viewFilterString && 
                            customFilterLastFiltersListMenuItem.DropDownItems[i].Tag.ToString() == filterString)
                        {
                            index = i;
                            break;
                        }
                    }
                    else
                        break;
                }

                if (index < 2)
                {
                    for (int i = customFilterLastFiltersListMenuItem.DropDownItems.Count - 2; i > 1; i--)
                    {
                        if (customFilterLastFiltersListMenuItem.DropDownItems[i].Available)
                        {
                            customFilterLastFiltersListMenuItem.DropDownItems[i + 1].Text = customFilterLastFiltersListMenuItem.DropDownItems[i].Text;
                            customFilterLastFiltersListMenuItem.DropDownItems[i + 1].Tag = customFilterLastFiltersListMenuItem.DropDownItems[i].Tag;
                        }
                    }
                    index = 2;

                    customFilterLastFiltersListMenuItem.DropDownItems[2].Text = viewFilterString;
                    customFilterLastFiltersListMenuItem.DropDownItems[2].Tag = filterString;
                }

                // Set custom filter
                SetCustomFilter(index);
            }
        }

        /// <summary>
        /// Custom Filter selection event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomFilterLastFilterMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuitem = sender as ToolStripMenuItem;

            for (int i = 2; i < customFilterLastFiltersListMenuItem.DropDownItems.Count; i++)
            {
                if (customFilterLastFiltersListMenuItem.DropDownItems[i].Text == menuitem.Text && customFilterLastFiltersListMenuItem.DropDownItems[i].Tag.ToString() == menuitem.Tag.ToString())
                {
                    //set current filter preset as active
                    SetCustomFilter(i);
                    break;
                }
            }
        }

        private void FilterSelectionListPanel_FilterSelected(object sender, ChecklistFilterSelectedEventArgs e)
        {
            UnCheckCustomFilters();
            customFilterLastFiltersListMenuItem.Checked = false;
            if (e.CancelCustomFilter)
            {
                CancelFilterMenuItem_Click(filterSelectionListPanel, new EventArgs());
            }
            else
            {
                ActiveFilterType = FilterType.CheckList;
                //
                if (e.CustomFilter != this.FilterString)
                {
                    this.FilterString = e.CustomFilter;
                    FilterChanged?.Invoke(this, new EventArgs());
                }
            }
        }

        #endregion


        #region // Private filter methods
        /// <summary>
        /// UnCheck all Custom Filter presets
        /// </summary>
        private void UnCheckCustomFilters()
        {
            for (int i = 2; i < customFilterLastFiltersListMenuItem.DropDownItems.Count; i++)
            {
                (customFilterLastFiltersListMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked = false;
            }
        }

        /// <summary>
        /// Set Custom Filter
        /// </summary>
        /// <param name="filtersMenuItemIndex"></param>
        private void SetCustomFilter(int filtersMenuItemIndex)
        {
            string filterstring = customFilterLastFiltersListMenuItem.DropDownItems[filtersMenuItemIndex].Tag.ToString();
            string viewfilterstring = customFilterLastFiltersListMenuItem.DropDownItems[filtersMenuItemIndex].Text;
            // Do preset jobs
            if (filtersMenuItemIndex != 2)
            {
                for (int i = filtersMenuItemIndex; i > 2; i--)
                {
                    customFilterLastFiltersListMenuItem.DropDownItems[i].Text = customFilterLastFiltersListMenuItem.DropDownItems[i - 1].Text;
                    customFilterLastFiltersListMenuItem.DropDownItems[i].Tag = customFilterLastFiltersListMenuItem.DropDownItems[i - 1].Tag;
                }

                customFilterLastFiltersListMenuItem.DropDownItems[2].Text = viewfilterstring;
                customFilterLastFiltersListMenuItem.DropDownItems[2].Tag = filterstring;
            }

            //uncheck other preset
            for (int i = 3; i < customFilterLastFiltersListMenuItem.DropDownItems.Count; i++)
            {
                (customFilterLastFiltersListMenuItem.DropDownItems[i] as ToolStripMenuItem).Checked = false;
            }

            (customFilterLastFiltersListMenuItem.DropDownItems[2] as ToolStripMenuItem).Checked = true;
            this.ActiveFilterType = FilterType.Custom;

            //get Filter string
            string oldfilter = FilterString;
            this.FilterString = filterstring;

            customFilterLastFiltersListMenuItem.Checked = true;

            //
            filterSelectionListPanel.SetCustomFilterMode();

            //fire Filter changed
            if (oldfilter != FilterString && FilterChanged != null)
                FilterChanged(this, new EventArgs());
        }


        #endregion


        #region // Resize menu

        private void SortFilterMenuStripResizer_ResizeMenu(object sender, SortFilterMenuStripResizeEventArgs re)
        {
            // Resize
            filterSelectionListPanel.Size = 
                new Size(filterSelectionListPanel.Width + re.WidthChange, filterSelectionListPanel.Height + re.HeightChange);
            // Move if RTL
            if (RightToLeft == RightToLeft.Yes)
            {
                int newX = Bounds.Location.X - re.WidthChange;
                base.Show(new Point(newX, Bounds.Location.Y));
            }
            Debug.WriteLine($"Menu size = {Size}, panel size = {filterSelectionListPanel.Size}");

        }

        #endregion
    }
}
