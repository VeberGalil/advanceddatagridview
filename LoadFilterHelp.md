AdvancedDataGridView.LoadFilter(String) method
==

Sets the filter to be used to exclude items from the collection of items returned by the data source.

    public void LoadFilter(string filter);

### Parameters   

*filter*   
&emsp;&emsp;The string used to filter items out in the item collection returned by the data source.


### Remarks   

LoadFilter method allows programmatically apply filter to grid (and underlying data source), as if it was selected 
by end user in filter menus for ADVG columns.

AdvancedDataGridView will parse provided filter string, splitting it to filters for individual columns. 
Each column filter will be tested in compliance with column data type.
If column filter is recognized as custom filter, it will be added to list of custom filters for this column.
If filter is not custom filter, AdvancedDataGridView will try to use it as value source for checklist filter.
In any case, control will build new filter string according to recognized filter parts and generate 
FilterStringChanged event, passing new filter string in FilterString property of FilterEventArgs parameter.
AdvancedDataGridView will also apply generated filter to binding source associated with DataSource property.        

        
### Filter String Syntax    

Filter string may consist of one or more column filters, delimited with AND:   

&emsp;&emsp;(*column_filter*) [AND (*column_filter*) ...]

In general, column filter consists of column name, relational operator and data value expression.

*Column name* should always be enclosed in square brackets. In may include uppercase or lowercase characters, 
digits and underscore (_) character. For example: `[My_column_1]`.

Available *relational operators* depend on column data type and filter type (custom or checklist filter), 
as described later in this manual.

*Data value expression* may be single value, value range (for BETWEEN operator) or comma-separated list 
of values. Data value expression for custom filter may also contain wildcard character % (any string of zero 
or more characters), as described later in this manual.

The exact syntax of column filter depends on column data type, and differs between custom filter and checklist 
filter for the same column.

If column data type allows 'empty' values (`null`, `DbNull.Value` or `String.Empty`), following syntax may be used to select these values in column:

&emsp;&emsp;[*column_name*] IS NULL

Following are column filter syntax and rules for different data types.

#### DateTime    

##### Custom filter   

Relation|Syntax
--------|------
Equal				|Convert(*column_name*,'System.String') LIKE '%*value*%'
Not equal			|Convert(*column_name*,'System.String') NOT LIKE '%*value*%'
Less then			|*column_name* &lt; '*value*'
Less then or equal	|*column_name* &lt;= '*value*'
Greater then		|*column_name* &gt; '*value*'
Greater then or equal|*column_name* &gt;= '*value*'
Between				|*column_name* &gt;= '*value1*' AND *column_name* &lt;= '*value2*'


##### Checklist filter   

Checklist filter may consist of one or more equality filters, separated with OR:

&emsp;&emsp;Convert(*column_name*,'System.String') LIKE '%*value*%' [ OR Convert(*column_name*,'System.String') LIKE '%*value*%' ...]

#### TimeStamp   

##### Custom filter   

TimeStamp values must conform to the W3C XML Schema Part 2: Datatypes recommendation for duration.

Relation|Syntax
--------|-------
Equal		|Convert(*column_name*,'System.String') LIKE '%*value*%'
Not equal	|Convert(*column_name*,'System.String') NOT LIKE '%*value*%'

##### Checklist filter   

Checklist filter may consist of one or more equality filters, separated with OR:

&emsp;&emsp;Convert(*column_name*,'System.String') LIKE '%*value*%' [ OR Convert(*column_name*,'System.String') LIKE '%*value*%' ...]


#### Int32, Int64, Int16, UInt32, UInt64, UInt16, Byte, SByte and Decimal   

##### Custom filter   

Relation|Syntax
--------|------
Equal					|*column_name* = *value*
Not equal				|*column_name* <> *value*
Less then				|*column_name* &lt; *value*
Less then or equal		|*column_name* &lt;= *value*
Greater then			|*column_name* &gt; *value*
Greater then or equal	|*column_name* &gt;= *value*
Between					|*column_name* &gt;= *value1* AND *column_name* &lt;= *value2*

##### Checklist filter   

Syntax|Description
------|-----------
*column_name* IN (*value1*[,*value2* ...])      |Rows with listed values are shown in control
*column_name* NOT IN (*value1*[,*value2* ...])	|Rows with listed values are hidden in control
		

#### Single and Double   

##### Custom filter   

Relation|Syntax
--------|------
Equal					|*column_name* = *value*
Not equal				|*column_name* <> *value*
Less then				|*column_name* &lt; *value*
Less then or equal		|*column_name* &lt;= *value*
Greater then			|*column_name* &gt; *value*
Greater then or equal	|*column_name* &gt;= *value*
Between					|*column_name* &gt;= *value1* AND *column_name* &lt;= *value2*

##### Checklist filter   

Description|Syntax
-----------|------
Convert(*column_name*,'System.String') IN (*value1*[,*value2* ...])		|Rows with listed values are shown in control
Convert(*column_name*,'System.String') NOT IN (*value1*[,*value2* ...])	|Rows with listed values are hidden in control

#### String   

If string value, used in either custom or checklist filter, contains apostroph (') character,
this character must be doubled in filter. For example, to show only rows that contain value "that's it"
in column Column1, use: 

&emsp;&emsp;Column1 LIKE '%that''s it%'

##### Custom filter   


Relation|Syntax
--------|------
Equals			|*column_name* LIKE '*value*'
Not equal		|*column_name* NOT LIKE '*value*'
Begins with		|*column_name* LIKE '%*value*'
Not begins with	|*column_name* NOT LIKE '%*value*'
Ends with		|*column_name* LIKE '*value*%'
Not ends with	|*column_name* NOT LIKE '*value*%'
Contains		|*column_name* LIKE '%*value*%'
Not contain		|*column_name* NOT LIKE '%*value*%'

##### Checklist filter   

Syntax|Description
------|-----------
*column_name* IN ('*value1*' [,'*value2*' ...])		|Rows with listed values are shown in control
*column_name* NOT IN ('*value1*' [,'*value2*' ...])	|Rows with listed values are hidden in control


#### Guid   

##### Custom filter   

Relation|Syntax
--------|------
Equals			|Convert(*column_name*,'System.String') LIKE '*value*'
Not equal		|Convert(*column_name*,'System.String') NOT LIKE '*value*'
Begins with		|Convert(*column_name*,'System.String') LIKE '%*value*'
Not begins with	|Convert(*column_name*,'System.String') NOT LIKE '%*value*'
Ends with		|Convert(*column_name*,'System.String') LIKE '*value*%'
Not ends with	|Convert(*column_name*,'System.String') NOT LIKE '*value*%'
Contains		|Convert(*column_name*,'System.String') LIKE '%*value*%'
Not contains	|Convert(*column_name*,'System.String') NOT LIKE '%*value*%'


##### Checklist filter   

Syntax|Description
------|-----------
Convert(*column_name*,'System.String') IN ('*value1*' [,'*value2*' ...])	|Rows with values selected in checklist are shown in control
Convert(*column_name*,'System.String') NOT IN ('*value1*' [,'*value2*' ...]	|Rows with values excluded from checklist are shown in control
