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
            treeFilterSelection.MouseLeave += TreeFilterSelection_MouseLeave;
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


        #region // Public properties
        /// <summary>
        /// Enable / disable use of context search in filter data
        /// </summary>
        public bool FilterContextSearchEnabled
        {
            get => textFilterSelection.Enabled;
            set => textFilterSelection.Enabled = value;
        }


        #endregion


        #region // Public events
        // or CancelEventHandler if event might be cancelled in menu code
        public event EventHandler FilterSelected;
        public event EventHandler FilterSelectionCancelled;

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
            // Subscribe for future events
            ((ContextMenuStrip)Owner).RightToLeftChanged += FilterSelectionListPanel_RightToLeftChanged;
        }

        private void FilterSelectionListPanel_RightToLeftChanged(object sender, EventArgs e)
        {
            RightToLeft = Owner.RightToLeft;
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
            throw new NotImplementedException();
        }

        private void TreeFilterSelection_MouseLeave(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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
    }
}
