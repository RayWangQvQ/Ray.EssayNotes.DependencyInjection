# Ray.EssayNotes.DependencyInjection

## 文章目录

* [01.Basic：基础知识与应用](https://github.com/WangRui321/Ray.EssayNotes.DependencyInjection/blob/master/01.Basic/README.md)
* [02.DIY：自己实现一个精简的类似.NET Core的依赖注入容器](https://github.com/WangRui321/Ray.EssayNotes.DependencyInjection/blob/master/02.DIY/README.md)
* [03.Auto：项目继承Autofac与特殊用法](https://github.com/WangRui321/Ray.EssayNotes.DependencyInjection/blob/master/03.AutoFac/docs/1.theory.md)
* [04.Futher：进(rù)阶(tǔ)](https://github.com/WangRui321/Ray.EssayNotes.DependencyInjection/blob/master/04.Further/src/Ray.EssayNotes.Di.Further/README.md)

---

## 说明

### 简介

该仓储是一个学习笔记型项目，用于DI初学者循序渐进的学习.NET下依赖注入的相关技术。

#### 01.Basic

以.NET Core的DI框架为基础，学习基础知识与用法。

主要包含了：

* DI理论基础
* 容器的概念使用
* .NET Core应用的启动与依赖注入的时机
* 注册、注入的常规用法
* 生命周期作用域与资源释放

#### 02.DIY

自己实现一个精简的类似.NET Core的依赖注入容器。

主要用于已经可以熟练使用依赖注入进行开发，但是想进一步了解其实现方法和原理的朋友。

比如构造注入到底是怎么实现的、生命周期作用域又是怎么管理的等等。

#### 03.AutoFac

是一个虚构的项目框架，以集成和使用Autofac为主。
主要展示了一个正常中小型项目是怎么使用依赖注入的，怎么在MVC、WebApi和Core Api下分别实现依赖注入。
其中也包含了一些Autofac的特殊运用技巧。

#### 04.Futher

进阶部分，主要关注.NET Core的DI框架的源码与设计，比如ServiceProvider、ServiceProviderEngine、ServiceProviderEngineScope的关系，比如在不同域的对象中注入IServiceProvider得到的是什么。

### 适用对象

该项目是我个人在学习过程中编写的代码和笔记，每个知识点我都尽量用代码写了测试程序，可以直接运行调试。

如果你是：

* 和我一样也在学习DI相关的知识

* 从来没听过IoC、DI是什么蛤蟆

* 了解一些依赖注入的理论知识但是缺乏实战

* 在.Net Framework下已熟练运用依赖注入，但在.Net Core还比较陌生

那么希望这个开源的项目可以对你有所帮助。

如果你是：

* 发量比我还少的秒天秒地的大牛

	那么也欢迎Fork，虽然可能对你帮助并不大，但是欢迎提供宝贵的意见，有觉得写的不好的地方，可以PR共同改进~

![Expert](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190704170034980-1208556913.jpg)

