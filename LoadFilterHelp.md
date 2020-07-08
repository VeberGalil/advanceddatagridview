#AdvancedDataGridView.LoadFilter(String) method
Sets the filter to be used to exclude items from the collection of items returned by the data source.

    public void LoadFilter(string filter);

###Parameters
*filter*   
<table><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;</td><td>The string used to filter items out in the item collection returned by the data source.</td></tr></table>


###Remarks
LoadFilter method allows programmatically apply filter to grid (and underlying data source), as if it was selected 
by end user in filter menus for ADVG columns.

AdvancedDataGridView will parse provided filter string, splitting it to filters for individual columns. 
Each column filter will be tested in compliance with column data type.
If column filter is recognized as custom filter, it will be added to list of custom filters for this column.
If filter is not custom filter, AdvancedDataGridView will try to use it as value source for checklist filter.
In any case, control will build new filter string according to recognized filter parts and generate 
FilterStringChanged event, passing new filter string in FilterString property of FilterEventArgs parameter.
AdvancedDataGridView will also apply generated filter to binding source associated with DataSource property.        

        
###Filter String Syntax
Filter string may consist of one or more column filters, delimited with AND:   

<table><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;</td><td>(<i>column_filter</i>) [AND (<i>column_filter</i>) ...]</td></tr></table>

In general, column filter consists of column name, relational operator and data value expression.

<i>Column name</i> should always be enclosed in square brackets. In may include uppercase or lowercase characters, 
digits and underscore (_) character. For example: <code>[My_column_1]</code>.

Available <i>relational operators</i> depend on column data type and filter type (custom or checklist filter), 
as described later in this manual.

<i>Data value expression</i> may be single value, value range (for BETWEEN operator) or comma-separated list 
of values. Data value expression for custom filter may also contain wildcard character % (any string of zero 
or more characters), as described later in this manual.

The exact syntax of column filter depends on column data type, and differs between custom filter and checklist 
filter for the same column.

If column data type allows 'empty' values (`null`, `DbNull.Value` or `String.Empty`), following syntax may be used to select these values in column:

<table><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;</td><td>[<i>column_name</i>] IS NULL</td></tr></table>

Following are column filter syntax and rules for different data types.

####DateTime
#####Custom filter

<table>
  <thead>
    <tr style="text-align:left"><th style="width: 150px;">Relation</th><th>Syntax</th></tr>
  </thead>
  <tbody style="padding:7px">
    <tr>
	  <td class="auto-style1">Equal</td>
	  <td><pre><code>Convert(<i>column_name</i>,'System.String') LIKE '%<i>value</i>%'</code></pre></td>
	</tr>
	<tr>
      <td class="auto-style1">Not equal</td>
      <td><pre><code>Convert(<i>column_name</i>,'System.String') NOT LIKE '%<i>value</i>%'</code></pre></td>
	</tr>
	<tr>
      <td class="auto-style1">Less then</td>
      <td><pre><code><i>column_name</i> &lt; '<i>value</i>'</code></pre></td>
	</tr>
	<tr>
      <td class="auto-style1">Less then or equal</td>
      <td><pre><code><i>column_name</i> &lt;= '<i>value</i>'</code></pre></td>
	</tr>
	<tr>
      <td class="auto-style1">Greater then</td>
      <td><pre><code><i>column_name</i> &gt; '<i>value</i>'</code></pre></td>
	</tr>
	<tr>
      <td class="auto-style1">Greater then or equal</td>
      <td><pre><code><i>column_name</i> &gt;= '<i>value</i>'</code></pre></td>
	</tr>
	<tr>
      <td class="auto-style1">Between</td>
      <td><pre><code><i>column_name</i> &gt;= '<i>value1</i>' AND <i>column_name</i> &lt;= '<i>value2</i>'</code></pre></td>
	</tr>
  </tbody>
</table>
#####Checklist filter
Checklist filter may consist of one or more equality filters, separated with OR:

<table><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;</td><td>Convert(<i>column_name</i>,'System.String') LIKE '%<i>value</i>%' [ OR Convert(<i>column_name</i>,'System.String') LIKE '%<i>value</i>%' ...]</td></tr></table>

####TimeStamp
#####Custom filter
TimeStamp values must conform to the W3C XML Schema Part 2: Datatypes recommendation for duration.

<table>
	<thead>
		<tr style="text-align:left"><th style="width:100px">Relation</th><th>Syntax</th></tr>
	</thead>
	<tbody style="padding:7px">
		<tr>
			<td>Equal</td>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') LIKE '%<i>value</i>%'</code></pre></td>
		</tr>
		<tr>
			<td>Not equal</td>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') NOT LIKE '%<i>value</i>%'</code></pre></td>
		</tr>
	</tbody>
</table>

#####Checklist filter
Checklist filter may consist of one or more equality filters, separated with OR:

<table><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;</td><td>Convert(<i>column_name</i>,'System.String') LIKE '%<i>value</i>%' [ OR Convert(<i>column_name</i>,'System.String') LIKE '%<i>value</i>%' ...]</td></tr></table>


####Int32, Int64, Int16, UInt32, UInt64, UInt16, Byte, SByte and Decimal
#####Custom filter

<table>
	<thead>
		<tr style="text-align:left"><th style="width:150px">Relation</th><th>Syntax</th></tr>
	</thead>
	<tbody style="padding:7px">
		<tr>
			<td>Equal</td>
			<td><pre><code><i>column_name</i> = <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Not equal</td>
			<td><pre><code><i>column_name</i> <> <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Less then</td>
			<td><pre><code><i>column_name</i> &lt; <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Less then or equal</td>
			<td><pre><code><i>column_name</i> &lt;= <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Greater then</td>
			<td><pre><code><i>column_name</i> &gt; <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Greater then or equal</td>
			<td><pre><code><i>column_name</i> &gt;= <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Between</td>
			<td><pre><code><i>column_name</i> &gt;= <i>value1</i> AND <i>column_name</i> &lt;= <i>value2</i></code></pre></td>
		</tr>
	</tbody>
</table>

#####Checklist filter

<table>
	<thead>
		<tr style="text-align:left">
			<th>Syntax</th>
			<th>Description</th>
		</tr>
	</thead>
	<tbody style="padding:7px">
		<tr>
			<td><pre><code><i>column_name</i> IN (<i>value1</i>[,<i>value2</i> ...])</code></pre></td>
			<td>Rows with listed values are shown in control</td>
		</tr>
		<tr>
			<td><pre><code><i>column_name</i> NOT IN (<i>value1</i>[,<i>value2</i> ...])</code></pre></td>
			<td>Rows with listed values are hidden in control</td>
		</tr>
	</tbody>
</table>
        
####Single and Double
#####Custom filter

<table>
	<thead>
		<tr style="text-align:left"><th style="width:150px">Relation</th><th>Syntax</th></tr>
	</thead>
	<tbody style="padding:7px">
		<tr>
			<td>Equal</td>
			<td><pre><code><i>column_name</i> = <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Not equal</td>
			<td><pre><code><i>column_name</i> <> <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Less then</td>
			<td><pre><code><i>column_name</i> &lt; <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Less then or equal</td>
			<td><pre><code><i>column_name</i> &lt;= <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Greater then</td>
			<td><pre><code><i>column_name</i> &gt; <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Greater then or equal</td>
			<td><pre><code><i>column_name</i> &gt;= <i>value</i></code></pre></td>
		</tr>
		<tr>
			<td>Between</td>
			<td><pre><code><i>column_name</i> &gt;= <i>value1</i> AND <i>column_name</i> &lt;= <i>value2</i></code></pre></td>
		</tr>
	</tbody>
</table>

#####Checklist filter

<table>
	<thead>
		<tr style="text-align:left">
			<th style="width:400px">Description</th>
			<th>Syntax</th>
		</tr>
	</thead>
	<tbody style="padding:7px">
		<tr>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') IN (<i>value1</i>[,<i>value2</i> ...])</code></pre></td>
			<td>Rows with listed values are shown in control</td>
		</tr>
		<tr>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') NOT IN (<i>value1</i>[,<i>value2</i> ...])</code></pre></td>
			<td>Rows with listed values are hidden in control</td>
		</tr>
	</tbody>
</table>

####String
If string value, used in either custom or checklist filter, contains apostroph (') character,
this character must be doubled in filter. For example, to show only rows that contain value "that's it"
in column Column1, use: 

<table><tr><td>&nbsp;&nbsp;&nbsp;&nbsp;</td><td>Column1 LIKE '%that''s it%'</td></tr></table>

#####Custom filter

<table>
	<thead>
		<tr style="text-align:left"><th style="width:150px">Relation</th><th>Syntax</th></tr>
	</thead>
	<tbody style="padding:7px">
		<tr>
			<td>Equals</td>
			<td><pre><code><i>column_name</i> LIKE '<i>value</i>'</code></pre></td>
		</tr>
		<tr>
			<td>Not equal</td>
			<td><pre><code><i>column_name</i> NOT LIKE '<i>value</i>'</code></pre></td>
		</tr>
		<tr>
			<td>Begins with</td>
			<td><pre><code><i>column_name</i> LIKE '%<i>value</i>'</code></pre></td>
		</tr>
		<tr>
			<td>Not begins with</td>
			<td><pre><code><i>column_name</i> NOT LIKE '%<i>value</i>'</code></pre></td>
		</tr>
		<tr>
			<td>Ends with</td>
			<td><pre><code><i>column_name</i> LIKE '<i>value</i>%'</code></pre></td>
		</tr>
		<tr>
			<td>Not ends with</td>
			<td><pre><code><i>column_name</i> NOT LIKE '<i>value</i>%'</code></pre></td>
		</tr>
		<tr>
			<td>Contains</td>
			<td><pre><code><i>column_name</i> LIKE '%<i>value</i>%'</code></pre></td>
		</tr>
		<tr>
			<td>Not contain</td>
			<td><pre><code><i>column_name</i> NOT LIKE '%<i>value</i>%'</code></pre></td>
		</tr>
	</tbody>
</table>

#####Checklist filter

<table>
	<thead>
		<tr style="text-align:left">
			<th>Syntax</th>
			<th>Description</th>
		</tr>
	</thead>
	<tbody style="padding:7px">
		<tr>
			<td><pre><code><i>column_name</i> IN ('<i>value1</i>' [,'<i>value2</i>' ...])</code></pre></td>
			<td>Rows with listed values are shown in control</td>
		</tr>
		<tr>
			<td><pre><code><i>column_name</i> NOT IN ('<i>value1</i>' [,'<i>value2</i>' ...])</code></pre></td>
			<td>Rows with listed values are hidden in control</td>
		</tr>
	</tbody>
</table>

####Guid
#####Custom filter

<table>
	<thead>
		<tr style="text-align:left"><th style="width:150px">Relation</th><th>Syntax</th></tr>
	</thead>
	<tbody style="padding:7px">
		<tr>
			<td>Equals</td>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') LIKE '<i>value</i>'</code></pre></td>
		</tr>
		<tr>
			<td>Not equal</td>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') NOT LIKE '<i>value</i>'</code></pre></td>
		</tr>
		<tr>
			<td>Begins with</td>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') LIKE '%<i>value</i>'</code></pre></td>
		</tr>
		<tr>
			<td>Not begins with</td>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') NOT LIKE '%<i>value</i>'</code></pre></td>
		</tr>
		<tr>
			<td>Ends with</td>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') LIKE '<i>value</i>%'</code></pre></td>
		</tr>
		<tr>
			<td>Not ends with</td>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') NOT LIKE '<i>value</i>%'</code></pre></td>
		</tr>
		<tr>
			<td>Contains</td>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') LIKE '%<i>value</i>%'</code></pre></td>
		</tr>
		<tr>
			<td>Not contain</td>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') NOT LIKE '%<i>value</i>%'</code></pre></td>
		</tr>
	</tbody>
</table>

#####Checklist filter

<table>
	<thead>
		<tr style="text-align:left">
			<th>Syntax</th>
			<th>Description</th>
		</tr>
	</thead>
	<tbody style="padding:7px">
		<tr>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') IN ('<i>value1</i>' [,'<i>value2</i>' ...])</code></pre></td>
			<td>Rows with values selected in checklist are shown in control</td>
		</tr>
		<tr>
			<td><pre><code>Convert(<i>column_name</i>,'System.String') NOT IN ('<i>value1</i>' [,'<i>value2</i>' ...])</code></pre></td>
			<td>Rows with values excluded from checklist are shown in control</td>
		</tr>
	</tbody>
</table>
