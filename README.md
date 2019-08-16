# Ray.EssayNotes.AutoFac

## 文章目录

1. [第一章|依赖注入理论基础](https://github.com/WangRui321/Ray.EssayNotes.AutoFac/blob/master/docs/1.theory.md)

1. [第二章|控制台程序下的依赖注入](https://github.com/WangRui321/Ray.EssayNotes.AutoFac/blob/master/docs/2.console.md)

1. [第三章|AutoFac用法](https://github.com/WangRui321/Ray.EssayNotes.AutoFac/blob/master/docs/3.autofac.md)

1. [第四章|Asp.Net Framework MVC程序实现AutoFac注入](https://github.com/WangRui321/Ray.EssayNotes.AutoFac/blob/master/docs/4.mvc.md)

1. [第五章|Asp.Net Framework WebApi程序实现AutoFac注入](https://github.com/WangRui321/Ray.EssayNotes.AutoFac/blob/master/docs/5.webapi.md)

1. [第六章|Asp.Net Cor的依赖注入](https://github.com/WangRui321/Ray.EssayNotes.AutoFac/blob/master/docs/6.core.md)

---

## 说明

### 简介

该项目是一个虚构的项目框架，类似于样例代码或者测试程序，用于DI初学者循序渐进的学习.NET下依赖注入的知识。

项目在以实战模式，在.net下的

* 控制台程序

* Framework Mvc程序

* Framework WebApi程序

* Core Api程序

分别实现依赖注入。

代码里写了很多注释，对理解DI，或怎么在MVC、WebApi和Core Api分别实现依赖注入有很好的帮助学习效果。

其中.Net Framework框架主要以如何引入**AutoFac**作为容器以及如何运用**AuotoFac**为主。

.Net Core框架除了实现了引入AutoFac的两种方式，同时也运用反射技巧对其**自带的DI框架**进行了初步封装，实现了相同的依赖注入效果。

项目架构如下图：

![structure](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190704172315934-1174377068.png)

| 项目 | 名称 | 类型 | 框架 |
| --- | --- | --- | --- |
| Ray.EssayNotes.AutoFac.Infrastructure.CoreIoc | Core容器 | 类库 | .NET Core 2.2 |
| Ray.EssayNotes.AutoFac.Infrastructure.Ioc | Framework容器 | 类库 | .NET Framework 4.5 |
| Ray.EssayNotes.AutoFac.Model | 实体层 | 类库 | .NET Framework 4.5 |
| Ray.EssayNotes.AutoFac.Repository | 仓储层 | 类库 | .NET Framework 4.5 |
| Ray.EssayNotes.AutoFac.Service | 业务逻辑层 | 类库 | .NET Framework 4.5 |
| Ray.EssayNotes.AutoFac.ConsoleApp | 控制台主程序 | 控制台项目 | .NET Framework 4.5 |
| Ray.EssayNotes.AutoFac.CoreApi | Core WebApi主程序 | Core Api项目 | .NET Core 2.2 |
| Ray.EssayNotes.AutoFac.NetFrameworkApi | Framework WebApi主程序 | Framework WebApi项目 | .NET Framework 4.5 |
| Ray.EssayNotes.AutoFac.NetFrameworkMvc | Framework MVC主程序 | Framework MVC项目 | .NET Framework 4.5 |

GitHub源码：[https://github.com/WangRui321/Ray.EssayNotes.AutoFac](https://github.com/WangRui321/Ray.EssayNotes.AutoFac)

Welcome to fork me~(欢迎来叉我~)

### 适用对象

该项目主要以**学习入门**为主，理论部分我会结合例子和代码，深入浅出地阐述，如果你是：

* 从来没听过IoC、DI是什么蛤蟆

* 了解一些依赖注入的理论知识但是缺乏实战

* 在.Net Framework下已熟练运用依赖注入，但在.Net Core还比较陌生

只要你跟着文章认真读完每一句话，我有信心这系列文章一定会对你有所帮助。

如果你是：

* 发量比我还少的秒天秒地的大牛

	那么也欢迎Fork，虽然可能对你帮助并不大，但是欢迎提供宝贵的意见，有写的不好的地方可以互相交流~

![Expert](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190704170034980-1208556913.jpg)

