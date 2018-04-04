# Kastra.Core

### The core library to build a Kastra website.

[![Build Status](https://kastra.visualstudio.com/_apis/public/build/definitions/1268a989-cd48-4170-99de-66c576a8bf1e/2/badge )](https://kastra.visualstudio.com/Kastra.Core/_build/index?definitionId=2)

## Requirements
* .NET Core 2.0.0 and SDK 2.0.0

## Installation

* Install the nuget package in your .NET project

.NET CLI

`$ dotnet add package kastra.core`

or

Nuget package manager

`$ Install-Package kastra.core`

## Usage

```C#
using Kastra.Core;
```

### In startup.cs

In ConfigureServices method, you need to add :
* Kastra application options
* Kastra dependancy injections
* Kastra services

Note that after creating your Kastra application settings, you need to load Kastra business, DAL and modules assemblies with the "DirectoryAssemblyLoader".

An example of ConfigureServices method could be :

```C#
// Add options
services.AddOptions();

AppSettings appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
services.Configure<AppSettings>(Configuration);

// Load assemblies
DirectoryAssemblyLoader.LoadAllAssemblies(appSettings);

// Add dependencies
var assemblies = KastraAssembliesContext.Instance.Assemblies;
services.AddDependencyInjection(Configuration, assemblies.Values.ToArray());

// Add Kastra default services
services.AddKastraServices();

services.AddMvc();
```

### In a controller to make a template controller

If you want to use the default template controller, your controller must derive from TemplateController which is available in the Kastra.Core.Controllers namespace.

An example of a template controller could be :

```C#
using Kastra.Core;
using Kastra.Core.Controllers;
using Kastra.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Kastra.Web.Controllers
{
    public class MyController : TemplateController
    {
        public MyController(IViewManager viewManager,
                              CacheEngine cacheEngine, 
                              IViewComponentDescriptorCollectionProvider viewcomponents, 
                              IParameterManager parameterManager) 
                            : base(viewManager, cacheEngine, viewcomponents, parameterManager){}
    }
}
```

### In a view compoenent to make a module

... Work in progress ...