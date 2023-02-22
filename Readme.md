# Money

A simple console expense manager software written in C#. It uses EntityFramework to store data and for console output Spectre.Console.

## Usage

The program is a console application and can be operated via commands, like git.

Selected commands offer Batch mode. In this input mode, the given command will open your text editor where you can specify multiple entries at once for editing.

### Add

```
DESCRIPTION:
Add a spending to the database

USAGE:
    money add [amount] [OPTIONS]

ARGUMENTS:
    [amount]    Amount of money spent

OPTIONS:
    -h, --help        Prints help information                             
    -t, --text        A short text description of the spending            
    -c, --category    Spending category                                   
    -d, --date        Spending time. If not set current date will be used 
    -b, --batch       Batch input. Allows to input multiple entries at once
```

### Find

```
DESCRIPTION:
Search spendings in the database

USAGE:
    money find [term] [OPTIONS]

ARGUMENTS:
    [term]    Search term to look for in spending description

OPTIONS:
    -h, --help         Prints help information                                        
    -r, --regex        When set, the search term will be handled as regular expression
    -s, --startdate    Start date (optional)                                          
    -e, --enddate      End date (optional)                                            
    -c, --category     Spending category. When set, only searches in given category   
```

### Category add

```
DESCRIPTION:
Add a spending category

USAGE:
    money category add [category] [OPTIONS]

ARGUMENTS:
    [category]    Category name

OPTIONS:
    -h, --help    Prints help information                             
    -b, --bach    Bach input. Allows to input multiple entries at once
```

### Category list

```
DESCRIPTION:
List all spending categories

USAGE:
    money category list [OPTIONS]

OPTIONS:
    -h, --help    Prints help information
```

### Category rename

```
DESCRIPTION:
Rename a category

USAGE:
    money category rename [oldcategory] [newcategory] [OPTIONS]

ARGUMENTS:
    [oldcategory]    Old category name
    [newcategory]    New category name

OPTIONS:
    -h, --help    Prints help information
```

### Export backup

```
DESCRIPTION:
Export data to backup format

USAGE:
    money export backup [file] [start] [end] [OPTIONS]

ARGUMENTS:
    [file]     file name    
    [start]    Start date (optional)
    [end]      End date (optional)

OPTIONS:
    -h, --help    Prints help information
```

### Export excel

```
DESCRIPTION:
Export data to excel xlsx file

USAGE:
    money export excel [file] [start] [end] [OPTIONS]

ARGUMENTS:
    [file]     file name            
    [start]    Start date (optional)
    [end]      End date (optional)  

OPTIONS:
    -h, --help    Prints help information
```

### Export report

```
DESCRIPTION:
Export a report to an html file

USAGE:
    money export report [file] [start] [end] [OPTIONS]

ARGUMENTS:
    [file]     file name            
    [start]    Start date (optional)
    [end]      End date (optional)  

OPTIONS:
    -h, --help    Prints help information
```

### Import backup

```
DESCRIPTION:
Import data to money backup format

USAGE:
    money import backup [file] [OPTIONS]

ARGUMENTS:
    [file]    file name

OPTIONS:
    -h, --help    Prints help information

```

### Import excel

```
DESCRIPTION:
Import data from excel xlsx file

USAGE:
    money import excel [file] [OPTIONS]

ARGUMENTS:
    [file]    file name

OPTIONS:
    -h, --help    Prints help information
```

### Import createtemplate

```
DESCRIPTION:
Create excel template for importing

USAGE:
    money import createtemplate [file] [OPTIONS]

ARGUMENTS:
    [file]    file name

OPTIONS:
    -h, --help    Prints help information
```

### Stat

```
DESCRIPTION:
Display statistics about spendings

USAGE:
    money stat [start] [end] [OPTIONS]

ARGUMENTS:
    [start]    Start date   
    [end]      End date date

OPTIONS:
    -h, --help        Prints help information              
    -d, --detailed    When set, prints daily spending stats
```
