using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Zuby.ADGV;


namespace AdvancedDataGridViewSample
{
    public partial class FormMainHeb : Form
    {
        private DataTable _dataTable = null;
        private readonly DataSet _dataSet = null;

        private readonly SortedDictionary<int, string> _filtersaved = new SortedDictionary<int, string>();
        private readonly SortedDictionary<int, string> _sortsaved = new SortedDictionary<int, string>();

        public FormMainHeb()
        {
            InitializeComponent();

            //set localization strings
            AdvancedDataGridView.SetTranslations(AdvancedDataGridView.LoadTranslationsFromFile("heb.json"));
            AdvancedDataGridViewSearchToolBar.SetTranslations(AdvancedDataGridViewSearchToolBar.LoadTranslationsFromFile("heb.json"));

            //set filter and sort saved
            _filtersaved.Add(0, "");
            _sortsaved.Add(0, "");
            comboBox_filtersaved.DataSource = new BindingSource(_filtersaved, null);
            comboBox_filtersaved.DisplayMember = "Key";
            comboBox_filtersaved.ValueMember = "Value";
            comboBox_filtersaved.SelectedIndex = -1;
            comboBox_sortsaved.DataSource = new BindingSource(_sortsaved, null);
            comboBox_sortsaved.DisplayMember = "Key";
            comboBox_sortsaved.ValueMember = "Value";
            comboBox_sortsaved.SelectedIndex = -1;

            //initialize dataset
            _dataTable = new DataTable();
            _dataSet = new DataSet();

            //initialize bindingsource
            bindingSource_main.DataSource = _dataSet;

            //initialize datagridview
            advancedDataGridView_main.SetDoubleBuffered();
            advancedDataGridView_main.DataSource = bindingSource_main;

            //set bindingsource
            SetTestData();
        }

        private void Button_load_Click(object sender, EventArgs e)
        {
            //add test data to bindsource
            AddTestData();
        }

        private void SetTestData()
        {
            _dataTable = _dataSet.Tables.Add("TableTest");
            _dataTable.Columns.Add("int", typeof(int));
            _dataTable.Columns.Add("decimal", typeof(decimal));
            _dataTable.Columns.Add("double", typeof(double));
            _dataTable.Columns.Add("date", typeof(DateTime));
            _dataTable.Columns.Add("datetime", typeof(DateTime));
            _dataTable.Columns.Add("string", typeof(string));
            _dataTable.Columns.Add("boolean", typeof(bool));
            _dataTable.Columns.Add("guid", typeof(Guid));
            _dataTable.Columns.Add("image", typeof(Bitmap));
            _dataTable.Columns.Add("timespan", typeof(TimeSpan));

            bindingSource_main.DataMember = _dataTable.TableName;

            advancedDataGridViewSearchToolBar_main.SetColumns(advancedDataGridView_main.Columns);
        }

        private void AddTestData()
        {
            Random r = new Random();
            Image[] sampleimages = new Image[2];
            sampleimages[0] = Image.FromFile(Path.Combine(Application.StartupPath, "flag-green_24.png"));
            sampleimages[1] = Image.FromFile(Path.Combine(Application.StartupPath, "flag-red_24.png"));

            int maxSeconds = (int)TimeSpan.FromDays(10).TotalSeconds;

            for (int i = 0; i <= 100; i++)
            {
                object[] newrow = new object[] {
                    i,
                    (decimal)i*2/3,
                    i % 2 == 0 ? (double)i*2/3 : (double)i/2,
                    DateTime.Today.AddHours(i*2).AddHours(i%2 == 0 ?i*10+1:0).AddMinutes(i%2 == 0 ?i*10+1:0).AddSeconds(i%2 == 0 ?i*10+1:0).AddMilliseconds(i%2 == 0 ?i*10+1:0).Date,
                    DateTime.Today.AddHours(i*2).AddHours(i%2 == 0 ?i*10+1:0).AddMinutes(i%2 == 0 ?i*10+1:0).AddSeconds(i%2 == 0 ?i*10+1:0).AddMilliseconds(i%2 == 0 ?i*10+1:0),
                    i*2 % 3 == 0 ? null : i.ToString()+" מחרוזת",
                    i % 2 == 0 ? true:false,
                    Guid.NewGuid(),
                    sampleimages[r.Next(0, 2)],
                    TimeSpan.FromSeconds(r.Next(maxSeconds))
                };

                _dataTable.Rows.Add(newrow);
            }
        }

        private void FormMainHeb_Load(object sender, EventArgs e)
        {
            //add test data to bindsource
            AddTestData();

            //setup datagridview
            //advancedDataGridView_main.DisableFilterAndSort(advancedDataGridView_main.Columns["int"]);
            advancedDataGridView_main.SetFilterDateAndTimeEnabled(advancedDataGridView_main.Columns["datetime"], true);
            //advancedDataGridView_main.SetSortEnabled(advancedDataGridView_main.Columns["guid"], false);
            //advancedDataGridView_main.SetFilterChecklistEnabled(advancedDataGridView_main.Columns["guid"], false);
            advancedDataGridView_main.SortDESC(advancedDataGridView_main.Columns["double"]);
            advancedDataGridView_main.SetTextFilterRemoveNodesOnSearch(advancedDataGridView_main.Columns["double"], false);
            advancedDataGridView_main.SetChecklistTextFilterRemoveNodesOnSearchMode(advancedDataGridView_main.Columns["decimal"], false);
            //advancedDataGridView_main.SetFilterChecklistEnabled(advancedDataGridView_main.Columns["double"], false);
        }

        private void AdvancedDataGridView_main_FilterStringChanged(object sender, FilterEventArgs e)
        {
            //eventually set the FilterString here
            //if e.Cancel is set to true one have to update the datasource here using
            //bindingSource_main.Filter = advancedDataGridView_main.FilterString;
            //otherwise it will be updated by the component

            //sample use of the override string filter
            string stringcolumnfilter = textBox_strfilter.Text;
            if (!String.IsNullOrEmpty(stringcolumnfilter))
                e.FilterString += (!String.IsNullOrEmpty(e.FilterString) ? " AND " : "") + String.Format("string LIKE '%{0}%'", stringcolumnfilter.Replace("'", "''"));

            textBox_filter.Text = e.FilterString;
        }

        private void AdvancedDataGridView_main_SortStringChanged(object sender, SortEventArgs e)
        {
            //eventually set the SortString here
            //if e.Cancel is set to true one have to update the datasource here
            //bindingSource_main.Sort = advancedDataGridView_main.SortString;
            //otherwise it will be updated by the component

            textBox_sort.Text = e.SortString;
        }

        private void TextBox_strfilter_TextChanged(object sender, EventArgs e)
        {
            //trigger the filter string changed function when text is changed
            advancedDataGridView_main.TriggerFilterStringChanged();

        }

        private void BindingSource_main_ListChanged(object sender, ListChangedEventArgs e)
        {
            textBox_total.Text = bindingSource_main.List.Count.ToString();
        }

        private void Button_savefilters_Click(object sender, EventArgs e)
        {
            _filtersaved.Add((comboBox_filtersaved.Items.Count - 1) + 1, advancedDataGridView_main.FilterString);
            comboBox_filtersaved.DataSource = new BindingSource(_filtersaved, null);
            comboBox_filtersaved.SelectedIndex = comboBox_filtersaved.Items.Count - 1;
            _sortsaved.Add((comboBox_sortsaved.Items.Count - 1) + 1, advancedDataGridView_main.SortString);
            comboBox_sortsaved.DataSource = new BindingSource(_sortsaved, null);
            comboBox_sortsaved.SelectedIndex = comboBox_sortsaved.Items.Count - 1;

        }

        private void Button_setsavedfilter_Click(object sender, EventArgs e)
        {
            if (comboBox_filtersaved.SelectedIndex != -1 && comboBox_sortsaved.SelectedIndex != -1)
                advancedDataGridView_main.LoadFilterAndSort(comboBox_filtersaved.SelectedValue.ToString(), comboBox_sortsaved.SelectedValue.ToString());

        }

        private void Button_unloadfilters_Click(object sender, EventArgs e)
        {
            advancedDataGridView_main.CleanFilterAndSort();
            comboBox_filtersaved.SelectedIndex = -1;
            comboBox_sortsaved.SelectedIndex = -1;

        }

        private void AdvancedDataGridViewSearchToolBar_main_Search(object sender, AdvancedDataGridViewSearchToolBarSearchEventArgs e)
        {
            bool restartsearch = true;
            int startColumn = 0;
            int startRow = 0;
            if (!e.FromBegin)
            {
                bool endcol = advancedDataGridView_main.CurrentCell.ColumnIndex + 1 >= advancedDataGridView_main.ColumnCount;
                bool endrow = advancedDataGridView_main.CurrentCell.RowIndex + 1 >= advancedDataGridView_main.RowCount;

                if (endcol && endrow)
                {
                    startColumn = advancedDataGridView_main.CurrentCell.ColumnIndex;
                    startRow = advancedDataGridView_main.CurrentCell.RowIndex;
                }
                else
                {
                    startColumn = endcol ? 0 : advancedDataGridView_main.CurrentCell.ColumnIndex + 1;
                    startRow = advancedDataGridView_main.CurrentCell.RowIndex + (endcol ? 1 : 0);
                }
            }
            DataGridViewCell c = advancedDataGridView_main.FindCell(
                e.ValueToSearch,
                e.ColumnToSearch?.Name,
                startRow,
                startColumn,
                e.WholeWord,
                e.CaseSensitive);
            if (c == null && restartsearch)
                c = advancedDataGridView_main.FindCell(
                    e.ValueToSearch,
                    e.ColumnToSearch?.Name,
                    0,
                    0,
                    e.WholeWord,
                    e.CaseSensitive);
            if (c != null)
                advancedDataGridView_main.CurrentCell = c;
        }

        private void BtnEnglish_Click(object sender, EventArgs e)
        {
            (new FormMain()).Show();

        }

        private void BtnLoadFilter_Click(object sender, EventArgs e)
        {
            string filter = textLoadFilter.Text.Trim();
            advancedDataGridView_main.LoadFilter(filter);
            if (textBox_filter.Text == filter)
            {
                MessageBox.Show("Success!");
            }
            else
            {
                MessageBox.Show("Filter mismatch");
            }
        }
    }
}
