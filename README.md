
<p align="center">
  <img src="Assets/parsini_white.png">
</p>
<br/>

![NugetDownloadCount](https://img.shields.io/nuget/dt/Parsini.svg?label=NuGet%20Downloads&colorB=007ec6)
![NugetVersion](https://img.shields.io/nuget/v/Parsini.svg?label=NuGet%20Version&colorB=007ec6)

A simple INI file parser in C#.

## Getting Started

Parsini is developed in `.NET Standard 2.0`, it is therefore supported on the following platforms:
- .NET Framework 4.6.1
- .NET Core 2.0
- Mono 5.4
- Xamarin.iOS 10.14
- Xamarin.Mac 3.8
- Xamarin.Android 7.5

### Installation
You can install the [NuGet package](https://www.nuget.org/packages/Parsini) via the Package Manager
```
PM> Install-Package Parsini -Version 1.0.0
```
or the .NET CLI
```
> dotnet add package Parsini --version 1.0.0
```

### INI Standard

Altough the INI file format is an **informal standard**, Parsini follows an opinionated naming convention.

|Element|Description|
|-------|-----------|
|Sections|Groups of **keys**, arbitrarely named.|
|Keys|Pairs of *names* and *values* contained within a **section**.


Parsini also assumes that 
- Sections are enclosed in brackets, as such `[section]`.
- Every keys that follow a section definition are included in that section, until the next section definition.
- If no section is defined, the keys will be added in the `DEFAULT` section.
- A key's name and value pair is separeted by an equal sign (`=`), as such `name=value`.
- Comments are defined by a semicolon (`;`) at the beginning of the line and are ignored by the parser.

## Usage

### Using Statements
The following `using` are available:
```csharp
using Parsini;                // Required, exposes main parser class.
using Parsini.Models;         // Optional, exposes types-related classes, useful for storing result in variables.
using Parsini.Collections;    // Optional, exposes collections-related classes, useful for storing result in variables.
```

### Instantiation

You will need to instantiate a new `IniParser` by passing the INI file's path.  

The parsing is done on instantiation and the result is accessible through the `Result` property.

```csharp
IniParser parser = new IniParser(@"C:\temp\conf.ini");
Ini configuration = parser.Result;
```

### Parsing

#### Standard INI File
The following examples use this sample INI file.  

Is is opinionatedly defined as *standard* since every **keys** are contained in **sections**, and it follows every rules defined in the aforementioned standard.

```
; Standard INI file sample.
[app]
version=0.0.1

[database]
server=127.0.0.1
port=123
```

The named sections are accessible either via indexes or names.

Once a section is returned, its keys are also accessible via indexes or names.

Once a key is returned, its value is accessed via the `Value` property.

```csharp
using Parsini;
using Parsini.Models;

IniParser parser = new IniParser(@"C:\temp\conf.ini");
Ini configuration = parser.Result;

string db_server = configuration.Sections["database"].Keys["server"].Value;
string db_port = configuration.Sections["database"].Keys["port"].Value;
string app_version = configuration.Sections["app"].Keys["version"].Value;
```

The parser's result may be accessed without storing it in another variable.  The `using Parsini.Models` statement may then be removed.
```csharp
using Parsini;

IniParser parser = new IniParser(@"C:\temp\conf.ini");

string db_server = parser.Result.Sections["database"].Keys["server"].Value;
string db_port = parser.Result.Sections["database"].Keys["port"].Value;
string app_version = parser.Result.Sections["app"].Keys["version"].Value;
```

#### INI File WITHOUT Sections
Although it is not the preferred way, INI files without sections are suported.

The following examples use this sample INI file.
```
; INI file sample without sections.
server=127.0.0.1
port=123
```

If you did not provide any sections in your INI file, Parsini creates a default section (named `DEFAULT`).  It is then accessible either though its name (`DEFAULT`), or its index (`0`).

```csharp
using Parsini;
using Parsini.Models;

IniParser parser = new IniParser(@"C:\temp\db.ini");
Ini db_conf = parser.Result;

string db_server = db_conf.Sections[0].Keys["server"].Value;          // Via index
string db_port = db_conf.Sections["DEFAULT"].Keys["port"].Value;      // Via name
```

Again, the parser's result may be accessed without storing it in another variable.  The `using Parsini.Models` statement may then be removed.
```csharp
using Parsini;

IniParser parser = new IniParser(@"C:\temp\db.ini");

string db_server = parser.Result.Sections[0].Keys["server"].Value;          // Via index
string db_port = parser.Result.Sections["DEFAULT"].Keys["port"].Value;      // Via name
```

---

## Change Log
### 1.0.0
- Initial Release!
- Support basic INI file parsing.
