# 启动

该demo用于测试ASP.NET Core程序的启动。

## 配置IHostBuilder构建器

配置构建器分为5个部分：

### PartOne：指定Host的预定义的默认配置

* ConfigureWebHostDefaults

比如：Kestrel服务、中间件等

微软官方解释：

> Initializes a new instance of the Microsoft.AspNetCore.Hosting.IWebHostBuilder class with pre-configured defaults.

> The following defaults are applied to the Microsoft.AspNetCore.Hosting.IWebHostBuilder:
use Kestrel as the web server and configure it using the application's configuration providers, 
adds the HostFiltering middleware, 
adds the ForwardedHeaders middleware if ASPNETCORE_FORWARDEDHEADERS_ENABLED=true, and enable IIS integration.

### PartTwo：指定Buider自身的配置

* ConfigureHostConfiguration

比如：IHostEnvironment等

微软官方解释：

> Set up the configuration for the builder itself.
This will be used to initialize the Microsoft.Extensions.Hosting.IHostEnvironment for use later in the build process.
This can be called multiple times and the results will be additive.

### PartThree：设置其余剩下的配置

* ConfigureAppConfiguration

//比如：嵌入自己新增的配置文件

微软官方解释：

> Sets up the configuration for the remainder of the build process and application.
This can be called multiple times and the results will be additive.
The results will be available at Microsoft.Extensions.Hosting.HostBuilderContext.Configuration for subsequent operations, as well as in Microsoft.Extensions.Hosting.IHost.Services.

### PartFour：往容器内注册组件

* ConfigureServices（注册组件到容器）

微软官方解释：

> Adds services to the container. 
This can be called multiple times and the results will be additive.

* ConfigureLogging
* Startup
* Startup.ConfigureServices

### PartFive：管道

* Startup.Configure（中间件）

## 配置执行顺序

5个部分顺序如上说式，其中第4部分注册组件中，ConfigureServices的顺序可能有变化：

当ConfigureServices写在ConfigureWebHostDefaults前面时，即在UseStartup<Startup>()之前时，ConfigureServices会在Startup.ConfigureServices之前执行，如Test02用例所示

当ConfigureServices写在ConfigureWebHostDefaults后面时，即在UseStartup<Startup>()之后时，ConfigureServices会在Startup.ConfigureServices之后执行，如Test01用例所示

## Startup类不是必须的

可以使用委托代替Startup类，如Test03用例所示