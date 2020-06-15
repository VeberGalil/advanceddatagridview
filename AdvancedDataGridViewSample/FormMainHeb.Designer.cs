namespace AdvancedDataGridViewSample
{
    partial class FormMainHeb
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
            this.btnLoadFilter = new System.Windows.Forms.Button();
            this.textLoadFilter = new System.Windows.Forms.TextBox();
            this.labelLoadFilter = new System.Windows.Forms.Label();
            this.btnEnglish = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_setsavedfilter = new System.Windows.Forms.Button();
            this.comboBox_sortsaved = new System.Windows.Forms.ComboBox();
            this.comboBox_filtersaved = new System.Windows.Forms.ComboBox();
            this.button_unloadfilters = new System.Windows.Forms.Button();
            this.button_savefilters = new System.Windows.Forms.Button();
            this.label_sortsaved = new System.Windows.Forms.Label();
            this.label_filtersaved = new System.Windows.Forms.Label();
            this.textBox_strfilter = new System.Windows.Forms.TextBox();
            this.label_strfilter = new System.Windows.Forms.Label();
            this.textBox_sort = new System.Windows.Forms.TextBox();
            this.textBox_filter = new System.Windows.Forms.TextBox();
            this.label_sort = new System.Windows.Forms.Label();
            this.label_filter = new System.Windows.Forms.Label();
            this.button_load = new System.Windows.Forms.Button();
            this.bindingSource_main = new System.Windows.Forms.BindingSource(this.components);
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.textBox_total = new System.Windows.Forms.TextBox();
            this.label_total = new System.Windows.Forms.Label();
            this.panel_grid = new System.Windows.Forms.Panel();
            this.advancedDataGridView_main = new Zuby.ADGV.AdvancedDataGridView();
            this.advancedDataGridViewSearchToolBar_main = new Zuby.ADGV.AdvancedDataGridViewSearchToolBar();
            this.linkFilterHelp = new System.Windows.Forms.LinkLabel();
            this.tooltipHelpLink = new System.Windows.Forms.ToolTip(this.components);
            this.panel_top.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_main)).BeginInit();
            this.panel_bottom.SuspendLayout();
            this.panel_grid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView_main)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_top
            // 
            this.panel_top.Controls.Add(this.linkFilterHelp);
            this.panel_top.Controls.Add(this.btnLoadFilter);
            this.panel_top.Controls.Add(this.textLoadFilter);
            this.panel_top.Controls.Add(this.labelLoadFilter);
            this.panel_top.Controls.Add(this.btnEnglish);
            this.panel_top.Controls.Add(this.panel1);
            this.panel_top.Controls.Add(this.button_setsavedfilter);
            this.panel_top.Controls.Add(this.comboBox_sortsaved);
            this.panel_top.Controls.Add(this.comboBox_filtersaved);
            this.panel_top.Controls.Add(this.button_unloadfilters);
            this.panel_top.Controls.Add(this.button_savefilters);
            this.panel_top.Controls.Add(this.label_sortsaved);
            this.panel_top.Controls.Add(this.label_filtersaved);
            this.panel_top.Controls.Add(this.textBox_strfilter);
            this.panel_top.Controls.Add(this.label_strfilter);
            this.panel_top.Controls.Add(this.textBox_sort);
            this.panel_top.Controls.Add(this.textBox_filter);
            this.panel_top.Controls.Add(this.label_sort);
            this.panel_top.Controls.Add(this.label_filter);
            this.panel_top.Controls.Add(this.button_load);
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(827, 205);
            this.panel_top.TabIndex = 0;
            // 
            // btnLoadFilter
            // 
            this.btnLoadFilter.Location = new System.Drawing.Point(13, 143);
            this.btnLoadFilter.Name = "btnLoadFilter";
            this.btnLoadFilter.Size = new System.Drawing.Size(99, 23);
            this.btnLoadFilter.TabIndex = 24;
            this.btnLoadFilter.Text = "סנן";
            this.btnLoadFilter.UseVisualStyleBackColor = true;
            this.btnLoadFilter.Click += new System.EventHandler(this.BtnLoadFilter_Click);
            // 
            // textLoadFilter
            // 
            this.textLoadFilter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textLoadFilter.Location = new System.Drawing.Point(118, 98);
            this.textLoadFilter.Multiline = true;
            this.textLoadFilter.Name = "textLoadFilter";
            this.textLoadFilter.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.textLoadFilter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textLoadFilter.Size = new System.Drawing.Size(287, 68);
            this.textLoadFilter.TabIndex = 23;
            // 
            // labelLoadFilter
            // 
            this.labelLoadFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLoadFilter.AutoSize = true;
            this.labelLoadFilter.Location = new System.Drawing.Point(279, 82);
            this.labelLoadFilter.Name = "labelLoadFilter";
            this.labelLoadFilter.Size = new System.Drawing.Size(129, 13);
            this.labelLoadFilter.TabIndex = 22;
            this.labelLoadFilter.Text = "קבע סינון באופן חופשי";
            // 
            // btnEnglish
            // 
            this.btnEnglish.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEnglish.Location = new System.Drawing.Point(542, 12);
            this.btnEnglish.Name = "btnEnglish";
            this.btnEnglish.Size = new System.Drawing.Size(99, 23);
            this.btnEnglish.TabIndex = 21;
            this.btnEnglish.Text = "English";
            this.btnEnglish.UseVisualStyleBackColor = true;
            this.btnEnglish.Click += new System.EventHandler(this.BtnEnglish_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.advancedDataGridViewSearchToolBar_main);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 176);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(827, 29);
            this.panel1.TabIndex = 11;
            // 
            // button_setsavedfilter
            // 
            this.button_setsavedfilter.Location = new System.Drawing.Point(13, 72);
            this.button_setsavedfilter.Name = "button_setsavedfilter";
            this.button_setsavedfilter.Size = new System.Drawing.Size(99, 23);
            this.button_setsavedfilter.TabIndex = 10;
            this.button_setsavedfilter.Text = "החל";
            this.button_setsavedfilter.UseVisualStyleBackColor = true;
            this.button_setsavedfilter.Click += new System.EventHandler(this.Button_setsavedfilter_Click);
            // 
            // comboBox_sortsaved
            // 
            this.comboBox_sortsaved.FormattingEnabled = true;
            this.comboBox_sortsaved.Location = new System.Drawing.Point(13, 45);
            this.comboBox_sortsaved.Name = "comboBox_sortsaved";
            this.comboBox_sortsaved.Size = new System.Drawing.Size(138, 21);
            this.comboBox_sortsaved.TabIndex = 9;
            // 
            // comboBox_filtersaved
            // 
            this.comboBox_filtersaved.FormattingEnabled = true;
            this.comboBox_filtersaved.Location = new System.Drawing.Point(13, 13);
            this.comboBox_filtersaved.Name = "comboBox_filtersaved";
            this.comboBox_filtersaved.Size = new System.Drawing.Size(138, 21);
            this.comboBox_filtersaved.TabIndex = 9;
            // 
            // button_unloadfilters
            // 
            this.button_unloadfilters.Location = new System.Drawing.Point(229, 43);
            this.button_unloadfilters.Name = "button_unloadfilters";
            this.button_unloadfilters.Size = new System.Drawing.Size(176, 23);
            this.button_unloadfilters.TabIndex = 8;
            this.button_unloadfilters.Text = "הסר סינון ומיון";
            this.button_unloadfilters.UseVisualStyleBackColor = true;
            this.button_unloadfilters.Click += new System.EventHandler(this.Button_unloadfilters_Click);
            // 
            // button_savefilters
            // 
            this.button_savefilters.Location = new System.Drawing.Point(229, 12);
            this.button_savefilters.Name = "button_savefilters";
            this.button_savefilters.Size = new System.Drawing.Size(176, 23);
            this.button_savefilters.TabIndex = 8;
            this.button_savefilters.Text = "שמור סינון ומיון נוכחיים";
            this.button_savefilters.UseVisualStyleBackColor = true;
            this.button_savefilters.Click += new System.EventHandler(this.Button_savefilters_Click);
            // 
            // label_sortsaved
            // 
            this.label_sortsaved.AutoSize = true;
            this.label_sortsaved.Location = new System.Drawing.Point(157, 48);
            this.label_sortsaved.Name = "label_sortsaved";
            this.label_sortsaved.Size = new System.Drawing.Size(59, 13);
            this.label_sortsaved.TabIndex = 7;
            this.label_sortsaved.Text = "מיון שמור";
            // 
            // label_filtersaved
            // 
            this.label_filtersaved.AutoSize = true;
            this.label_filtersaved.Location = new System.Drawing.Point(157, 17);
            this.label_filtersaved.Name = "label_filtersaved";
            this.label_filtersaved.Size = new System.Drawing.Size(64, 13);
            this.label_filtersaved.TabIndex = 7;
            this.label_filtersaved.Text = "סינון שמור";
            // 
            // textBox_strfilter
            // 
            this.textBox_strfilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_strfilter.Location = new System.Drawing.Point(566, 150);
            this.textBox_strfilter.Name = "textBox_strfilter";
            this.textBox_strfilter.Size = new System.Drawing.Size(130, 20);
            this.textBox_strfilter.TabIndex = 6;
            this.textBox_strfilter.TextChanged += new System.EventHandler(this.TextBox_strfilter_TextChanged);
            // 
            // label_strfilter
            // 
            this.label_strfilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_strfilter.AutoSize = true;
            this.label_strfilter.Location = new System.Drawing.Point(702, 153);
            this.label_strfilter.Name = "label_strfilter";
            this.label_strfilter.Size = new System.Drawing.Size(113, 13);
            this.label_strfilter.TabIndex = 5;
            this.label_strfilter.Text = "סנן עמודה \"מחרוזת\":";
            // 
            // textBox_sort
            // 
            this.textBox_sort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_sort.Location = new System.Drawing.Point(425, 64);
            this.textBox_sort.Multiline = true;
            this.textBox_sort.Name = "textBox_sort";
            this.textBox_sort.ReadOnly = true;
            this.textBox_sort.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_sort.Size = new System.Drawing.Size(187, 80);
            this.textBox_sort.TabIndex = 4;
            // 
            // textBox_filter
            // 
            this.textBox_filter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_filter.Location = new System.Drawing.Point(618, 64);
            this.textBox_filter.Multiline = true;
            this.textBox_filter.Name = "textBox_filter";
            this.textBox_filter.ReadOnly = true;
            this.textBox_filter.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_filter.Size = new System.Drawing.Size(194, 80);
            this.textBox_filter.TabIndex = 3;
            // 
            // label_sort
            // 
            this.label_sort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_sort.AutoSize = true;
            this.label_sort.Location = new System.Drawing.Point(546, 48);
            this.label_sort.Name = "label_sort";
            this.label_sort.Size = new System.Drawing.Size(69, 13);
            this.label_sort.TabIndex = 2;
            this.label_sort.Text = "מחרוזת מיון";
            // 
            // label_filter
            // 
            this.label_filter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_filter.AutoSize = true;
            this.label_filter.Location = new System.Drawing.Point(741, 48);
            this.label_filter.Name = "label_filter";
            this.label_filter.Size = new System.Drawing.Size(74, 13);
            this.label_filter.TabIndex = 1;
            this.label_filter.Text = "מחרוזת סינון";
            // 
            // button_load
            // 
            this.button_load.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_load.Location = new System.Drawing.Point(667, 12);
            this.button_load.Name = "button_load";
            this.button_load.Size = new System.Drawing.Size(145, 23);
            this.button_load.TabIndex = 0;
            this.button_load.Text = "הצג נתונים אקראים";
            this.button_load.UseVisualStyleBackColor = true;
            this.button_load.Click += new System.EventHandler(this.Button_load_Click);
            // 
            // bindingSource_main
            // 
            this.bindingSource_main.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.BindingSource_main_ListChanged);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Controls.Add(this.textBox_total);
            this.panel_bottom.Controls.Add(this.label_total);
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_bottom.Location = new System.Drawing.Point(0, 420);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(827, 44);
            this.panel_bottom.TabIndex = 4;
            // 
            // textBox_total
            // 
            this.textBox_total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_total.Location = new System.Drawing.Point(641, 11);
            this.textBox_total.Name = "textBox_total";
            this.textBox_total.ReadOnly = true;
            this.textBox_total.Size = new System.Drawing.Size(100, 20);
            this.textBox_total.TabIndex = 1;
            // 
            // label_total
            // 
            this.label_total.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_total.AutoSize = true;
            this.label_total.Location = new System.Drawing.Point(747, 14);
            this.label_total.Name = "label_total";
            this.label_total.Size = new System.Drawing.Size(68, 13);
            this.label_total.TabIndex = 0;
            this.label_total.Text = "סה\"כ שורות";
            // 
            // panel_grid
            // 
            this.panel_grid.Controls.Add(this.advancedDataGridView_main);
            this.panel_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_grid.Location = new System.Drawing.Point(0, 205);
            this.panel_grid.Name = "panel_grid";
            this.panel_grid.Size = new System.Drawing.Size(827, 215);
            this.panel_grid.TabIndex = 5;
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
            this.advancedDataGridView_main.Size = new System.Drawing.Size(827, 215);
            this.advancedDataGridView_main.TabIndex = 0;
            this.advancedDataGridView_main.SortStringChanged += new System.EventHandler<Zuby.ADGV.SortEventArgs>(this.AdvancedDataGridView_main_SortStringChanged);
            this.advancedDataGridView_main.FilterStringChanged += new System.EventHandler<Zuby.ADGV.FilterEventArgs>(this.AdvancedDataGridView_main_FilterStringChanged);
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
            this.advancedDataGridViewSearchToolBar_main.Size = new System.Drawing.Size(827, 27);
            this.advancedDataGridViewSearchToolBar_main.TabIndex = 12;
            this.advancedDataGridViewSearchToolBar_main.Text = "advancedDataGridViewSearchToolBar1";
            this.advancedDataGridViewSearchToolBar_main.Search += new Zuby.ADGV.AdvancedDataGridViewSearchToolBarSearchEventHandler(this.AdvancedDataGridViewSearchToolBar_main_Search);
            // 
            // linkFilterHelp
            // 
            this.linkFilterHelp.AutoSize = true;
            this.linkFilterHelp.Location = new System.Drawing.Point(265, 82);
            this.linkFilterHelp.Name = "linkFilterHelp";
            this.linkFilterHelp.Size = new System.Drawing.Size(19, 13);
            this.linkFilterHelp.TabIndex = 25;
            this.linkFilterHelp.TabStop = true;
            this.linkFilterHelp.Text = "(?)";
            this.linkFilterHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkFilterHelp_LinkClicked);
            // 
            // FormMainHeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 464);
            this.Controls.Add(this.panel_grid);
            this.Controls.Add(this.panel_bottom);
            this.Controls.Add(this.panel_top);
            this.Name = "FormMainHeb";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RightToLeftLayout = true;
            this.Text = "דוגמה של AdvancedDataGridView";
            this.Load += new System.EventHandler(this.FormMainHeb_Load);
            this.panel_top.ResumeLayout(false);
            this.panel_top.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource_main)).EndInit();
            this.panel_bottom.ResumeLayout(false);
            this.panel_bottom.PerformLayout();
            this.panel_grid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.advancedDataGridView_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.BindingSource bindingSource_main;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.TextBox textBox_total;
        private System.Windows.Forms.Label label_total;
        private System.Windows.Forms.Panel panel_grid;
        private Zuby.ADGV.AdvancedDataGridView advancedDataGridView_main;
        private System.Windows.Forms.Button button_load;
        private System.Windows.Forms.Label label_sort;
        private System.Windows.Forms.Label label_filter;
        private System.Windows.Forms.Label label_strfilter;
        private System.Windows.Forms.TextBox textBox_sort;
        private System.Windows.Forms.TextBox textBox_filter;
        private System.Windows.Forms.TextBox textBox_strfilter;
        private System.Windows.Forms.Label label_sortsaved;
        private System.Windows.Forms.Label label_filtersaved;
        private System.Windows.Forms.ComboBox comboBox_sortsaved;
        private System.Windows.Forms.ComboBox comboBox_filtersaved;
        private System.Windows.Forms.Button button_unloadfilters;
        private System.Windows.Forms.Button button_savefilters;
        private System.Windows.Forms.Button button_setsavedfilter;
        private System.Windows.Forms.Panel panel1;
        private Zuby.ADGV.AdvancedDataGridViewSearchToolBar advancedDataGridViewSearchToolBar_main;
        private System.Windows.Forms.Button btnEnglish;
        private System.Windows.Forms.Button btnLoadFilter;
        private System.Windows.Forms.TextBox textLoadFilter;
        private System.Windows.Forms.Label labelLoadFilter;
        private System.Windows.Forms.LinkLabel linkFilterHelp;
        private System.Windows.Forms.ToolTip tooltipHelpLink;
    }
}