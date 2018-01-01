Ixq Framework
=================
Ixq framework是一个Web应用程序框架，实现了自动注入、自动映射、日志、缓存等功能，帮助开发者快速构建Web应用程序。

组件介绍
----------------

| # | 组件 | 介绍| Nuget|
| ---| -------------------------------- | ------------------------------------------------------------------------------------------------------ |---------------|
|  1 | Ixq.Core                         |  Ixq.Core是Ixq framework的核心类库，此类库定义了如实体、Repository、依赖注入、日志、缓存等功能的接口       |  [![1.1]][1.2]|
|  2 | Ixq.Logging                      |  基于Log4Net实现的日志组件。                                                                           | [![2.1]][2.2]|
|  3 | Ixq.Redis                        |  基于StackExchange.Redis实现的缓存组件。                                                               | [![3.1]][3.2]|
|  4 | Ixq.Data                         |  基于Entity Framework实现的数据访问。                                                                  | [![4.1]][4.2]|
|  5 | Ixq.DependencyInjection          |  依赖注入组件，基于Autofac实现了框架的依赖注入功能。                                                     | [![5.1]][5.2]|
|  6 | Ixq.Mapper                       |  基于AutoMapper实现的自动映射组件。                                                                    | [![6.1]][6.2]|
|  7 | Ixq.Security                     |  身份验证、权限管理组件，基于AspNet.Identity实现。                                                      | [![7.1]][7.2]|
|  8 | Ixq.UI                           |  定义了Ixq.Web.Mvc组件的视图模型。                                                                     | [![8.1]][8.2]|
|  9 | Ixq.Web.Mvc                      |  Ixq.Web.Mvc是基于Mvc模式的Web应用程序展示层的组件，有助于快速构建Web应用程序。                           | [![9.1]][9.2]|
|  10 | Ixq.Owin                        |  对IAppBuilder启动方式的支持。                                                                        | [![10.1]][10.2]|
|  11 | Ixq.Extensions                  |  封装了一些常用的扩展方法。                                                                            | [![11.1]][11.2]|

[1.1]: https://img.shields.io/nuget/v/Ixq.Core.svg?style=flat
[1.2]: https://www.nuget.org/packages/Ixq.Core
[2.1]: https://img.shields.io/nuget/v/Ixq.Logging.svg?style=flat
[2.2]: https://www.nuget.org/packages/Ixq.Logging
[3.1]: https://img.shields.io/nuget/v/Ixq.Redis.svg?style=flat
[3.2]: https://www.nuget.org/packages/Ixq.Redis
[4.1]: https://img.shields.io/nuget/v/Ixq.Data.svg?style=flat
[4.2]: https://www.nuget.org/packages/Ixq.Data
[5.1]: https://img.shields.io/nuget/v/Ixq.DependencyInjection.svg?style=flat
[5.2]: https://www.nuget.org/packages/Ixq.DependencyInjection
[6.1]: https://img.shields.io/nuget/v/Ixq.Mapper.svg?style=flat
[6.2]: https://www.nuget.org/packages/Ixq.Mapper
[7.1]: https://img.shields.io/nuget/v/Ixq.Security.svg?style=flat
[7.2]: https://www.nuget.org/packages/Ixq.Security
[8.1]: https://img.shields.io/nuget/v/Ixq.UI.svg?style=flat
[8.2]: https://www.nuget.org/packages/Ixq.UI
[9.1]: https://img.shields.io/nuget/v/Ixq.Web.Mvc.svg?style=flat
[9.2]: https://www.nuget.org/packages/Ixq.Web.Mvc
[10.1]: https://img.shields.io/nuget/v/Ixq.Owin.svg?style=flat
[10.2]: https://www.nuget.org/packages/Ixq.Owin
[11.1]: https://img.shields.io/nuget/v/Ixq.Extensions.svg?style=flat
[11.2]: https://www.nuget.org/packages/Ixq.Extensions

什么是Ixq Framework
----------------
在 Ixq 中以接口的形式定义了各各组件的规范，并基于您熟悉的组件，如：Autofac、log4net、AutoMapper、Entity Framework等等，提供了默认实现，
如果您觉得默认的组件不符合您的需求，
您可以以您的方式去定制符合您需求的组件。 <br>
所以 Ixq 极具扩展性，降低了层与层之间的耦合。

如何使用
----------------

### 日志

```C#
static void Main()
{
    // 设置日志工厂。
    Ixq.Core.Logging.LogManager.SetLoggerFactory(new Ixq.Logging.Log4Net.Log4NetLoggerFactory());
    
    // 输出日志。
    var logger = Ixq.Core.Logging.LogManager.GetLogger(GetType());
    logger.Info("Hello Ixq Framework.");
}

```

### 缓存

```C#
static void Main()
{
    // 设置缓存提供者。
    Ixq.Core.Cache.CacheManager.SetCacheProvider(new Ixq.Core.Cache.MemoryCacheProvider());
    var globalCache = Ixq.Core.Cache.CacheManager.GetGlobalCache();
    // 写入。
    globalCache.Set("Ixq", "Hello Ixq Framework.");
    // 读取。
    var res = globalCache.Get<string>("Ixq");
}

```

### 

License
----------------
Ixq Framework 遵循 MIT 开源协议，[LICENSE](https://github.com/zhk0603/ixq/blob/master/LICENSE.txt "LICENSE") .

