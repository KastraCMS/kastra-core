# Kastra.Core

### The core library to build a Kastra website.

[![Build Status](https://kastra.visualstudio.com/_apis/public/build/definitions/1268a989-cd48-4170-99de-66c576a8bf1e/2/badge )](https://kastra.visualstudio.com/Kastra.Core/_build/index?definitionId=2)

## Requirements

* .NET Core 2.1.12

## Installation

Install the [Kastra.Core nuget package](https://www.nuget.org/packages/Kastra.Core/) in your .NET project

* .NET CLI : `$ dotnet add package kastra.core`

or

* Nuget package manager : `$ Install-Package kastra.core`

## Usage

```C#
using Kastra.Core;
```

### In startup.cs

In ConfigureServices method, you need to add :
* Kastra application options
* Kastra dependancy injections
* Kastra services

Note that after creating your Kastra application settings, you need to load Kastra business, DAL and modules assemblies with the `DirectoryAssemblyLoader` static class.

An example of ConfigureServices method could be :

```C#
// Add options
services.AddOptions();

AppSettings appSettings = Configuration.GetSection("AppSettings").Get<AppSettings>();
services.Configure<AppSettings>(Configuration);

// Load assemblies
DirectoryAssemblyLoader.LoadAllAssemblies(appSettings);

// Add dependencies
var assemblies = KastraAssembliesContext.Instance.Assemblies.Values.ToArray();
services.AddDependencyInjection(Configuration, assemblies);

// Add Kastra default services
services.AddKastraServices();

services.AddMvc();
```
### Make a template controller

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

### Declare a module

You need to add a class which derives from ModuleBase class available in the Kastra.Core namespace.
The install method of the ModuleBase class will install automatically the module data in your website. These data must be in a fil named `moduleconfig.json`.

An example could be :

*ArticleModule.cs*
```C#
public class ArticleModule : ModuleBase
{
    public override void SetDependencyInjections(IServiceCollection services, IConfiguration configuration)
    {
        // Add services or dependancy injections
    }

    public override void Install(IServiceProvider serviceProvider, IViewManager viewManager)
    {
        base.Install(serviceProvider, viewManager);

         // Add your specific code to install your module
    }

    public override void Uninstall()
    {
        // Add your specific code to uninstall your module
    }
}
```

*moduleconfig.json*
```Json
{
	"Modules": [{
		"Definition": {
			"SystemName": "Article",
			"DisplayName": "Article",
			"Namespace": "Kastra.Module.Article",
			"Path": "Default/Article",
			"Version": "1.0"
		},
		"Controls": [{
				"KeyName": "Settings",
				"Path": "Settings"
			},
			{
				"KeyName": "Edit",
				"Path": "Edit"
			}
		]
	}]
}
```

### Make a module view component in a module

To make a view component module, you can use the `Kastra.Core.ViewComponents` namespace. Your view component must derive from `ModuleViewComponent` class. You can use a model which must derive from `ModuleModelBinder` class.

An example could be :

*IndexViewComponent.cs*

```C#
namespace Kastra.Module.Article
{
    [ViewComponent(Name = "Kastra.Module.Article.Index")]
    public class IndexViewComponent : ModuleViewComponent
    {
        private readonly ArticleContext _dbContext = null;
        private readonly IArticleBusiness _articleManager = null;

        public ArticleViewComponent(ArticleContext dbContext, IArticleBusiness articleBusiness)
        {
            _dbContext = dbContext;
            _articleManager = articleBusiness;
        }
        
        public override ViewViewComponentResult OnViewComponentLoad()
        {
            IndexModel model = new IndexModel(this);
            
            // Fill the model or do other thing here ...

            return ModuleView("Index", model);
        }
    }
}
```

*IndexModel.cs*

```C#
namespace Kastra.Module.Article.Models
{
    public class IndexModel: ModuleModelBinder
    {
        public IndexModel(ModuleViewComponent moduleView) : base(moduleView) { }

        public IList<ArticleInfo> Articles { get; set; }

        public Int32 PageId { get; set; }
    }
}
```