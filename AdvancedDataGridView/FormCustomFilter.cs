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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Zuby.ADGV
{
    internal partial class FormCustomFilter : Form
    {

        #region class properties

        private enum FilterType
        {
            Unknown,
            DateTime,
            TimeSpan,
            String,
            Float,
            Integer
        }

        private readonly FilterType _filterType = FilterType.Unknown;
        private readonly Control _valControl1 = null;
        private readonly Control _valControl2 = null;

        private readonly bool _filterDateAndTimeEnabled = true;

        #endregion


        #region constructors

        /// <summary>
        /// Main constructor
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="filterDateAndTimeEnabled"></param>
        public FormCustomFilter(Type dataType, bool filterDateAndTimeEnabled)
            : base()
        {
            //initialize components
            InitializeComponent();

            //set component translations
            this.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFormTitle.ToString()];
            this.label_columnName.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLabelColumnNameText.ToString()];
            this.label_and.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLabelAnd.ToString()];
            this.button_ok.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVButtonOk.ToString()];
            this.button_cancel.Text = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVButtonCancel.ToString()];

            if (dataType == typeof(DateTime))
                _filterType = FilterType.DateTime;
            else if (dataType == typeof(TimeSpan))
                _filterType = FilterType.TimeSpan;
            else if (dataType == typeof(Int32) || dataType == typeof(Int64) || dataType == typeof(Int16) ||
                    dataType == typeof(UInt32) || dataType == typeof(UInt64) || dataType == typeof(UInt16) ||
                    dataType == typeof(Byte) || dataType == typeof(SByte))
                _filterType = FilterType.Integer;
            else if (dataType == typeof(Single) || dataType == typeof(Double) || dataType == typeof(Decimal))
                _filterType = FilterType.Float;
            else if (dataType == typeof(String))
                _filterType = FilterType.String;
            else
                _filterType = FilterType.Unknown;

            _filterDateAndTimeEnabled = filterDateAndTimeEnabled;

            switch (_filterType)
            {
                case FilterType.DateTime:
                    _valControl1 = new DateTimePicker();
                    _valControl2 = new DateTimePicker();
                    if (_filterDateAndTimeEnabled)
                    {
                        DateTimeFormatInfo dt = Thread.CurrentThread.CurrentCulture.DateTimeFormat;

                        (_valControl1 as DateTimePicker).CustomFormat = dt.ShortDatePattern + " " + "HH:mm";
                        (_valControl2 as DateTimePicker).CustomFormat = dt.ShortDatePattern + " " + "HH:mm";
                        (_valControl1 as DateTimePicker).Format = DateTimePickerFormat.Custom;
                        (_valControl2 as DateTimePicker).Format = DateTimePickerFormat.Custom;
                    }
                    else
                    {
                        (_valControl1 as DateTimePicker).Format = DateTimePickerFormat.Short;
                        (_valControl2 as DateTimePicker).Format = DateTimePickerFormat.Short;
                    }

                    comboBox_filterType.Items.AddRange(new string[] {
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEquals.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEqual.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEarlierThan.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEarlierThanOrEqualTo.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLaterThan.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLaterThanOrEqualTo.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVBetween.ToString()]
                    });
                    break;

                case FilterType.TimeSpan:
                    _valControl1 = new TextBox();
                    _valControl2 = new TextBox();
                    comboBox_filterType.Items.AddRange(new string[] {
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVContains.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotContain.ToString()]
                    });
                    break;

                case FilterType.Integer:
                case FilterType.Float:
                    _valControl1 = new TextBox();
                    _valControl2 = new TextBox();
                    _valControl1.TextChanged += ValControl_TextChanged;
                    _valControl2.TextChanged += ValControl_TextChanged;
                    comboBox_filterType.Items.AddRange(new string[] {
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEquals.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEqual.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVGreaterThan.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVGreaterThanOrEqualTo.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLessThan.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLessThanOrEqualTo.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVBetween.ToString()]
                    });
                    _valControl1.Tag = true;
                    _valControl2.Tag = true;
                    button_ok.Enabled = false;
                    break;

                default:
                    _valControl1 = new TextBox();
                    _valControl2 = new TextBox();
                    comboBox_filterType.Items.AddRange(new string[] {
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEquals.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEqual.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVBeginsWith.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotBeginWith.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEndsWith.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEndWith.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVContains.ToString()],
                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotContain.ToString()]
                    });
                    break;
            }
            comboBox_filterType.SelectedIndex = 0;

            _valControl1.Name = "valControl1";
            _valControl1.Location = new System.Drawing.Point(30, 66);
            _valControl1.Size = new System.Drawing.Size(166, 20);
            _valControl1.TabIndex = 4;
            _valControl1.Visible = true;
            _valControl1.KeyDown += ValControl_KeyDown;

            _valControl2.Name = "valControl2";
            _valControl2.Location = new System.Drawing.Point(30, 108);
            _valControl2.Size = new System.Drawing.Size(166, 20);
            _valControl2.TabIndex = 5;
            _valControl2.Visible = false;
            _valControl2.VisibleChanged += new EventHandler(ValControl2_VisibleChanged);
            _valControl2.KeyDown += ValControl_KeyDown;

            Controls.Add(_valControl1);
            Controls.Add(_valControl2);

            errorProvider.SetIconAlignment(_valControl1, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(_valControl1, -18);
            errorProvider.SetIconAlignment(_valControl2, ErrorIconAlignment.MiddleRight);
            errorProvider.SetIconPadding(_valControl2, -18);
        }

        #endregion


        #region public filter methods

        /// <summary>
        /// Get the Filter string
        /// </summary>
        public string FilterString { get; private set; } = null;

        /// <summary>
        /// Get the Filter string description
        /// </summary>
        public string FilterStringDescription { get; private set; } = null;

        #endregion


        #region filter parser
        /// <summary>
        /// Try parse filter string for custom filter
        /// </summary>
        /// <param name="dataType"></param>
        /// <param name="filterDateAndTimeEnabled"></param>
        /// <param name="filter"></param>
        /// <param name="visualFilter"></param>
        /// <returns></returns>
        public static bool TryParse(Type dataType, bool filterDateAndTimeEnabled, string filter, out string visualFilter)
        {
            visualFilter = null;
            string valueText, condition;
            if (dataType == typeof(DateTime))
            {
                #region // Parse DateTime values
                // Equality condition
                Match match = Regex.Match(filter, @"^(?n)Convert\(\[\w+\]\,\s*\'System\.String'\)\s+(?<not>NOT\s)?LIKE\s+\'\%(?<value>[^%]+)\%\'$");
                if (match.Success)
                {
                    // Validate value
                    valueText = match.Groups["value"].Value;
                    if (DateTime.TryParse(valueText, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime dateTime))
                    {
                        // if time part is not enabled, filter can't contain time
                        if (!filterDateAndTimeEnabled && dateTime.TimeOfDay.TotalHours != 0)
                            return false;
                    }
                    else
                        return false;
                    // equal / not equal
                    if (match.Groups["not"].Success)
                    {
                        // not equal
                        condition = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEqual.ToString()];
                    }
                    else
                    {
                        // equal
                        condition = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEquals.ToString()];
                    }
                    visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()], condition, valueText);

                }
                else
                {
                    // Between
                    match = Regex.Match(filter, @"^\[(\w+)\]\s+>=\s+\'(?<val1>[^']+)\'\s+AND\s+\[\1\]\s+<=\s+\'(?<val2>[^']+)\'$");
                    if (match.Success)
                    {
                        // Validate values
                        valueText = match.Groups["val1"].Value;
                        if (DateTime.TryParse(valueText, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime dateTime))
                        {
                            // if time part is not enabled, filter can't contain time
                            if (!filterDateAndTimeEnabled && dateTime.TimeOfDay.TotalHours != 0)
                                return false;
                        }
                        else
                            return false;
                        string val2 = match.Groups["val2"].Value;
                        if (DateTime.TryParse(val2, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime dt))
                        {
                            // if time part is not enabled, filter can't contain time
                            if (!filterDateAndTimeEnabled && dt.TimeOfDay.TotalHours != 0)
                                return false;
                        }
                        else
                            return false;
                        // 
                        visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                     AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVBetween.ToString()], valueText) + " " + 
                                                     AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLabelAnd.ToString()] + " \"" + val2 + "\"";
                    }
                    else
                    {
                        // Inequalities
                        match = Regex.Match(filter, @"^\[\w+\]\s+(?<compare><|<=|>|>=)\s+\'(?<value>[^']+)\'$");
                        if (match.Success)
                        {
                            // Validate value
                            valueText = match.Groups["value"].Value;
                            if (DateTime.TryParse(valueText, CultureInfo.CurrentCulture, DateTimeStyles.AssumeLocal, out DateTime dateTime))
                            {
                                // if time part is not enabled, filter can't contain time
                                if (!filterDateAndTimeEnabled && dateTime.TimeOfDay.TotalHours != 0)
                                    return false;
                            }
                            else
                                return false;
                            // Validate comparison
                            switch (match.Groups["compare"].Value)
                            {
                                case "<":
                                    condition = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEarlierThan.ToString()];
                                    break;
                                case "<=":
                                    condition = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEarlierThanOrEqualTo.ToString()];
                                    break;
                                case ">":
                                    condition = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLaterThan.ToString()];
                                    break;
                                case ">=":
                                    condition = AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLaterThanOrEqualTo.ToString()];
                                    break;
                                default:
                                    condition = null;
                                    break;
                            }
                            if (string.IsNullOrEmpty(condition))
                                return false;
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()], condition, valueText);
                        }
                        else
                        {
                            // total failure
                            return false;
                        }
                    }
                }
                #endregion 
            }
            else if (dataType == typeof(TimeSpan))
            {
                #region // Parse TimeSpan values
                Match match = Regex.Match(filter, 
                                            @"^\(?Convert\(\[\w+\]\,\s*\'System\.String'\)\s+(?<not>NOT\s)?LIKE\s+\'\%(?<value>[-]?P(?!$)(\d+D)?(T(?=\d)(\d+H)?(\d+M)?(\d+(?:\.\d+)?S)?)?)\%\'\)?$");
                if (match.Success && match.Groups["value"].Success)
                {
                    try
                    {
                        TimeSpan ts = XmlConvert.ToTimeSpan(match.Groups["value"].Value);
                        if (match.Groups["not"].Success)
                        {   // NOT LIKE logic
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()], 
                                                         AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotContain.ToString()], 
                                                         ts);
                        }
                        else
                        {
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                         AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVContains.ToString()],
                                                         ts);
                        }
                    }
                    catch (FormatException ex)
                    {
                        // failed to accept
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        return false;
                    }
                }
                else
                {
                    // total failure
                    return false;
                }
                #endregion
            }
            else if (dataType == typeof(Int32) || dataType == typeof(Int64) || dataType == typeof(Int16) ||
                    dataType == typeof(UInt32) || dataType == typeof(UInt64) || dataType == typeof(UInt16) ||
                    dataType == typeof(Byte) || dataType == typeof(SByte) || dataType == typeof(Single) ||
                    dataType == typeof(Double) || dataType == typeof(Decimal))
            {
                #region // Parse numeric data types
                Match match = Regex.Match(filter,
                                          @"^\[\w+\]\s+(?<operator>=|<>|>|>=|<|<=)\s+(?<value>\d+(?:\.\d+)?)(?<between>\s+AND\s+\[\w+\]\s+<=\s+(?<value2>\d+(?:\.\d+)?))?$");
                if (match.Success)
                {
                    string value = match.Groups["value"].Value;
                    switch (match.Groups["operator"].Value)
                    {
                        case "=":
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                         AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEquals.ToString()],
                                                         value);
                            break;
                        case "<>":
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                         AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEqual.ToString()],
                                                         value);
                            break;
                        case ">":
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                         AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVGreaterThan.ToString()],
                                                         value);

                            break;
                        case ">=":
                            if (match.Groups["value2"].Success)
                            {   // between
                                visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                             AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVBetween.ToString()],
                                                             value) + " "
                                                             + AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLabelAnd.ToString()]
                                                             + " \"" + match.Groups["value2"] + "\"";
                            }
                            else
                            {
                                visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                             AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVGreaterThanOrEqualTo.ToString()],
                                                             value);

                            }
                            break;
                        case "<":
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                         AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLessThan.ToString()],
                                                         value);
                            break;
                        case "<=":
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                         AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLessThanOrEqualTo.ToString()],
                                                         value);
                            break;
                        default:
                            return false;
                    }
                }
                #endregion
            }
            else if (dataType == typeof(String))
            {
                #region // Parse string
                Match match = Regex.Match(filter,
                                @"^\[\w+\]\s+(?<not>NOT\s+)?LIKE\s+\'(?<sall>%)?(?<value>.+?(?=%?\'$))(?<eall>%)?\'$");
                if (match.Success)
                {
                    string value = match.Groups["value"].Value;
                    value = value.Replace("''", "'");
                    value = Regex.Replace(value, "\\[(?<spchar>\\%|\\[|\\]|\\*|\\\\{1,2})\\]", "${spchar}");
                    bool sall = match.Groups["sall"].Success;
                    bool eall = match.Groups["eall"].Success;
                    if (match.Groups["not"].Success)
                    {
                        if (sall && eall)
                        {
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotContain.ToString()],
                                                        value);
                        }
                        else if(sall)
                        {
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEndWith.ToString()],
                                                        value);
                        }
                        else if(eall)
                        {
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotBeginWith.ToString()],
                                                        value);
                        }
                        else
                        {
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEqual.ToString()],
                                                        value);
                        }
                    }
                    else
                    {
                        if (sall && eall)
                        {
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVContains.ToString()],
                                                        value);
                        }
                        else if (sall)
                        {
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEndsWith.ToString()],
                                                        value);
                        }
                        else if (eall)
                        {
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVBeginsWith.ToString()],
                                                        value);
                        }
                        else
                        {
                            visualFilter = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()],
                                                        AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEquals.ToString()],
                                                        value);
                        }
                    }
                }
                else
                {
                    return false;
                }
                #endregion 
            }
            // Any other type is unsupported in custom filter (bool, for example)
            return !string.IsNullOrEmpty(visualFilter);
        }

        #endregion 


        #region filter builder

        /// <summary>
        /// Build a Filter string
        /// </summary>
        /// <param name="filterType"></param>
        /// <param name="filterDateAndTimeEnabled"></param>
        /// <param name="filterTypeConditionText"></param>
        /// <param name="control1"></param>
        /// <param name="control2"></param>
        /// <returns></returns>
        private string BuildCustomFilter(FilterType filterType, bool filterDateAndTimeEnabled, string filterTypeConditionText, Control control1, Control control2)
        {

            string column = "[{0}] ";

            if (filterType == FilterType.Unknown)
                column = "Convert([{0}], 'System.String') ";

            string filterString = column;

            switch (filterType)
            {
                case FilterType.DateTime:
                    DateTime dt = ((DateTimePicker)control1).Value;
                    if (filterDateAndTimeEnabled)
                    {
                        dt = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
                    }
                    else
                    {
                        dt = new DateTime(dt.Year, dt.Month, dt.Day);
                    }
                    if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEquals.ToString()])
                        filterString = "Convert([{0}], 'System.String') LIKE '%" + Convert.ToString(dt, CultureInfo.CurrentCulture) + "%'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEarlierThan.ToString()])
                        filterString += "< '" + Convert.ToString(dt, CultureInfo.CurrentCulture) + "'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEarlierThanOrEqualTo.ToString()])
                        filterString += "<= '" + Convert.ToString(dt, CultureInfo.CurrentCulture) + "'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLaterThan.ToString()])
                        filterString += "> '" + Convert.ToString(dt, CultureInfo.CurrentCulture) + "'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLaterThanOrEqualTo.ToString()])
                        filterString += ">= '" + Convert.ToString(dt, CultureInfo.CurrentCulture) + "'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVBetween.ToString()])
                    {
                        DateTime dt1 = ((DateTimePicker)control2).Value;
                        if (filterDateAndTimeEnabled)
                        {
                            dt1 = new DateTime(dt1.Year, dt1.Month, dt1.Day, dt1.Hour, dt1.Minute, 0);
                        }
                        else
                        {
                            dt1 = new DateTime(dt1.Year, dt1.Month, dt1.Day);
                        }
                        filterString += ">= '" + Convert.ToString(dt, CultureInfo.CurrentCulture) + "'";
                        filterString += " AND " + column + "<= '" + Convert.ToString(dt1, CultureInfo.CurrentCulture) + "'";
                    }
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEqual.ToString()])
                        filterString = "Convert([{0}], 'System.String') NOT LIKE '%" + Convert.ToString(dt, CultureInfo.CurrentCulture) + "%'";
                    break;

                case FilterType.TimeSpan:
                    if(TimeSpan.TryParse(control1.Text, out TimeSpan ts))
                    {
                        // Convert timespan to ISO 8601 duration format
                        string tsValue = XmlConvert.ToString(ts);
                        // 
                        if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVContains.ToString()])
                        {
                            filterString = "Convert([{0}], 'System.String') LIKE '%" + tsValue + "%'";
                        }
                        else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotContain.ToString()])
                        {
                            filterString = "Convert([{0}], 'System.String') NOT LIKE '%" + tsValue + "%'";
                        }
                    }
                    else
                    {
                        filterString = null;
                    }
                    break;

                case FilterType.Integer:
                case FilterType.Float:

                    string num = control1.Text;

                    if (filterType == FilterType.Float)
                        num = num.Replace(",", ".");

                    if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEquals.ToString()])
                        filterString += "= " + num;
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVBetween.ToString()])
                        filterString += ">= " + num + " AND " + column + "<= " + (filterType == FilterType.Float ? control2.Text.Replace(",", ".") : control2.Text);
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEqual.ToString()])
                        filterString += "<> " + num;
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVGreaterThan.ToString()])
                        filterString += "> " + num;
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVGreaterThanOrEqualTo.ToString()])
                        filterString += ">= " + num;
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLessThan.ToString()])
                        filterString += "< " + num;
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVLessThanOrEqualTo.ToString()])
                        filterString += "<= " + num;
                    break;

                default:
                    string txt = FormatFilterString(control1.Text);
                    if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEquals.ToString()])
                        filterString += "LIKE '" + txt + "'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEqual.ToString()])
                        filterString += "NOT LIKE '" + txt + "'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVBeginsWith.ToString()])
                        filterString += "LIKE '" + txt + "%'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVEndsWith.ToString()])
                        filterString += "LIKE '%" + txt + "'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotBeginWith.ToString()])
                        filterString += "NOT LIKE '" + txt + "%'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotEndWith.ToString()])
                        filterString += "NOT LIKE '%" + txt + "'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVContains.ToString()])
                        filterString += "LIKE '%" + txt + "%'";
                    else if (filterTypeConditionText == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVDoesNotContain.ToString()])
                        filterString += "NOT LIKE '%" + txt + "%'";
                    break;
            }

            return filterString;
        }

        /// <summary>
        /// Format a text Filter string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string FormatFilterString(string text)
        {
            string result = "";
            string s;
            string[] replace = { "%", "[", "]", "*", "\"", "\\" };

            for (int i = 0; i < text.Length; i++)
            {
                s = text[i].ToString();
                if (replace.Contains(s))
                    result += "[" + s + "]";
                else
                    result += s;
            }

            return result.Replace("'", "''");
        }


        #endregion


        #region buttons events

        /// <summary>
        /// Button Cancel Clieck
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_cancel_Click(object sender, EventArgs e)
        {
            FilterStringDescription = null;
            FilterString = null;
            Close();
        }

        /// <summary>
        /// Button OK Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_ok_Click(object sender, EventArgs e)
        {
            if ((_valControl1.Visible && _valControl1.Tag != null && ((bool)_valControl1.Tag)) ||
                (_valControl2.Visible && _valControl2.Tag != null && ((bool)_valControl2.Tag)))
            {
                button_ok.Enabled = false;
                return;
            }

            string filterString = BuildCustomFilter(_filterType, _filterDateAndTimeEnabled, comboBox_filterType.Text, _valControl1, _valControl2);

            if (!String.IsNullOrEmpty(filterString))
            {
                FilterString = filterString;
                FilterStringDescription = String.Format(AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVFilterStringDescription.ToString()], comboBox_filterType.Text, _valControl1.Text);
                if (_valControl2.Visible)
                    FilterStringDescription += " " + label_and.Text + " \"" + _valControl2.Text + "\"";
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
            {
                FilterString = null;
                FilterStringDescription = null;
                DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }

            Close();
        }

        #endregion


        #region changed status events

        /// <summary>
        /// Changed condition type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_filterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _valControl2.Visible = comboBox_filterType.Text == AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVBetween.ToString()];
            button_ok.Enabled = !(_valControl1.Visible && _valControl1.Tag != null && ((bool)_valControl1.Tag)) ||
                (_valControl2.Visible && _valControl2.Tag != null && ((bool)_valControl2.Tag));
        }

        /// <summary>
        /// Changed control2 visibility
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValControl2_VisibleChanged(object sender, EventArgs e)
        {
            label_and.Visible = _valControl2.Visible;
        }

        /// <summary>
        /// Changed a control Text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValControl_TextChanged(object sender, EventArgs e)
        {
            bool hasErrors = false;
            switch (_filterType)
            {
                case FilterType.Integer:
                    hasErrors = !Int64.TryParse((sender as TextBox).Text, out _);   // Don't need parsed value, so use discard
                    break;
                case FilterType.Float:
                    hasErrors = !Double.TryParse((sender as TextBox).Text, out _);  // Don't need parsed value, so use discard
                    break;
            }

            (sender as Control).Tag = hasErrors || (sender as TextBox).Text.Length == 0;

            if (hasErrors && (sender as TextBox).Text.Length > 0)
                errorProvider.SetError((sender as Control), AdvancedDataGridView.Translations[AdvancedDataGridView.TranslationKey.ADGVInvalidValue.ToString()]);
            else
                errorProvider.SetError((sender as Control), "");

            button_ok.Enabled = !(_valControl1.Visible && _valControl1.Tag != null && ((bool)_valControl1.Tag)) ||
                (_valControl2.Visible && _valControl2.Tag != null && ((bool)_valControl2.Tag));
        }

        /// <summary>
        /// KeyDown on a control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (sender == _valControl1)
                {
                    if (_valControl2.Visible)
                        _valControl2.Focus();
                    else
                        Button_ok_Click(button_ok, new EventArgs());
                }
                else
                {
                    Button_ok_Click(button_ok, new EventArgs());
                }

                e.SuppressKeyPress = false;
                e.Handled = true;
            }
        }

        #endregion

    }
}