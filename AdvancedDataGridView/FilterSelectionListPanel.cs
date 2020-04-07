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

namespace Zuby.ADGV
{
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
            treeFilterSelection.CheckBoxes = true;
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
        /// Set the text filter search nodes behaviour
        /// </summary>
        public bool DoesTextFilterRemoveNodesOnSearch { get; set; } = true;

        #endregion


        #region // Public events
        // or CancelEventHandler if event might be cancelled in menu code
        public event EventHandler FilterSelected;
        public event EventHandler FilterSelectionCancelled;

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
                                                    TreeNodeItemSelector.CustomNodeType.SelectAll);
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
                                                                            TreeNodeItemSelector.CustomNodeType.SelectAll);
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
            BuildNodes(valueCells, valueType);
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

            if (activeFilterType == FilterType.Custom)
                SetNodesCheckState(treeFilterSelection.Nodes, false);
            DuplicateNodes();

            ClearFilterSelectionText();
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
            // Set minimum size for ContextMenuStrip, which will be used by Resizer
            Owner.MinimumSize = new Size(Math.Max(Owner.MinimumSize.Width, SelectionListPanel.MinimumSize.Width),
                                         Math.Max(Owner.MinimumSize.Height, SelectionListPanel.MinimumSize.Height));
            // Change panel RTL layout with owning menu
            ((ContextMenuStrip)Owner).RightToLeftChanged += (s, ea) =>
            {
                RightToLeft = Owner.RightToLeft;
            };
        }
        #endregion


        #region // Control events

        private void TextFilterSelection_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        private void TreeFilterSelection_KeyDown(object sender, KeyEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TreeFilterSelection_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TreeFilterSelection_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void TreeFilterSelection_MouseEnter(object sender, EventArgs e)
        {
            treeFilterSelection.Focus();
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //((ContextMenuStrip)Owner).Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
        private void BuildNodes(IEnumerable<DataGridViewCell> valueCells, Type valueType)
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
                                                    TreeNodeItemSelector.CustomNodeType.SelectAll);
                allnode.NodeFont = new Font(treeFilterSelection.Font, FontStyle.Bold);
                treeFilterSelection.Nodes.Add(allnode);

                if (valueCells.Count() > 0)
                {
                    var nonulls = valueCells.Where<DataGridViewCell>(c => c.Value != null && c.Value != DBNull.Value);

                    //add select empty node
                    if (valueCells.Count() != nonulls.Count())
                    {
                        TreeNodeItemSelector nullnode = TreeNodeItemSelector.CreateNode(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVNodeSelectEmpty.ToString()] + "               ", null, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.SelectEmpty);
                        nullnode.NodeFont = new Font(treeFilterSelection.Font, FontStyle.Bold);
                        treeFilterSelection.Nodes.Add(nullnode);
                    }

                    //add datetime nodes
                    if (valueType == typeof(DateTime))
                    {
                        var years =
                            from year in nonulls
                            group year by ((DateTime)year.Value).Year into cy
                            orderby cy.Key ascending
                            select cy;

                        foreach (var year in years)
                        {
                            TreeNodeItemSelector yearnode = TreeNodeItemSelector.CreateNode(year.Key.ToString(), year.Key, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.DateTimeNode);
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
                    else if (valueType == typeof(TimeSpan))
                    {
                        var days =
                            from day in nonulls
                            group day by ((TimeSpan)day.Value).Days into cd
                            orderby cd.Key ascending
                            select cd;

                        foreach (var day in days)
                        {
                            TreeNodeItemSelector daysnode = TreeNodeItemSelector.CreateNode(day.Key.ToString("D2"), day.Key, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.DateTimeNode);
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
                    else if (valueType == typeof(bool))
                    {
                        var values = nonulls.Where<DataGridViewCell>(c => (bool)c.Value == true);

                        if (values.Count() != nonulls.Count())
                        {
                            TreeNodeItemSelector node = TreeNodeItemSelector.CreateNode(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVNodeSelectFalse.ToString()], false, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.Default);
                            treeFilterSelection.Nodes.Add(node);
                        }

                        if (values.Count() > 0)
                        {
                            TreeNodeItemSelector node = TreeNodeItemSelector.CreateNode(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVNodeSelectTrue.ToString()], true, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.Default);
                            treeFilterSelection.Nodes.Add(node);
                        }
                    }

                    //ignore image nodes
                    else if (valueType == typeof(Bitmap))
                    { }

                    //add string nodes
                    else
                    {
                        foreach (var v in nonulls.GroupBy(c => c.Value).OrderBy(g => g.Key))
                        {
                            TreeNodeItemSelector node = TreeNodeItemSelector.CreateNode(v.First().FormattedValue.ToString(), v.Key, CheckState.Checked, TreeNodeItemSelector.CustomNodeType.Default);
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

        #endregion
    }
}
