namespace AdvancedDataGridViewSample
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel_top = new System.Windows.Forms.Panel();
            this.btnDesc = new System.Windows.Forms.Button();
            this.btnAsc = new System.Windows.Forms.Button();
            this.optionSortDesc = new System.Windows.Forms.RadioButton();
            this.optionSortAsc = new System.Windows.Forms.RadioButton();
            this.optionSortNone = new System.Windows.Forms.RadioButton();
            this.label_date_sort = new System.Windows.Forms.Label();
            this.btnHebrew = new System.Windows.Forms.Button();
            this.label_strfilter = new System.Windows.Forms.Label();
            this.textBox_strfilter = new System.Windows.Forms.TextBox();
            this.button_load = new System.Windows.Forms.Button();
            this.textBox_sort = new System.Windows.Forms.TextBox();
            this.textBox_filter = new System.Windows.Forms.TextBox();
            this.label_sort = new System.Windows.Forms.Label();
            this.label_filter = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.advancedDataGridViewSearchToolBar_main = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.button_unloadfilters = new System.Windows.Forms.Button();
            this.label_sortsaved = new System.Windows.Forms.Label();
            this.label_filtersaved = new System.Windows.Forms.Label();
            this.comboBox_sortsaved = new System.Windows.Forms.ComboBox();
            this.button_setsavedfilter = new System.Windows.Forms.Button();
            this.button_savefilters = new System.Windows.Forms.Button();
            this.comboBox_filtersaved = new System.Windows.Forms.ComboBox();
            this.label_total = new System.Windows.Forms.Label();
            this.textBox_total = new System.Windows.Forms.TextBox();
            this.panel_grid = new System.Windows.Forms.Panel();
            this.advancedDataGridView_main = new Zuby.ADGV.AdvancedDataGridView();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.bindingSource_main = new System.Windows.Forms.BindingSource(this.components);
            this.optionIgnoreSort = new System.Windows.Forms.RadioButton();
            this.panel_top.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel_grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView_main)).BeginInit();
            this.panel_bottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_main)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_top
            // 
            this.panel_top.Controls.Add(this.btnDesc);
            this.panel_top.Controls.Add(this.btnAsc);
            this.panel_top.Controls.Add(this.optionIgnoreSort);
            this.panel_top.Controls.Add(this.optionSortDesc);
            this.panel_top.Controls.Add(this.optionSortAsc);
            this.panel_top.Controls.Add(this.optionSortNone);
            this.panel_top.Controls.Add(this.label_date_sort);
            this.panel_top.Controls.Add(this.btnHebrew);
            this.panel_top.Controls.Add(this.label_strfilter);
            this.panel_top.Controls.Add(this.textBox_strfilter);
            this.panel_top.Controls.Add(this.button_load);
            this.panel_top.Controls.Add(this.textBox_sort);
            this.panel_top.Controls.Add(this.textBox_filter);
            this.panel_top.Controls.Add(this.label_sort);
            this.panel_top.Controls.Add(this.label_filter);
            this.panel_top.Controls.Add(this.panel3);
            this.panel_top.Controls.Add(this.button_unloadfilters);
            this.panel_top.Controls.Add(this.label_sortsaved);
            this.panel_top.Controls.Add(this.label_filtersaved);
            this.panel_top.Controls.Add(this.comboBox_sortsaved);
            this.panel_top.Controls.Add(this.button_setsavedfilter);
            this.panel_top.Controls.Add(this.button_savefilters);
            this.panel_top.Controls.Add(this.comboBox_filtersaved);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(784, 192);
            this.panel_top.TabIndex = 0;
            // 
            // btnDesc
            // 
            this.btnDesc.Location = new System.Drawing.Point(717, 136);
            this.btnDesc.Name = "btnDesc";
            this.btnDesc.Size = new System.Drawing.Size(46, 23);
            this.btnDesc.TabIndex = 23;
            this.btnDesc.Text = "DESC";
            this.btnDesc.UseVisualStyleBackColor = true;
            this.btnDesc.Click += new System.EventHandler(this.btnDesc_Click);
            // 
            // btnAsc
            // 
            this.btnAsc.Location = new System.Drawing.Point(665, 136);
            this.btnAsc.Name = "btnAsc";
            this.btnAsc.Size = new System.Drawing.Size(46, 23);
            this.btnAsc.TabIndex = 23;
            this.btnAsc.Text = "ASC";
            this.btnAsc.UseVisualStyleBackColor = true;
            this.btnAsc.Click += new System.EventHandler(this.btnAsc_Click);
            // 
            // optionSortDesc
            // 
            this.optionSortDesc.AutoSize = true;
            this.optionSortDesc.Location = new System.Drawing.Point(489, 139);
            this.optionSortDesc.Name = "optionSortDesc";
            this.optionSortDesc.Size = new System.Drawing.Size(76, 17);
            this.optionSortDesc.TabIndex = 22;
            this.optionSortDesc.Text = "Sort DESC";
            this.optionSortDesc.UseVisualStyleBackColor = true;
            this.optionSortDesc.CheckedChanged += new System.EventHandler(this.optionSort_ChechedChanged);
            // 
            // optionSortAsc
            // 
            this.optionSortAsc.AutoSize = true;
            this.optionSortAsc.Location = new System.Drawing.Point(414, 139);
            this.optionSortAsc.Name = "optionSortAsc";
            this.optionSortAsc.Size = new System.Drawing.Size(68, 17);
            this.optionSortAsc.TabIndex = 22;
            this.optionSortAsc.Text = "Sort ASC";
            this.optionSortAsc.UseVisualStyleBackColor = true;
            this.optionSortAsc.CheckedChanged += new System.EventHandler(this.optionSort_ChechedChanged);
            // 
            // optionSortNone
            // 
            this.optionSortNone.AutoSize = true;
            this.optionSortNone.Location = new System.Drawing.Point(339, 139);
            this.optionSortNone.Name = "optionSortNone";
            this.optionSortNone.Size = new System.Drawing.Size(69, 17);
            this.optionSortNone.TabIndex = 22;
            this.optionSortNone.Text = "Clear sort";
            this.optionSortNone.UseVisualStyleBackColor = true;
            this.optionSortNone.CheckedChanged += new System.EventHandler(this.optionSort_ChechedChanged);
            // 
            // label_date_sort
            // 
            this.label_date_sort.AutoSize = true;
            this.label_date_sort.Location = new System.Drawing.Point(233, 141);
            this.label_date_sort.Name = "label_date_sort";
            this.label_date_sort.Size = new System.Drawing.Size(100, 13);
            this.label_date_sort.TabIndex = 21;
            this.label_date_sort.Text = "Sort column \"date\":";
            // 
            // btnHebrew
            // 
            this.btnHebrew.Location = new System.Drawing.Point(141, 10);
            this.btnHebrew.Name = "btnHebrew";
            this.btnHebrew.Size = new System.Drawing.Size(114, 23);
            this.btnHebrew.TabIndex = 20;
            this.btnHebrew.Text = "עברית";
            this.btnHebrew.UseVisualStyleBackColor = true;
            this.btnHebrew.Click += new System.EventHandler(this.btnHebrew_Click);
            // 
            // label_strfilter
            // 
            this.label_strfilter.AutoSize = true;
            this.label_strfilter.Location = new System.Drawing.Point(12, 141);
            this.label_strfilter.Name = "label_strfilter";
            this.label_strfilter.Size = new System.Drawing.Size(107, 13);
            this.label_strfilter.TabIndex = 19;
            this.label_strfilter.Text = "Filter column \"string\":";
            // 
            // textBox_strfilter
            // 
            this.textBox_strfilter.Location = new System.Drawing.Point(122, 138);
            this.textBox_strfilter.Name = "textBox_strfilter";
            this.textBox_strfilter.Size = new System.Drawing.Size(100, 20);
            this.textBox_strfilter.TabIndex = 18;
            this.textBox_strfilter.TextChanged += new System.EventHandler(this.textBox_strfilter_TextChanged);
            // 
            // button_load
            // 
            this.button_load.Location = new System.Drawing.Point(15, 10);
            this.button_load.Name = "button_load";
            this.button_load.Size = new System.Drawing.Size(120, 23);
            this.button_load.TabIndex = 17;
            this.button_load.Text = "Load Random Data";
            this.button_load.UseVisualStyleBackColor = true;
            this.button_load.Click += new System.EventHandler(this.button_load_Click);
            // 
            // textBox_sort
            // 
            this.textBox_sort.Location = new System.Drawing.Point(201, 52);
            this.textBox_sort.Multiline = true;
            this.textBox_sort.Name = "textBox_sort";
            this.textBox_sort.ReadOnly = true;
            this.textBox_sort.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_sort.Size = new System.Drawing.Size(180, 80);
            this.textBox_sort.TabIndex = 14;
            // 
            // textBox_filter
            // 
            this.textBox_filter.Location = new System.Drawing.Point(15, 52);
            this.textBox_filter.Multiline = true;
            this.textBox_filter.Name = "textBox_filter";
            this.textBox_filter.ReadOnly = true;
            this.textBox_filter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_filter.Size = new System.Drawing.Size(180, 80);
            this.textBox_filter.TabIndex = 13;
            // 
            // label_sort
            // 
            this.label_sort.AutoSize = true;
            this.label_sort.Location = new System.Drawing.Point(198, 36);
            this.label_sort.Name = "label_sort";
            this.label_sort.Size = new System.Drawing.Size(57, 13);
            this.label_sort.TabIndex = 12;
            this.label_sort.Text = "Sort string:";
            // 
            // label_filter
            // 
            this.label_filter.AutoSize = true;
            this.label_filter.Location = new System.Drawing.Point(12, 36);
            this.label_filter.Name = "label_filter";
            this.label_filter.Size = new System.Drawing.Size(60, 13);
            this.label_filter.TabIndex = 11;
            this.label_filter.Text = "Filter string:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.advancedDataGridViewSearchToolBar_main);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 164);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(784, 28);
            this.panel3.TabIndex = 10;
            // 
            // advancedDataGridViewSearchToolBar_main
            // 
            this.advancedDataGridViewSearchToolBar_main.AllowMerge = false;
            this.advancedDataGridViewSearchToolBar_main.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.advancedDataGridViewSearchToolBar_main.Location = new System.Drawing.Point(0, 0);
            this.advancedDataGridViewSearchToolBar_main.MaximumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_main.MinimumSize = new System.Drawing.Size(0, 27);
            this.advancedDataGridViewSearchToolBar_main.Name = "advancedDataGridViewSearchToolBar_main";
            this.advancedDataGridViewSearchToolBar_main.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.advancedDataGridViewSearchToolBar_main.Size = new System.Drawing.Size(784, 27);
            this.advancedDataGridViewSearchToolBar_main.TabIndex = 0;
            this.advancedDataGridViewSearchToolBar_main.Text = "advancedDataGridViewSearchToolBar_main";
            this.advancedDataGridViewSearchToolBar_main.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.advancedDataGridViewSearchToolBar_main_Search);
            // 
            // button_unloadfilters
            // 
            this.button_unloadfilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_unloadfilters.Location = new System.Drawing.Point(408, 37);
            this.button_unloadfilters.Name = "button_unloadfilters";
            this.button_unloadfilters.Size = new System.Drawing.Size(150, 23);
            this.button_unloadfilters.TabIndex = 9;
            this.button_unloadfilters.Text = "Clean Filter And Sort";
            this.button_unloadfilters.UseVisualStyleBackColor = true;
            this.button_unloadfilters.Click += new System.EventHandler(this.button_unloadfilters_Click);
            // 
            // label_sortsaved
            // 
            this.label_sortsaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_sortsaved.AutoSize = true;
            this.label_sortsaved.Location = new System.Drawing.Point(581, 42);
            this.label_sortsaved.Name = "label_sortsaved";
            this.label_sortsaved.Size = new System.Drawing.Size(61, 13);
            this.label_sortsaved.TabIndex = 8;
            this.label_sortsaved.Text = "Sort saved:";
            // 
            // label_filtersaved
            // 
            this.label_filtersaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_filtersaved.AutoSize = true;
            this.label_filtersaved.Location = new System.Drawing.Point(581, 15);
            this.label_filtersaved.Name = "label_filtersaved";
            this.label_filtersaved.Size = new System.Drawing.Size(64, 13);
            this.label_filtersaved.TabIndex = 7;
            this.label_filtersaved.Text = "Filter saved:";
            // 
            // comboBox_sortsaved
            // 
            this.comboBox_sortsaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_sortsaved.FormattingEnabled = true;
            this.comboBox_sortsaved.Location = new System.Drawing.Point(651, 39);
            this.comboBox_sortsaved.Name = "comboBox_sortsaved";
            this.comboBox_sortsaved.Size = new System.Drawing.Size(121, 21);
            this.comboBox_sortsaved.TabIndex = 6;
            // 
            // button_setsavedfilter
            // 
            this.button_setsavedfilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_setsavedfilter.Location = new System.Drawing.Point(581, 66);
            this.button_setsavedfilter.Name = "button_setsavedfilter";
            this.button_setsavedfilter.Size = new System.Drawing.Size(191, 23);
            this.button_setsavedfilter.TabIndex = 5;
            this.button_setsavedfilter.Text = "Load Saved Filter And Sort";
            this.button_setsavedfilter.UseVisualStyleBackColor = true;
            this.button_setsavedfilter.Click += new System.EventHandler(this.button_setsavedfilter_Click);
            // 
            // button_savefilters
            // 
            this.button_savefilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_savefilters.Location = new System.Drawing.Point(408, 10);
            this.button_savefilters.Name = "button_savefilters";
            this.button_savefilters.Size = new System.Drawing.Size(150, 23);
            this.button_savefilters.TabIndex = 3;
            this.button_savefilters.Text = "Save Current Filter And Sort";
            this.button_savefilters.UseVisualStyleBackColor = true;
            this.button_savefilters.Click += new System.EventHandler(this.button_savefilters_Click);
            // 
            // comboBox_filtersaved
            // 
            this.comboBox_filtersaved.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_filtersaved.FormattingEnabled = true;
            this.comboBox_filtersaved.Location = new System.Drawing.Point(651, 12);
            this.comboBox_filtersaved.Name = "comboBox_filtersaved";
            this.comboBox_filtersaved.Size = new System.Drawing.Size(121, 21);
            this.comboBox_filtersaved.TabIndex = 2;
            // 
            // label_total
            // 
            this.label_total.AutoSize = true;
            this.label_total.Location = new System.Drawing.Point(12, 12);
            this.label_total.Name = "label_total";
            this.label_total.Size = new System.Drawing.Size(59, 13);
            this.label_total.TabIndex = 16;
            this.label_total.Text = "Total rows:";
            // 
            // textBox_total
            // 
            this.textBox_total.Location = new System.Drawing.Point(77, 9);
            this.textBox_total.Name = "textBox_total";
            this.textBox_total.ReadOnly = true;
            this.textBox_total.Size = new System.Drawing.Size(70, 20);
            this.textBox_total.TabIndex = 15;
            // 
            // panel_grid
            // 
            this.panel_grid.Controls.Add(this.advancedDataGridView_main);
            this.panel_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_grid.Location = new System.Drawing.Point(0, 192);
            this.panel_grid.Name = "panel_grid";
            this.panel_grid.Size = new System.Drawing.Size(784, 235);
            this.panel_grid.TabIndex = 1;
            // 
            // advancedDataGridView_main
            // 
            this.advancedDataGridView_main.AllowUserToAddRows = false;
            this.advancedDataGridView_main.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.advancedDataGridView_main.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.advancedDataGridView_main.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.advancedDataGridView_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.advancedDataGridView_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.advancedDataGridView_main.FilterAndSortEnabled = true;
            this.advancedDataGridView_main.Location = new System.Drawing.Point(0, 0);
            this.advancedDataGridView_main.Name = "advancedDataGridView_main";
            this.advancedDataGridView_main.ReadOnly = true;
            this.advancedDataGridView_main.RowHeadersVisible = false;
            this.advancedDataGridView_main.Size = new System.Drawing.Size(784, 235);
            this.advancedDataGridView_main.TabIndex = 0;
            this.advancedDataGridView_main.SortStringChanged += new System.EventHandler<Zuby.ADGV.AdvancedDataGridView.SortEventArgs>(this.advancedDataGridView_main_SortStringChanged);
            this.advancedDataGridView_main.FilterStringChanged += new System.EventHandler<Zuby.ADGV.AdvancedDataGridView.FilterEventArgs>(this.advancedDataGridView_main_FilterStringChanged);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Controls.Add(this.textBox_total);
            this.panel_bottom.Controls.Add(this.label_total);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(0, 427);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(784, 34);
            this.panel_bottom.TabIndex = 2;
            // 
            // bindingSource_main
            // 
            this.bindingSource_main.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.bindingSource_main_ListChanged);
            // 
            // optionIgnoreSort
            // 
            this.optionIgnoreSort.AutoSize = true;
            this.optionIgnoreSort.Checked = true;
            this.optionIgnoreSort.Location = new System.Drawing.Point(571, 139);
            this.optionIgnoreSort.Name = "optionIgnoreSort";
            this.optionIgnoreSort.Size = new System.Drawing.Size(88, 17);
            this.optionIgnoreSort.TabIndex = 22;
            this.optionIgnoreSort.TabStop = true;
            this.optionIgnoreSort.Text = "Sort by Menu";
            this.optionIgnoreSort.UseVisualStyleBackColor = true;
            this.optionIgnoreSort.CheckedChanged += new System.EventHandler(this.optionSort_ChechedChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.panel_grid);
            this.Controls.Add(this.panel_bottom);
            this.Controls.Add(this.panel_top);
            this.MinimumSize = new System.Drawing.Size(800, 500);
            this.Name = "FormMain";
            this.Text = "AdvancedDataGridView Sample";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panel_top.ResumeLayout(false);
            this.panel_top.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel_grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView_main)).EndInit();
            this.panel_bottom.ResumeLayout(false);
            this.panel_bottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Panel panel_grid;
        private System.Windows.Forms.BindingSource bindingSource_main;
        private System.Windows.Forms.Button button_savefilters;
        private System.Windows.Forms.ComboBox comboBox_filtersaved;
        private System.Windows.Forms.Button button_setsavedfilter;
        private System.Windows.Forms.Label label_sortsaved;
        private System.Windows.Forms.Label label_filtersaved;
        private System.Windows.Forms.ComboBox comboBox_sortsaved;
        private System.Windows.Forms.Button button_unloadfilters;
        private Zuby.ADGV.AdvancedDataGridView advancedDataGridView_main;
        private System.Windows.Forms.Panel panel3;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_main;
        private System.Windows.Forms.Label label_total;
        private System.Windows.Forms.TextBox textBox_total;
        private System.Windows.Forms.TextBox textBox_sort;
        private System.Windows.Forms.TextBox textBox_filter;
        private System.Windows.Forms.Label label_sort;
        private System.Windows.Forms.Label label_filter;
        private System.Windows.Forms.Button button_load;
        private System.Windows.Forms.Label label_strfilter;
        private System.Windows.Forms.TextBox textBox_strfilter;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Button btnHebrew;
        private System.Windows.Forms.RadioButton optionSortDesc;
        private System.Windows.Forms.RadioButton optionSortAsc;
        private System.Windows.Forms.RadioButton optionSortNone;
        private System.Windows.Forms.Label label_date_sort;
        private System.Windows.Forms.Button btnDesc;
        private System.Windows.Forms.Button btnAsc;
        private System.Windows.Forms.RadioButton optionIgnoreSort;
    }
}

