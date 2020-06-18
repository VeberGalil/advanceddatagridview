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
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using System.Xml;
using System.Text.RegularExpressions;

namespace Zuby.ADGV
{
    internal class ChecklistFilterSelectedEventArgs: EventArgs
    {
        public bool CancelCustomFilter { get; private set; }
        public string CustomFilter { get; private set; }

        public ChecklistFilterSelectedEventArgs(bool cancel, string filter)
        {
            this.CancelCustomFilter = cancel;
            this.CustomFilter = filter;
        }
    }

    internal delegate void CustomFilterSelectedEventHandler(object sender, ChecklistFilterSelectedEventArgs ea);

    internal class FilterSelectionListPanel : ToolStripControlHost
    {
        #region // Constructor 
        /// <summary>
        /// Public constructor
        /// </summary>
        public FilterSelectionListPanel() : base(new Panel())
        {
            InitializeComponent();
            // Set translations
            btnFilter.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVButtonFilter.ToString()];
            btnCancel.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVButtonUndofilter.ToString()];

        }

        /// <summary>
        /// Hosted control
        /// </summary>
        protected Panel SelectionListPanel
        {
            get => Control as Panel;
        }

        /// <summary>
        /// UI elements
        /// </summary>
        private TableLayoutPanel tableControls;
        private Button btnFilter;
        private Button btnCancel;
        private Panel panelSelector;
        private TreeView treeFilterSelection;
        private TextBox textFilterSelection;

        /// <summary>
        /// Standard UI builder
        /// </summary>
        private void InitializeComponent()
        {
            this.tableControls = new TableLayoutPanel();
            this.btnFilter = new Button();
            this.btnCancel = new Button();
            this.panelSelector = new Panel();
            this.textFilterSelection = new TextBox();
            this.treeFilterSelection = new TreeView();
            this.SelectionListPanel.SuspendLayout();
            this.tableControls.SuspendLayout();
            this.panelSelector.SuspendLayout();

            // 
            // tableControls
            // 
            tableControls.Name = "tableControls";
            tableControls.Location = new Point(0, 170);
            tableControls.Size = new Size(200, 30);
            tableControls.Dock = DockStyle.Bottom;
            tableControls.RowCount = 1;
            tableControls.ColumnCount = 3;
            tableControls.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableControls.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableControls.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tableControls.Controls.Add(btnFilter, 1, 0);
            tableControls.Controls.Add(btnCancel, 2, 0);
            tableControls.TabIndex = 0;
            // 
            // btnFilter
            // 
            btnFilter.Name = "btnFilter";
            btnFilter.Text = "Filter";
            btnFilter.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Right);
            btnFilter.Location = new Point(43, 3);
            btnFilter.Size = new Size(75, 23);
            btnFilter.TabIndex = 0;
            btnFilter.UseVisualStyleBackColor = true;
            btnFilter.Click += BtnFilter_Click;
            // 
            // btnCancel
            // 
            btnCancel.Name = "btnCancel";
            btnCancel.Text = "Cancel";
            btnCancel.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Right);
            btnCancel.Location = new Point(123, 3);
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 1;
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += BtnCancel_Click;

            // 
            // panelSelector
            // 
            panelSelector.Name = "panelSelector";
            panelSelector.Dock = DockStyle.Fill;
            panelSelector.Location = new Point(0, 0);
            panelSelector.Size = new Size(200, 170);
            panelSelector.Controls.Add(this.treeFilterSelection);
            panelSelector.Controls.Add(this.textFilterSelection);
            panelSelector.TabIndex = 1;
            // 
            // textFilterSelection
            // 
            textFilterSelection.Name = "textFilterSelection";
            textFilterSelection.Anchor = (AnchorStyles)((AnchorStyles.Top | AnchorStyles.Left) | AnchorStyles.Right);
            textFilterSelection.Location = new Point(3, 3);
            textFilterSelection.Size = new Size(194, 22);
            textFilterSelection.TabIndex = 0;
            textFilterSelection.TextChanged += TextFilterSelection_TextChanged;
            //
            // treeFilterSelection
            //
            treeFilterSelection.Name = "treeFilterSelection";
            treeFilterSelection.StateImageList = TreeNodeStateImages.GetCheckListStateImages();
            treeFilterSelection.CheckBoxes = false;
            treeFilterSelection.Anchor = (AnchorStyles)(((AnchorStyles.Top | AnchorStyles.Bottom) | AnchorStyles.Left) | AnchorStyles.Right);
            treeFilterSelection.Location = new Point(3, 31);
            treeFilterSelection.Size = new Size(194, 139);
            treeFilterSelection.TabIndex = 1;
            treeFilterSelection.KeyDown += TreeFilterSelection_KeyDown;
            treeFilterSelection.MouseEnter += TreeFilterSelection_MouseEnter;
            treeFilterSelection.NodeMouseClick += TreeFilterSelection_NodeMouseClick;
            treeFilterSelection.NodeMouseDoubleClick += TreeFilterSelection_NodeMouseDoubleClick;
            //
            // SelectionListPanel
            //
            this.SelectionListPanel.Controls.Add(this.panelSelector);
            this.SelectionListPanel.Controls.Add(this.tableControls);
            this.SelectionListPanel.Dock = DockStyle.Fill;
            this.SelectionListPanel.MinimumSize = new Size(200, 200);
            this.SelectionListPanel.BackColor = Color.Transparent;
            this.SelectionListPanel.Size = new Size(200, 200);
            //
            // 
            //
            this.AutoSize = false;
            //
            this.SelectionListPanel.ResumeLayout(false);
            tableControls.ResumeLayout(false);
            panelSelector.ResumeLayout(false);
            panelSelector.PerformLayout();
        }

        #endregion

        #region // Private / protected data fields
        private Type _filterValueType = null;
        private TreeNodeItemSelector[] _startingNodes = null;
        private TreeNodeItemSelector[] _filterNodes = null;
        private TreeNodeItemSelector[] _initialNodes = new TreeNodeItemSelector[] { };
        private TreeNodeItemSelector[] _restoreNodes = new TreeNodeItemSelector[] { };
        private bool _textFilterSelectionSetByText = false;
        private bool _textFilterSelectionChangedEnabled = true;
        #endregion

        #region // Public properties
        /// <summary>
        /// Is filter value checklist enabled
        /// </summary>
        public bool FilterChecklistEnabled { get; set; } = true;

        /// <summary>
        /// Will date/time values appear in checklist as year-month-day-hour-minute-second hierarchy
        /// </summary>
        public bool FilterDateAndTimeEnabled { get; set; } = true;

        /// <summary>
        /// Enable / disable use of context search in filter data
        /// </summary>
        public bool FilterContextSearchEnabled
        {
            get => textFilterSelection.Enabled;
            set => textFilterSelection.Enabled = value;
        }

        /// <summary>
        /// Is filter selection exclusive (i.e., selected items are filtered out, like DataSet WHERE NOT IN)
        /// </summary>
        public bool IsFilterNOTINLogicEnabled { get; set; } = false;

        /// <summary>
        /// Set the text filter search nodes behaviour
        /// </summary>
        public bool DoesTextFilterRemoveNodesOnSearch { get; set; } = true;

        #endregion

        #region // Public events
        // or CancelEventHandler if event might be cancelled in menu code
        public event CustomFilterSelectedEventHandler FilterSelected;

        #endregion

        #region // Public methods
        /// <summary>
        /// Cancel filter
        /// </summary>
        public void CancelFilter()
        {
            _startingNodes = null;
            ClearFilterSelectionText();
        }

        /// <summary>
        ///  Enable or disable Filter controls
        /// </summary>
        /// <param name="enabled"></param>
        public void SetFilterEnabled(bool enabled)
        {
            textFilterSelection.Enabled = enabled;
            treeFilterSelection.Enabled = enabled;
            btnFilter.Enabled = enabled;
            btnCancel.Enabled = enabled;
        }

        /// <summary>
        /// Enable or disable Filter checkList controls
        /// </summary>
        /// <param name="enabled"></param>
        public void SetFilterChecklistEnabled(bool enabled)
        {
            this.FilterChecklistEnabled = enabled;
            //
            textFilterSelection.ReadOnly = !enabled;
            treeFilterSelection.Enabled = enabled;
            if (!enabled)
            {
                treeFilterSelection.BeginUpdate();
                treeFilterSelection.Nodes.Clear();
                TreeNodeItemSelector disablednode = 
                    TreeNodeItemSelector.CreateNode(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterChecklistDisable.ToString()] + "            ", 
                                                    null, 
                                                    CheckState.Checked, 
                                                    TreeNodeItemSelector.CustomNodeType.SelectAll,
                                                    this.RightToLeft);
                disablednode.NodeFont = new Font(treeFilterSelection.Font, FontStyle.Bold);
                treeFilterSelection.Nodes.Add(disablednode);
            }
        }

        /// <summary>
        /// Set preloaded filter mode
        /// </summary>
        public void SetLoadedMode()
        {
            _filterNodes = null;
            treeFilterSelection.Nodes.Clear();
            TreeNodeItemSelector allnode = TreeNodeItemSelector.CreateNode("(Select All)" + "            ", 
                                                                            null, 
                                                                            CheckState.Checked, 
                                                                            TreeNodeItemSelector.CustomNodeType.SelectAll,
                                                                            this.RightToLeft);
            allnode.NodeFont = new Font(treeFilterSelection.Font, FontStyle.Bold);
            allnode.CheckState = CheckState.Indeterminate;
            treeFilterSelection.Nodes.Add(allnode);
        }

        /// <summary>
        /// Populate filter checklist from column cell values
        /// </summary>
        /// <param name="valueCells">Filter values</param>
        /// <param name="valueType">Data type of filter values</param>
        /// <param name="activeFilterType">Active FilterType</param>
        public void LoadChecklist(IEnumerable<DataGridViewCell> valueCells, Type valueType, FilterType activeFilterType)
        {
            LoadChecklist(valueCells, valueType, () => {
                if (activeFilterType == FilterType.Custom)
                    SetNodesCheckState(treeFilterSelection.Nodes, false);
            });

            #region // refactoring code: moved to LoadChecklist(IEnumerable<DataGridViewCell>, Type, Action)
            //_filterValueType = valueType;
            //BuildNodes(valueCells);
            //if (DoesTextFilterRemoveNodesOnSearch && treeFilterSelection.Nodes.Count != _initialNodes.Count())
            //{
            //    _initialNodes = new TreeNodeItemSelector[treeFilterSelection.Nodes.Count];
            //    _restoreNodes = new TreeNodeItemSelector[treeFilterSelection.Nodes.Count];
            //    int i = 0;
            //    foreach (TreeNodeItemSelector n in treeFilterSelection.Nodes)
            //    {
            //        _initialNodes[i] = n.Clone();
            //        _restoreNodes[i] = n.Clone();
            //        i++;
            //    }
            //}

            //if (activeFilterType == FilterType.Custom)
            //    SetNodesCheckState(treeFilterSelection.Nodes, false);
            //DuplicateNodes();

            //ClearFilterSelectionText();
            #endregion 
        }

        /// <summary>
        /// Populate filter checklist from column cell values
        /// </summary>
        /// <param name="valueCells">Filter values</param>
        /// <param name="valueType">Data type of filter values</param>
        /// <param name="activeFilterType">Active FilterType</param>
        public void LoadChecklist(IEnumerable<DataGridViewCell> valueCells, Type valueType, string filter)
        {
            LoadChecklist(valueCells, valueType, () => {
                if (!string.IsNullOrWhiteSpace(filter))
                {   
                    // Remove all check marks
                    SetNodesCheckState(treeFilterSelection.Nodes, false);
                    // Parse filter string and mark only unfiltered nodes
                    ApplyFilter(filter);
                    // Set filter string 
                    SetCheckListFilter();
                }
            });
        }

        /// <summary>
        /// Restore previous filter selection
        /// </summary>
        /// <param name="restoreFilter"></param>
        public void RestoreFilter(bool restoreFilter)
        {
            ClearFilterSelectionText();

            if (restoreFilter)
                RestoreFilterNodes();
            DuplicateNodes();

            if (DoesTextFilterRemoveNodesOnSearch && _textFilterSelectionSetByText)
            {
                _restoreNodes = new TreeNodeItemSelector[_initialNodes.Count()];
                int i = 0;
                foreach (TreeNodeItemSelector n in _initialNodes)
                {
                    _restoreNodes[i] = n.Clone();
                    i++;
                }
                treeFilterSelection.BeginUpdate();
                treeFilterSelection.Nodes.Clear();
                foreach (TreeNodeItemSelector node in _initialNodes)
                {
                    treeFilterSelection.Nodes.Add(node);
                }
                treeFilterSelection.EndUpdate();
            }
        }

        /// <summary>
        /// Clean the Filter
        /// </summary>
        public void CleanFilter()
        {
            if (DoesTextFilterRemoveNodesOnSearch)
            {
                _initialNodes = new TreeNodeItemSelector[] { };
                _restoreNodes = new TreeNodeItemSelector[] { };
                _textFilterSelectionSetByText = false;
            }
            SetNodesCheckState(treeFilterSelection.Nodes, true);
            _filterNodes = null;
            btnFilter.Enabled = true;
        }

        /// <summary>
        /// Disable checklist filter
        /// </summary>
        public void SetCustomFilterMode()
        {
            //set CheckList nodes
            SetNodesCheckState(treeFilterSelection.Nodes, false);
            DuplicateFilterNodes();

            btnFilter.Enabled = false;
        }

        #endregion

        #region // Overrides for RTL and resizing support
        public override RightToLeft RightToLeft
        {
            get => base.RightToLeft;
            set
            {
                base.RightToLeft = value;
                treeFilterSelection.RightToLeft = value;
                treeFilterSelection.RightToLeftLayout = (value == RightToLeft.Yes);
                tableControls.RightToLeft = value;
            }
        }

        public override Size Size
        {
            get => SelectionListPanel.Size;
            set
            {
                if (SelectionListPanel != null)
                {
                    SelectionListPanel.Size = value;
                }
                base.Size = value;
            }
        }

        // To set RTL when added to owning ContextMenu
        protected override void OnOwnerChanged(EventArgs e)
        {
            base.OnOwnerChanged(e);
            RightToLeft = Owner.RightToLeft;
            // Change panel RTL layout with owning menu
            ((ContextMenuStrip)Owner).RightToLeftChanged += (s, ea) =>
            {
                RightToLeft = Owner.RightToLeft;
            };
            // Set minimum size for ContextMenuStrip, which will be used by Resizer
            Owner.MinimumSize = new Size(Math.Max(Owner.MinimumSize.Width, SelectionListPanel.MinimumSize.Width),
                                         Math.Max(Owner.MinimumSize.Height, SelectionListPanel.MinimumSize.Height));

        }
        #endregion

        #region // Control events
        /// <summary>
        /// Context search text changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextFilterSelection_TextChanged(object sender, EventArgs e)
        {
            if (!_textFilterSelectionChangedEnabled)
                return;
            _textFilterSelectionSetByText = !string.IsNullOrWhiteSpace(textFilterSelection.Text);
            // If context search hides irrelevant nodes, restore them before applying new search filter on nodes
            if (DoesTextFilterRemoveNodesOnSearch)
            {
                _startingNodes = _initialNodes;

                treeFilterSelection.BeginUpdate();  // Freeze UI update until finished with heavy update
                RestoreNodes();
            }
            // Special nodes
            TreeNodeItemSelector allnode = 
                TreeNodeItemSelector.CreateNode(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVNodeSelectAll.ToString()]   + "            ", 
                                                null, 
                                                CheckState.Checked, 
                                                TreeNodeItemSelector.CustomNodeType.SelectAll,
                                                this.RightToLeft);
            TreeNodeItemSelector nullnode = 
                TreeNodeItemSelector.CreateNode(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVNodeSelectEmpty.ToString()] + "               ", 
                                                null, 
                                                CheckState.Checked, 
                                                TreeNodeItemSelector.CustomNodeType.SelectEmpty,
                                                this.RightToLeft);
            // Check relevant nodes
            for (int i = treeFilterSelection.Nodes.Count - 1; i >= 0; i--)
            {
                TreeNodeItemSelector node = treeFilterSelection.Nodes[i] as TreeNodeItemSelector;
                if (node.Text == allnode.Text)
                {
                    node.CheckState = CheckState.Indeterminate;
                }
                else if (node.Text == nullnode.Text)
                {
                    node.CheckState = CheckState.Unchecked;
                }
                else
                {
                    node.Checked = !node.Text.ToLower().Contains(textFilterSelection.Text.ToLower());
                    NodeCheckChange(node as TreeNodeItemSelector);
                }
            }
            // If context search hides irrelevant nodes, 
            if (DoesTextFilterRemoveNodesOnSearch)
            {
                // Update initial nodes check state 
                foreach (TreeNodeItemSelector node in _initialNodes)
                {
                    if (node.Text == allnode.Text)
                    {
                        node.CheckState = CheckState.Indeterminate;
                    }
                    else if (node.Text == nullnode.Text)
                    {
                        node.CheckState = CheckState.Unchecked;
                    }
                    else
                    {
                        node.CheckState = node.Text.ToLower().Contains(textFilterSelection.Text.ToLower()) ? CheckState.Checked : CheckState.Unchecked;
                    }
                }
                // Remove irrelevant nodes from UI
                for (int i = treeFilterSelection.Nodes.Count - 1; i >= 0; i--)
                {
                    TreeNodeItemSelector node = treeFilterSelection.Nodes[i] as TreeNodeItemSelector;
                    if (!(node.Text == allnode.Text || node.Text == nullnode.Text))
                    {
                        if (!node.Text.ToLower().Contains(textFilterSelection.Text.ToLower()))
                        {
                            node.Remove();
                        }
                    }
                }
                // Resume UI
                treeFilterSelection.EndUpdate();
            }
        }

        /// <summary>
        /// Node 'checkbox' icon clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeFilterSelection_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeViewHitTestInfo hitTestInfo = treeFilterSelection.HitTest(e.X, e.Y);
            if (hitTestInfo != null && hitTestInfo.Location == TreeViewHitTestLocations.StateImage)
            {   //check the node check status
                NodeCheckChange(e.Node as TreeNodeItemSelector);
            }
        }

        /// <summary>
        /// Double click on tree node = filter by single value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeFilterSelection_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNodeItemSelector node = e.Node as TreeNodeItemSelector;
            // Clear selection from all nodes, but double-clicked
            SetNodesCheckState(treeFilterSelection.Nodes, false);
            node.CheckState = CheckState.Unchecked;
            NodeCheckChange(node);
            // 'Press' Filter button
            BtnFilter_Click(this, new EventArgs());
        }

        /// <summary>
        /// Space key pressed on tree = change checked status of selected node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeFilterSelection_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
                NodeCheckChange(treeFilterSelection.SelectedNode as TreeNodeItemSelector);
        }

        private void TreeFilterSelection_MouseEnter(object sender, EventArgs e)
        {
            treeFilterSelection.Focus();
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            SetCheckListFilter();
            ((ContextMenuStrip)Owner).Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            bool restoredByFilter = false;
            if (DoesTextFilterRemoveNodesOnSearch && _textFilterSelectionSetByText)
            {
                _initialNodes = new TreeNodeItemSelector[_restoreNodes.Count()];
                int i = 0;
                foreach (TreeNodeItemSelector n in _restoreNodes)
                {
                    _initialNodes[i] = n.Clone();
                    i++;
                }

                restoredByFilter = true;
                treeFilterSelection.BeginUpdate();
                treeFilterSelection.Nodes.Clear();
                foreach (TreeNodeItemSelector node in _restoreNodes)
                {
                    treeFilterSelection.Nodes.Add(node);
                }
                treeFilterSelection.EndUpdate();
            }

            if (!restoredByFilter)
                RestoreNodes();
            ((ContextMenuStrip)Owner).Close();
        }
        #endregion

        #region // Supporting methods
        /// <summary>
        /// Clear context search text
        /// </summary>
        private void ClearFilterSelectionText()
        {
            _textFilterSelectionChangedEnabled = false;
            textFilterSelection.Text = "";
            _textFilterSelectionChangedEnabled = true;
        }

        /// <summary>
        /// Add nodes to checkList
        /// </summary>
        /// <param name="valueCells"></param>
        /// <param name="valueType"></param>
        private void BuildNodes(IEnumerable<DataGridViewCell> valueCells)
        {
            if (!this.FilterChecklistEnabled)
                return;

            treeFilterSelection.BeginUpdate();
            treeFilterSelection.Nodes.Clear();

            if (valueCells != null)
            {
                //add select all node
                TreeNodeItemSelector allnode = 
                    TreeNodeItemSelector.CreateNode(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVNodeSelectAll.ToString()] + "            ", 
                                                    null, 
                                                    CheckState.Checked, 
                                                    TreeNodeItemSelector.CustomNodeType.SelectAll,
                                                    this.RightToLeft);
                allnode.NodeFont = new Font(treeFilterSelection.Font, FontStyle.Bold);
                treeFilterSelection.Nodes.Add(allnode);

                if (valueCells.Count() > 0)
                {
                    var nonulls = valueCells.Where<DataGridViewCell>(c => c.Value != null && c.Value != DBNull.Value);

                    //add select empty node
                    if (valueCells.Count() != nonulls.Count())
                    {
                        TreeNodeItemSelector nullnode = 
                            TreeNodeItemSelector.CreateNode(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVNodeSelectEmpty.ToString()] + "               ", 
                                                            null, 
                                                            CheckState.Checked, 
                                                            TreeNodeItemSelector.CustomNodeType.SelectEmpty,
                                                            this.RightToLeft);
                        nullnode.NodeFont = new Font(treeFilterSelection.Font, FontStyle.Bold);
                        treeFilterSelection.Nodes.Add(nullnode);
                    }

                    //add datetime nodes
                    if (_filterValueType == typeof(DateTime))
                    {
                        var years =
                            from year in nonulls
                            group year by ((DateTime)year.Value).Year into cy
                            orderby cy.Key ascending
                            select cy;

                        foreach (var year in years)
                        {
                            TreeNodeItemSelector yearnode = TreeNodeItemSelector.CreateNode(year.Key.ToString(), 
                                                                                            year.Key, 
                                                                                            CheckState.Checked, 
                                                                                            TreeNodeItemSelector.CustomNodeType.DateTimeNode,
                                                                                            this.RightToLeft);
                            treeFilterSelection.Nodes.Add(yearnode);

                            var months =
                                from month in year
                                group month by ((DateTime)month.Value).Month into cm
                                orderby cm.Key ascending
                                select cm;

                            foreach (var month in months)
                            {
                                TreeNodeItemSelector monthnode = yearnode.CreateChildNode(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month.Key), month.Key);

                                var days =
                                    from day in month
                                    group day by ((DateTime)day.Value).Day into cd
                                    orderby cd.Key ascending
                                    select cd;

                                foreach (var day in days)
                                {
                                    TreeNodeItemSelector daysnode;

                                    if (!this.FilterDateAndTimeEnabled)
                                        daysnode = monthnode.CreateChildNode(day.Key.ToString("D2"), day.First().Value);
                                    else
                                    {
                                        daysnode = monthnode.CreateChildNode(day.Key.ToString("D2"), day.Key);

                                        var hours =
                                            from hour in day
                                            group hour by ((DateTime)hour.Value).Hour into ch
                                            orderby ch.Key ascending
                                            select ch;

                                        foreach (var hour in hours)
                                        {
                                            TreeNodeItemSelector hoursnode = daysnode.CreateChildNode(hour.Key.ToString("D2") + " " + "h", hour.Key);

                                            var mins =
                                                from min in hour
                                                group min by ((DateTime)min.Value).Minute into cmin
                                                orderby cmin.Key ascending
                                                select cmin;

                                            foreach (var min in mins)
                                            {
                                                TreeNodeItemSelector minsnode = hoursnode.CreateChildNode(min.Key.ToString("D2") + " " + "m", min.Key);

                                                var secs =
                                                    from sec in min
                                                    group sec by ((DateTime)sec.Value).Second into cs
                                                    orderby cs.Key ascending
                                                    select cs;

                                                foreach (var sec in secs)
                                                {
                                                    TreeNodeItemSelector secsnode = minsnode.CreateChildNode(sec.Key.ToString("D2") + " " + "s", sec.First().Value);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    //add timespan nodes
                    else if (_filterValueType == typeof(TimeSpan))
                    {
                        var days =
                            from day in nonulls
                            group day by ((TimeSpan)day.Value).Days into cd
                            orderby cd.Key ascending
                            select cd;

                        foreach (var day in days)
                        {
                            TreeNodeItemSelector daysnode = TreeNodeItemSelector.CreateNode(day.Key.ToString("D2"), 
                                                                                            day.Key, 
                                                                                            CheckState.Checked, 
                                                                                            TreeNodeItemSelector.CustomNodeType.DateTimeNode,
                                                                                            this.RightToLeft);
                            treeFilterSelection.Nodes.Add(daysnode);

                            var hours =
                                from hour in day
                                group hour by ((TimeSpan)hour.Value).Hours into ch
                                orderby ch.Key ascending
                                select ch;

                            foreach (var hour in hours)
                            {
                                TreeNodeItemSelector hoursnode = daysnode.CreateChildNode(hour.Key.ToString("D2") + " " + "h", hour.Key);

                                var mins =
                                    from min in hour
                                    group min by ((TimeSpan)min.Value).Minutes into cmin
                                    orderby cmin.Key ascending
                                    select cmin;

                                foreach (var min in mins)
                                {
                                    TreeNodeItemSelector minsnode = hoursnode.CreateChildNode(min.Key.ToString("D2") + " " + "m", min.Key);

                                    var secs =
                                        from sec in min
                                        group sec by ((TimeSpan)sec.Value).Seconds into cs
                                        orderby cs.Key ascending
                                        select cs;

                                    foreach (var sec in secs)
                                    {
                                        TreeNodeItemSelector secsnode = minsnode.CreateChildNode(sec.Key.ToString("D2") + " " + "s", sec.First().Value);
                                    }
                                }
                            }
                        }
                    }

                    //add boolean nodes
                    else if (_filterValueType == typeof(bool))
                    {
                        var values = nonulls.Where<DataGridViewCell>(c => (bool)c.Value == true);

                        if (values.Count() != nonulls.Count())
                        {
                            TreeNodeItemSelector node = 
                                TreeNodeItemSelector.CreateNode(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVNodeSelectFalse.ToString()], 
                                                                false, 
                                                                CheckState.Checked, 
                                                                TreeNodeItemSelector.CustomNodeType.Default,
                                                                this.RightToLeft);
                            treeFilterSelection.Nodes.Add(node);
                        }

                        if (values.Count() > 0)
                        {
                            TreeNodeItemSelector node = 
                                TreeNodeItemSelector.CreateNode(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVNodeSelectTrue.ToString()], 
                                                                true, 
                                                                CheckState.Checked, 
                                                                TreeNodeItemSelector.CustomNodeType.Default,
                                                                this.RightToLeft);
                            treeFilterSelection.Nodes.Add(node);
                        }
                    }

                    //ignore image nodes
                    else if (_filterValueType == typeof(Bitmap))
                    { }

                    //add string nodes
                    else
                    {
                        foreach (var v in nonulls.GroupBy(c => c.Value).OrderBy(g => g.Key))
                        {
                            TreeNodeItemSelector node = TreeNodeItemSelector.CreateNode(v.First().FormattedValue.ToString(), 
                                                                                        v.Key, 
                                                                                        CheckState.Checked, 
                                                                                        TreeNodeItemSelector.CustomNodeType.Default,
                                                                                        this.RightToLeft);
                            treeFilterSelection.Nodes.Add(node);
                        }
                    }
                }
            }

            treeFilterSelection.EndUpdate();
        }

        /// <summary>
        /// Duplicate Nodes
        /// </summary>
        private void DuplicateNodes()
        {
            _startingNodes = new TreeNodeItemSelector[treeFilterSelection.Nodes.Count];
            int i = 0;
            foreach (TreeNodeItemSelector n in treeFilterSelection.Nodes)
            {
                _startingNodes[i] = n.Clone();
                i++;
            }
        }

        /// <summary>
        /// Duplicate filter nodes
        /// </summary>
        private void DuplicateFilterNodes()
        {
            _filterNodes = new TreeNodeItemSelector[treeFilterSelection.Nodes.Count];
            int i = 0;
            foreach (TreeNodeItemSelector n in treeFilterSelection.Nodes)
            {
                _filterNodes[i] = n.Clone();
                i++;
            }
        }

        /// <summary>
        /// Restore Nodes
        /// </summary>
        private void RestoreNodes()
        {
            treeFilterSelection.Nodes.Clear();
            if (_startingNodes != null)
                treeFilterSelection.Nodes.AddRange(_startingNodes);
        }

        /// <summary>
        /// Restore Filter
        /// </summary>
        private void RestoreFilterNodes()
        {
            treeFilterSelection.Nodes.Clear();
            if (_filterNodes != null)
                treeFilterSelection.Nodes.AddRange(_filterNodes);
        }

        /// <summary>
        /// Set CheckState property for all nodes
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="isChecked"></param>
        private void SetNodesCheckState(TreeNodeCollection nodes, bool isChecked)
        {
            foreach (TreeNodeItemSelector node in nodes)
            {
                node.Checked = isChecked;
                if (node.Nodes != null && node.Nodes.Count > 0)
                    SetNodesCheckState(node.Nodes, isChecked);
            }
        }

        /// <summary>
        /// Change node checked state
        /// </summary>
        /// <param name="node"></param>
        private void NodeCheckChange(TreeNodeItemSelector node)
        {
            node.CheckState = (node.CheckState == CheckState.Checked) ? CheckState.Unchecked : CheckState.Checked;
            //
            if (node.NodeType == TreeNodeItemSelector.CustomNodeType.SelectAll)
            {
                SetNodesCheckState(treeFilterSelection.Nodes, node.Checked);
                btnFilter.Enabled = node.Checked;
            }
            else
            {
                // Take care of child nodes
                if (node.Nodes.Count > 0)
                {
                    SetNodesCheckState(node.Nodes, node.Checked);
                }
                // Refresh all nodes
                CheckState state = UpdateNodesCheckState(treeFilterSelection.Nodes);

                GetSelectAllNode().CheckState = state;
                btnFilter.Enabled = !(state == CheckState.Unchecked);
            }
        }

        /// <summary>
        /// Update Nodes CheckState recursively
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private CheckState UpdateNodesCheckState(TreeNodeCollection nodes)
        {
            CheckState result = CheckState.Unchecked;
            bool isFirstNode = true;
            bool isAllNodesSomeCheckState = true;

            foreach (TreeNodeItemSelector n in nodes)
            {
                if (n.NodeType == TreeNodeItemSelector.CustomNodeType.SelectAll)
                    continue;

                if (n.Nodes.Count > 0)
                {
                    n.CheckState = UpdateNodesCheckState(n.Nodes);
                }

                if (isFirstNode)
                {
                    result = n.CheckState;
                    isFirstNode = false;
                }
                else
                {
                    if (result != n.CheckState)
                        isAllNodesSomeCheckState = false;
                }
            }

            if (isAllNodesSomeCheckState)
                return result;
            else
                return CheckState.Indeterminate;
        }

        /// <summary>
        /// Get the SelectAll Node
        /// </summary>
        /// <returns></returns>
        private TreeNodeItemSelector GetSelectAllNode()
        {
            TreeNodeItemSelector result = null;
            int i = 0;
            foreach (TreeNodeItemSelector n in treeFilterSelection.Nodes)
            {
                if (n.NodeType == TreeNodeItemSelector.CustomNodeType.SelectAll)
                {
                    result = n;
                    break;
                }
                else if (i > 2)
                    break;
                else
                    i++;
            }

            return result;
        }

        /// <summary>
        /// Get the SelectEmpty Node
        /// </summary>
        /// <returns></returns>
        private TreeNodeItemSelector GetSelectEmptyNode()
        {
            TreeNodeItemSelector result = null;
            int i = 0;
            foreach (TreeNodeItemSelector n in treeFilterSelection.Nodes)
            {
                if (n.NodeType == TreeNodeItemSelector.CustomNodeType.SelectEmpty)
                {
                    result = n;
                    break;
                }
                else if (i > 2)
                    break;
                else
                    i++;
            }

            return result;
        }

        /// <summary>
        /// Build a Filter string based on selected Nodes
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        private string BuildNodesFilterString(IEnumerable<TreeNodeItemSelector> nodes)
        {
            StringBuilder sb = new StringBuilder("");
            string appx = (_filterValueType == typeof(DateTime) || _filterValueType == typeof(TimeSpan)) ? " OR " : ", ";

            if (nodes != null && nodes.Count() > 0)
            {
                if (_filterValueType == typeof(DateTime))
                {
                    foreach (TreeNodeItemSelector n in nodes)
                    {
                        if (n.Checked && (n.Nodes.AsParallel().Cast<TreeNodeItemSelector>().Where(sn => sn.CheckState != CheckState.Unchecked).Count() == 0))
                        {
                            DateTime dt = (DateTime)n.Value;
                            sb.Append("(Convert([{0}], 'System.String') LIKE '%" + Convert.ToString((this.FilterDateAndTimeEnabled ? dt : dt.Date), CultureInfo.CurrentCulture) + "%')" + appx);
                        }
                        else if (n.CheckState != CheckState.Unchecked && n.Nodes.Count > 0)
                        {
                            string subnode = BuildNodesFilterString(n.Nodes.AsParallel().Cast<TreeNodeItemSelector>().Where(sn => sn.CheckState != CheckState.Unchecked));
                            if (subnode.Length > 0)
                                sb.Append(subnode + appx);
                        }
                    }
                }
                else if (_filterValueType == typeof(TimeSpan))
                {
                    foreach (TreeNodeItemSelector n in nodes)
                    {
                        if (n.Checked && (n.Nodes.AsParallel().Cast<TreeNodeItemSelector>().Where(sn => sn.CheckState != CheckState.Unchecked).Count() == 0))
                        {
                            TimeSpan ts = (TimeSpan)n.Value;
                            sb.Append("(Convert([{0}], 'System.String') LIKE '%" + XmlConvert.ToString(ts) + "%')" + appx);
                            // sb.Append("(Convert([{0}], 'System.String') LIKE '%P" + ((int)ts.Days > 0 ? (int)ts.Days + "D" : "") + (ts.TotalHours > 0 ? "T" : "") + ((int)ts.Hours > 0 ? (int)ts.Hours + "H" : "") + ((int)ts.Minutes > 0 ? (int)ts.Minutes + "M" : "") + ((int)ts.Seconds > 0 ? (int)ts.Seconds + "S" : "") + "%')" + appx);
                        }
                        else if (n.CheckState != CheckState.Unchecked && n.Nodes.Count > 0)
                        {
                            string subnode = BuildNodesFilterString(n.Nodes.AsParallel().Cast<TreeNodeItemSelector>().Where(sn => sn.CheckState != CheckState.Unchecked));
                            if (subnode.Length > 0)
                                sb.Append(subnode + appx);
                        }
                    }
                }
                else if (_filterValueType == typeof(bool))
                {
                    foreach (TreeNodeItemSelector n in nodes)
                    {
                        sb.Append(n.Value.ToString());
                        break;
                    }
                }
                else if (_filterValueType == typeof(Int32) || _filterValueType == typeof(Int64) || _filterValueType == typeof(Int16) ||
                    _filterValueType == typeof(UInt32) || _filterValueType == typeof(UInt64) || _filterValueType == typeof(UInt16) ||
                    _filterValueType == typeof(Byte) || _filterValueType == typeof(SByte))
                {
                    foreach (TreeNodeItemSelector n in nodes)
                        sb.Append(n.Value.ToString() + appx);
                }
                else if (_filterValueType == typeof(Single) || _filterValueType == typeof(Double) || _filterValueType == typeof(Decimal))
                {
                    foreach (TreeNodeItemSelector n in nodes)
                        sb.Append(n.Value.ToString().Replace(",", ".") + appx);
                }
                else if (_filterValueType == typeof(Bitmap))
                { }
                else
                {
                    foreach (TreeNodeItemSelector n in nodes)
                        sb.Append("'" + n.Value.ToString().Replace("'", "''") + "'" + appx);
                }
            }

            if (sb.Length > appx.Length && _filterValueType != typeof(bool))
                sb.Remove(sb.Length - appx.Length, appx.Length);

            return sb.ToString();
        }

        /// <summary>
        /// Set the Filter String using checkList selected Nodes
        /// </summary>
        private void SetCheckListFilter()
        {
            bool allNodes = false;
            string filterString = string.Empty; // No fillter by default
            TreeNodeItemSelector selectAllNode = GetSelectAllNode();
            if (selectAllNode != null && selectAllNode.Checked)
            {   // Selection of 'All Items' means cancellation of selected filter
                allNodes = true;
            }
            else
            {
                StringBuilder filterBuilder = new StringBuilder();
                if (treeFilterSelection.Nodes.Count > 1)
                {
                    // Reuse variable to select Empty Value node
                    selectAllNode = GetSelectEmptyNode();
                    // If Empty Value checked, start filter string with this filter
                    if (selectAllNode != null && selectAllNode.Checked)
                        filterBuilder.Append("[{0}] IS NULL");
                    // Traverse tree is search for seleced nodes
                    if (treeFilterSelection.Nodes.Count > 2 || selectAllNode == null)
                    {
                        string nodesFilter = BuildNodesFilterString(
                                (IsFilterNOTINLogicEnabled && 
                                 (_filterValueType != typeof(DateTime) && _filterValueType != typeof(TimeSpan) && _filterValueType != typeof(bool)) ?
                                treeFilterSelection.Nodes.AsParallel().Cast<TreeNodeItemSelector>().Where(
                                            n => n.NodeType != TreeNodeItemSelector.CustomNodeType.SelectAll
                                                && n.NodeType != TreeNodeItemSelector.CustomNodeType.SelectEmpty
                                                && n.CheckState == CheckState.Unchecked) :
                                treeFilterSelection.Nodes.AsParallel().Cast<TreeNodeItemSelector>().Where(
                                            n => n.NodeType != TreeNodeItemSelector.CustomNodeType.SelectAll
                                                && n.NodeType != TreeNodeItemSelector.CustomNodeType.SelectEmpty
                                                && n.CheckState != CheckState.Unchecked)
                                )
                        );


                        if (nodesFilter.Length > 0)
                        {
                            if (filterBuilder.Length > 0)
                                filterBuilder.Append(" OR ");

                            if (_filterValueType == typeof(DateTime) || _filterValueType == typeof(TimeSpan))
                            {
                                filterBuilder.Append(nodesFilter);
                            }
                            else if (_filterValueType == typeof(bool))
                            {
                                filterBuilder.Append("[{0}] =");
                                filterBuilder.Append(nodesFilter);
                            }
                            else if (_filterValueType == typeof(Int32) || _filterValueType == typeof(Int64) || _filterValueType == typeof(Int16) ||
                                        _filterValueType == typeof(UInt32) || _filterValueType == typeof(UInt64) || _filterValueType == typeof(UInt16) ||
                                        _filterValueType == typeof(Decimal) || 
                                        _filterValueType == typeof(Byte) || _filterValueType == typeof(SByte) || _filterValueType == typeof(String))
                            {
                                filterBuilder.Append(IsFilterNOTINLogicEnabled ? "[{0}] NOT IN (" : "[{0}] IN (");
                                filterBuilder.Append(nodesFilter);
                                filterBuilder.Append(")");
                            }
                            else if (_filterValueType == typeof(Bitmap))
                            { /* Exclude bitmap columns from filter */ }
                            else
                            {   // Any other type (like Single, Double, Guid, etc)
                                filterBuilder.Append(IsFilterNOTINLogicEnabled ? "Convert([{0}],System.String) NOT IN (" : "Convert([{0}],System.String) IN (");
                                filterBuilder.Append(nodesFilter);
                                filterBuilder.Append(")");
                            }
                        }
                    }
                }
                //
                DuplicateFilterNodes();
                //
                filterString = filterBuilder.ToString();
            }

            FilterSelected?.Invoke(this, new ChecklistFilterSelectedEventArgs(allNodes, filterString));

        }

        private void ApplyFilter(string filter)
        {
            string[] subfilters = filter.Split(new string[] { " OR " }, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (string subfilter in subfilters)
            {
                if (Regex.IsMatch(subfilter.Trim(), @"^\[\w(\w|\d)*\] IS NULL$"))
                {
                    // Find and select 'blanks' node
                    TreeNodeItemSelector emptyNode = GetSelectEmptyNode();
                    if (emptyNode != null)
                        emptyNode.Checked = true;
                }
                else if (_filterValueType == typeof(DateTime))
                {
                    // Parse value from subfilter string (Convert([{0}], 'System.String') LIKE '%val1%') (using CurrentCulture)
                    Match match = Regex.Match(subfilter,
                                                @"^(?n)\(?Convert\(\[\w(\w|\d)\]\,\s*\'?System\.String\'?\)\s+LIKE\s+\'%(?<value>[^%]+)%\'\)?$");
                    if (match.Success && DateTime.TryParse(match.Groups["value"].Value,
                                                            CultureInfo.CurrentCulture,
                                                            DateTimeStyles.AssumeLocal,
                                                            out DateTime date))
                    {
                        SetDateTimeNodesCheckState(treeFilterSelection.Nodes, date, 0);
                    }

                    // Local function to traverse tree and set DateTime nodes state
                    CheckState SetDateTimeNodesCheckState(TreeNodeCollection nodes, DateTime dateValue, int treeLevel)
                    {
                        // If function is called, it means that parent node is OK to select
                        if (nodes.Count == 0)
                        {
                            return CheckState.Checked;
                        }
                        // Value to asses nodes
                        int testValue = -1;
                        switch (treeLevel)
                        {
                            case 0: testValue = dateValue.Year; break;
                            case 1: testValue = dateValue.Month; break;
                            case 2: testValue = dateValue.Day; break;
                            case 3: testValue = dateValue.Hour; break;
                            case 4: testValue = dateValue.Minute; break;
                            case 5: testValue = dateValue.Second; break;
                        }
                        CheckState parentState = CheckState.Unchecked;
                        CheckState nodeState;
                        // Search for nodes with this value and check it
                        foreach (TreeNodeItemSelector n in nodes)
                        {
                            if ((int)n.Value == testValue)
                            {
                                nodeState = SetDateTimeNodesCheckState(n.Nodes, dateValue, treeLevel + 1);
                                if (nodeState != CheckState.Unchecked)
                                {   // node is already in unchecked state
                                    n.CheckState = nodeState;
                                }
                            }
                            else
                            {
                                nodeState = CheckState.Unchecked;
                            }
                            // Determin parent state
                            if (nodeState == CheckState.Checked && parentState != CheckState.Indeterminate)
                            {
                                parentState = CheckState.Checked;
                            }
                            else if (!(nodeState == CheckState.Unchecked && parentState == CheckState.Unchecked))
                            {
                                parentState = CheckState.Indeterminate;
                            }
                        }
                        return parentState;
                    }
                }
                else if (_filterValueType == typeof(TimeSpan))
                {
                    // Parse value from subfilter string (Convert([{0}], 'System.String') LIKE '%val1%') (using XmlConvert)
                    Match match = Regex.Match(subfilter,
                        @"^\(?Convert\(\[\w(\w|\d)*\]\,\s*\'System\.String'\)\s+LIKE\s+\'\%(?<value>[-]?P(?!$)(\d+D)?(T(?=\d)(\d+H)?(\d+M)?(\d+(?:\.\d+)?S)?)?)\%\'\)?$");
                    if (match.Success && match.Groups["value"].Success)
                    {
                        try
                        {
                            TimeSpan ts = XmlConvert.ToTimeSpan(match.Groups["value"].Value);
                            SetTimeSpanNodesCheckState(treeFilterSelection.Nodes, ts, 0);
                        }
                        catch (FormatException ex)
                        {
                            // failed to accept
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }

                        CheckState SetTimeSpanNodesCheckState(TreeNodeCollection nodes, TimeSpan ts, int treeLevel)
                        {
                            int checkedCnt = 0;
                            CheckState nodeState = CheckState.Unchecked;
                            // Search for nodes with this value and check it
                            foreach (TreeNodeItemSelector n in nodes)
                            {
                                if (treeLevel < 3)
                                {
                                    nodeState = SetTimeSpanNodesCheckState(n.Nodes, ts, treeLevel + 1);
                                }
                                else
                                {   // Seconds level, where each node holds TimeSpan value
                                    if ((TimeSpan)n.Value == ts)
                                    {
                                        nodeState = CheckState.Checked;
                                    }
                                }
                                if (nodeState != CheckState.Unchecked)
                                {   // node is already in unchecked state
                                    n.CheckState = nodeState;
                                    if (nodeState == CheckState.Checked)
                                    {
                                        checkedCnt++;
                                    }
                                }
                            }
                            CheckState parentState = CheckState.Unchecked;
                            if (checkedCnt > 0)
                            {
                                parentState = (nodes.Count == checkedCnt ? CheckState.Checked : CheckState.Indeterminate);
                            }
                            return parentState;
                        }
                    }
                }
                else if (_filterValueType == typeof(bool))
                {
                    // Parse value from subfilter string [{0}] = True/False
                    Match match = Regex.Match(subfilter.ToLower(),
                                                @"(?n)^\(?\[\w(\w|\d)*\]\s+=\s+(?<value>true|false)\)?$");
                    if (match.Success && match.Groups["value"].Success)
                    {
                        bool checkVal = (match.Groups["value"].Value == "true");
                        // Search for nodes with this value and check it
                        foreach (TreeNodeItemSelector n in treeFilterSelection.Nodes)
                        {
                            if ((bool)n.Value == checkVal)
                            {
                                n.Checked = true;
                            }
                        }
                    }
                }
                else if (_filterValueType == typeof(Int32) || _filterValueType == typeof(Int64) || _filterValueType == typeof(Int16) ||
                    _filterValueType == typeof(UInt32) || _filterValueType == typeof(UInt64) || _filterValueType == typeof(UInt16) ||
                    _filterValueType == typeof(Byte) || _filterValueType == typeof(SByte) || _filterValueType == typeof(Decimal))
                {
                    // Parse values from subfilter string:
                    //  [{0}] [NOT] IN (val1, val2, etc) 
                    string decimalPattern = @"(?<value>[-+]?[0-9]*\.?[0-9]*)";
                    string filterPattern = @"(?n)^\(?\[\w(\w|\d)*\](?:\s+(?<not>NOT))?\s+IN\s+\(" +
                                                decimalPattern + @"\s*(,\s*" + decimalPattern + @")*\){1,2}$";
                    Match match = Regex.Match(subfilter.Trim(), filterPattern);
                    if (match.Success && match.Groups["value"].Success)
                    {
                        // decimal has largest value range among these types
                        List<decimal> values = new List<decimal>();
                        foreach (Capture val in match.Groups["value"].Captures)
                        {
                            if (decimal.TryParse(val.Value, out decimal value))
                            {
                                values.Add(value);
                            }
                        }

                        bool notIn = match.Groups["not"].Success;

                        // Search for nodes with these values and check them
                        foreach (TreeNodeItemSelector n in treeFilterSelection.Nodes)
                        {
                            if (n.NodeType == TreeNodeItemSelector.CustomNodeType.Default)
                            {
                                decimal nodeValue = (decimal)Convert.ChangeType(n.Value, TypeCode.Decimal);
                                foreach (decimal value in values)
                                {
                                    if (nodeValue == value)
                                    {
                                        n.Checked = true;
                                        break;
                                    }
                                }
                                if (notIn)
                                {   
                                    n.Checked = !n.Checked;
                                }
                            }
                        }
                    }
                }
                else if (_filterValueType == typeof(Single) || _filterValueType == typeof(Double))
                {
                    // Parse values from subfilter string:
                    //  Convert([{0}],System.String) [NOT] IN (val1, val2, etc)
                    string doublePattern = @"(?<value>[+-]?[0-9]*\.?[Ee]?[+-]?[0-9]*)";   // add exponential form?
                    string filterPattern = 
                            @"(?n)^\(?Convert\(\[\w(\w|\d)*\],\s*\'?System\.String\'?\)(?:\s+(?<not>NOT))?\s+IN\s+\(" + 
                            doublePattern + @"\s*(,\s*" + doublePattern + @")*\){1,2}$";
                    Match match = Regex.Match(subfilter.Trim(), filterPattern); 
                    if (match.Success && match.Groups["value"].Success)
                    {
                        // Double has largest value range among these types
                        List<double> values = new List<double>();
                        foreach (Capture val in match.Groups["value"].Captures)
                        {
                            if (double.TryParse(val.Value, out double value))
                            {
                                values.Add(value);
                            }
                        }

                        bool notIn = match.Groups["not"].Success;

                        // Search for nodes with these values and check them
                        foreach (TreeNodeItemSelector n in treeFilterSelection.Nodes)
                        {
                            if (n.NodeType == TreeNodeItemSelector.CustomNodeType.Default)
                            {
                                foreach (double value in values)
                                {
                                    if ((double)n.Value == value)
                                    {
                                        n.Checked = true;
                                        break;
                                    }
                                }
                                if (notIn)
                                {
                                    n.Checked = !n.Checked;
                                }
                            }
                        }
                    }
                }
                else if (_filterValueType == typeof(Bitmap))
                { /* do nothing */ }
                else if (_filterValueType == typeof(String))
                {
                    // Parse values from subfilter string:
                    //  [{0}] [NOT] IN ('val1', 'val2', etc) 
                    // Anyway, replace '' with ' in each valN
                    string filterPattern = 
                        @"(?n)^\(?\[\w(\w|\d)*\](?:\s+(?<not>NOT))?\s+IN\s+\(\'(?<value>.*?)\'\s*(,\s*\'(?<value>.*?)\')*\)$";
                    Match match = Regex.Match(subfilter.Trim(), filterPattern);
                    if (match.Success && match.Groups["value"].Success)
                    {

                        bool notIn = match.Groups["not"].Success;

                        // Search for nodes with these values and check them
                        foreach (TreeNodeItemSelector n in treeFilterSelection.Nodes)
                        {
                            if (n.NodeType == TreeNodeItemSelector.CustomNodeType.Default)
                            {
                                string nodeValue = n.Value.ToString();
                                foreach (Capture val in match.Groups["value"].Captures)
                                {
                                    string value = val.Value.Replace("''", "'");
                                    if (nodeValue == value)
                                    {
                                        n.Checked = true;
                                        break;
                                    }
                                    if (notIn)
                                    {
                                        n.Checked = !n.Checked;
                                    }
                                }
                            }
                        }
                    }
                }
                else  // GUID and all others
                { 
                    // Parse values from subfilter string:
                    //  Convert([{ 0}],System.String) [NOT] IN('val1', 'val2', etc) 
                    string filterPattern =
                        @"(?n)^\(?Convert\(\[\w(\w|\d)*\]\,\s*\'?System\.String\'?\)(?:\s+(?<not>NOT))?\s+IN\s+\(\'(?<value>.*?)\'\s*(?:,\s*\'(?<value>.*?)\')*\)$";
                    Match match = Regex.Match(subfilter.Trim(),filterPattern);
                    if (match.Success && match.Groups["value"].Success)
                    {
                        bool notIn = match.Groups["not"].Success;

                        // Search for nodes with these values and check them
                        foreach (TreeNodeItemSelector n in treeFilterSelection.Nodes)
                        {
                            if (n.NodeType == TreeNodeItemSelector.CustomNodeType.Default)
                            {
                                foreach (Capture val in match.Groups["value"].Captures)
                                {
                                    // Doubled apostrophe ('') is not supposed to be part of the value
                                    if (n.Value.ToString() == val.Value)
                                    {
                                        n.Checked = true;
                                        break;
                                    }
                                    if (notIn)
                                    {
                                        n.Checked = !n.Checked;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            SetCheckListFilter();
        }

        private void LoadChecklist(IEnumerable<DataGridViewCell> valueCells, Type valueType, Action setCheckState)
        {
            _filterValueType = valueType;
            BuildNodes(valueCells);
            if (DoesTextFilterRemoveNodesOnSearch && treeFilterSelection.Nodes.Count != _initialNodes.Count())
            {
                _initialNodes = new TreeNodeItemSelector[treeFilterSelection.Nodes.Count];
                _restoreNodes = new TreeNodeItemSelector[treeFilterSelection.Nodes.Count];
                int i = 0;
                foreach (TreeNodeItemSelector n in treeFilterSelection.Nodes)
                {
                    _initialNodes[i] = n.Clone();
                    _restoreNodes[i] = n.Clone();
                    i++;
                }
            }

            setCheckState?.Invoke();
            DuplicateNodes();

            ClearFilterSelectionText();
        }

        #endregion
    }
}
