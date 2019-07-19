# Ray.EssayNotes.AutoFac

## 系列目录

1. [第一章|理论基础+实战控制台程序实现AutoFac注入](https://www.cnblogs.com/RayWang/p/11128554.html)

1. 第二章|AutoFac的常见使用套路

1. 第三章|实战Asp.Net Framework Web程序实现AutoFac注入

1. 第四章|实战Asp.Net Core自带DI实现依赖注入

1. 第五章|实战Asp.Net Core引入AutoFac的两种方式

---

## 说明

### 简介

该系列共5篇文章，旨在以实战模式，在.net下的

* 控制台程序

* Framework Mvc程序

* Framework WebApi程序

* Core Api程序

分别实现依赖注入。

其中.Net Framework框架主要以如何引入**AutoFac**作为容器以及如何运用**AuotoFac**为主，.Net Core框架除了研究引入AutoFac的两种方式，同时也运用反射技巧对其**自带的DI框架**进行了初步封装，实现了相同的依赖注入效果。
项目架构如下图：
![structure](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190704172315934-1174377068.png)

GitHub源码地址：[https://github.com/WangRui321/Ray.EssayNotes.AutoFac](https://github.com/WangRui321/Ray.EssayNotes.AutoFac)
Welcome to fork me~(欢迎来叉我~)

### 适用对象

该项目主要**实战为主**，理论部分我会结合例子和代码，深入浅出地阐述，如果你是：

* 从来没听过IoC、DI这些劳什子
* 了解一些依赖注入的理论知识但是缺乏实战
* 在.Net Framework下已熟练运用依赖注入，但在.Net Core还比较陌生

只要你花上半个小时认真读完每一句话，我有信心这篇文章一定会对你有所帮助。

如果你是：

* 发量比我还少的秒天秒地的大牛
![Expert](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190704170034980-1208556913.jpg)

那么也欢迎阅读，虽然可能对你帮助并不大，但是欢迎提供宝贵的意见，有写的不好的地方可以互相交流~

下面开始第一章《理论知识+实战控制台程序实现AutoFac注入》

---

## 理论基础

### 依赖

依赖，简单说就是，当一个类需要另一个类协作来完成工作的时候就产生了依赖。这也是耦合的一种形式。

举个例子，比如标准的**三层架构**模式

|       名称        |       职责       |       举例        |
| :---------------: | :--------------: | :---------------: |
|   界面层（UI）    |   负责展示数据   | StudentController |
| 业务逻辑层（BLL） | 负责业务逻辑运算 |  StudentService   |
| 数据访问层（DAL） |   负责提供数据   | StudentRepository |

数据访问层（DAL）代码：

```csharp
    /// <summary>
    /// 学生仓储
    /// </summary>
    public class StudentRepository
    {
        public string GetName(long id)
        {
            return "学生张三";//造个假数据返回
        }
    }
```

业务层（BLL）代码：

```csharp
    /// <summary>
    /// 学生逻辑处理
    /// </summary>
    public class StudentService
    {
        private readonly StudentRepository _studentRepository;

        public StudentService()
        {
            _studentRepository = new StudentRepository();
        }

        public string GetStuName(long id)
        {
            var stu = _studentRepository.Get(id);
            return stu.Name;
        }
    }
```

其中，StudentService的实现，就必须要依赖于StudentRepository。而且这是一种紧耦合，一旦StudentRepository有任何更改，必然导致StudentService的代码同样也需要更改，这种情况是程序员们不愿意看到的。

### 接口驱动

接口驱动是为了实现一个设计原则：**要依赖于抽象，而不是具体的实现**。
还拿上面的例子说明，现在我添加一个DAL的**接口层**，IStudentRepository，抽象出所需方法：

```csharp
    /// <summary>
    /// 学生仓储interface
    /// </summary>
    public interface IStudentRepository
    {
        string GetName(long id);
    }
```

然后让StudentRepository去实现这个接口：

```csharp
    /// <summary>
    /// 学生仓储
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        public string GetName(long id)
        {
            return "学生张三";//造个假数据返回
        }
    }
```

然后在StudentService里只依赖于IStudentRepository，以后的增删改查都通过IStudentRepository这个抽象来做：

```csharp
    /// <summary>
    /// 学生逻辑处理
    /// </summary>
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService()
        {
            _studentRepository = new StudentRepository();
        }

        public string GetStuName(long id)
        {
            var stu = _studentRepository.Get(id);
            return stu.Name;
        }
    }
```

这样做的好处有两个，一个是低耦合，一个是职责清晰。如果对此还有怀疑的话，我们可以想象一个情景，就是负责写StudentService的是程序员A，负责写StudentRepository的是另一个程序员B，那么：

* 针对程序员A

我（程序员A）只需要关注业务逻辑层面，如果我需要从仓储层拿数据库的数据，比如我需要根据Id获取学生实体，那么我只需要去IStudentRepository找Get(long id)函数就可以了，至于实现它的仓储怎么实现这个方法我完全不用管，你怎么从数据库拿数据不是我该关心的事情。

* 针对程序员B

我（程序员B）的工作就是实现IStudentRepository接口的所有方法就行了，简单而明确，至于谁来调用我，我不用管。IStudentRepository里有根据Id获取学生姓名的方法，我实现了就行，至于业务逻辑层拿这个名字干啥，那不是我要关心的事情。

这样看的话是不是彼此的职责就清晰多了，更进一步再举个极端的例子：
比如程序员B是个实习生，整天划水摸鱼，技术停留在上个世纪，结果他写的仓储层读取数据库全部用的手写sql语句的方式，极难维护，后来被领导发现领了盒饭，公司安排了另一个程序员C来重写仓储层，C这时不需要动其他代码，只需要新建一个仓储StudentNewRepository,然后实现之前的IStudentRepository，C使用Dapper或者EF，写完新的仓储层之后，剩下的只需要在StudentService里改一个地方就行了：

```csharp
        public StudentService()
        {
            _studentRepository = new StudentNewRepository();
        }
```

是不是很清晰，耦合不会像以前那么重。
其实对于这个小例子来说，接口驱动的优势还不太明显，但是在系统层面优势就会被放大。比如上面换仓储的例子，虽然指责是清晰了，但是项目里有几个Service就需要改几个地方，还是很麻烦。原因就是上面讲的，这是一种依赖关系，Service要依赖Repository，有没有一种方法可以让这种控制关系反转过来呢？当Service需要使用Repository，有没有办法让我需要的Repository自己注入到我这里来？
当然有，这就是我们实现的依赖注入。使用依赖注入后你会发现，当C写完新的仓储后，业务逻辑层（StudentService）是不需要改任何代码的，所有的Service都不需要一个一个去改，直接在注入的时候修改规则，不要注入以前老的直接注入新的仓储就可以了。

面向接口后的架构：

|              名称              |         职责         |        举例        |
| :----------------------------: | :------------------: | :----------------: |
|          界面层（UI）          |     负责展示数据     | StudentController  |
| 业务逻辑抽象层（InterfaceBLL） | 业务逻辑运算抽象接口 |  IStudentService   |
|       业务逻辑层（BLL）        |   负责业务逻辑运算   |   StudentService   |
| 数据访问抽象层（InterfaceDAL） |   数据访问抽象接口   | IStudentRepository |
|       数据访问层（DAL）        |     负责提供数据     | StudentRepository  |

### 什么是IoC

IoC，全称Inversion of Control，即“**控制反转**”，是一种**设计原则**，最早由Martin Fowler提出，因为其理论提出时间和成熟时间相对较晚，所以并没有被包含在GoF的《设计模式》中。

### 什么是DI

DI，全称Dependency Injection，即**依赖注入**，是实现IoC的其中一种设计方法。
其特征是通过一些技巧，将依赖的对象注入到调用者当中。（比如把Repository注入到Service当中）
这里说的技巧目前主要指的就是引入**容器**，先把所有会产生依赖的对象统一添加到容器当中，比如StudentRepository和StudentService，把分配权限交给容器，当StudentService内部需要使用StudentRepository时，这时不应该让它自己new出来一个，而是通过容器，把StudentRepository注入到StudentService当中。
这就是名称“依赖注入”的由来。
![Dependency Injection](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190704174344478-682084802.jpg)

### DI和IoC有什么区别

这是个老生常谈的问题了，而且这两个名字经常在各种大牛和伪大牛的吹逼现场频繁出现 ，听的新手云里雾里，莫名感到神圣不可侵犯。那么DI和IoC是同一个东西吗？如果不是，它们又有什么区别呢？
回答很简单：**不是一个东西**。
区别也很简单，一句话概括就是：**IoC是一种很宽泛的理念，DI是实现了IoC的其中一种方法**。
说到这里我已经感觉到屏幕后的你性感地添了一下嘴唇，囤积好口水，准备开始喷我了。
先别慌，我有证据，我们先来看下微软怎么说：
> ASP.NET Core supports the dependency injection (DI) software design pattern, which is a technique for achieving Inversion of Control (IoC) between classes and their dependencies.

地址：[https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.2)

翻译过来就是“_ASP.NET Core支持依赖注入（DI）的软件设计模式，该模式是一种在类和它依赖的对象之间实现了控制反转（IoC）的技术_”。

如果有人觉得辣鸡微软不够权威，那我们去看下IoC以及DI这两个概念的发明人——Martin Fowler怎么说：
> 几位轻量级容器的作者曾骄傲地对我说：这些容器非常有用，因为它们实现了控制反转。这样的说辞让我深感迷惑：控制反转是框架所共有的特征，如果仅仅因为使用了控制反转就认为这些轻量级容器与众不同，就好象在说我的轿车是与众不同的，因为它有四个轮子。
因此，我想我们需要给这个模式起一个更能说明其特点的名字——”控制反转”这个名字太泛了，常常让人有些迷惑。经与多位IoC 爱好者讨论之后，我们决定将这个模式叫做”依赖注入”（Dependency Injection）。

地址：[http://insights.thoughtworkers.org/injection/](http://insights.thoughtworkers.org/injection/)

Martin Fowler说的比较委婉，其实说白了就是建议我们，**不要乱用IoC装逼**，IoC是一种设计理念，很宽泛，_你把程序里的一个写死的变量改成从配置文件里读取也是一种控制反转（由**程序控制**反转为由**框架控制**），你把这个配置改成用户UI界面的一个输入文本框由用户输入也是一种控制反转（由**框架控制**反转为由**用户自己控制**）_。
所以，如果确定讨论的模式是DI，那么就表述为DI，还是尽量少用IoC这种宽泛的表达。

### 实战控制台程序依赖注入

目标很简单，就是控制台程序启动后，将学生姓名打印出来。
程序启动流程是，控制台主程序调用Service层，Service层调用Repository层获取数据（示例项目的仓储层没有连接数据库，只是直接造个假数据返回）。
没有依赖注入的情况下，肯定是主程序会new一个StudentService，StudentService里会new一个StudentRepository，现在引入依赖注入后，就不应该这么new出来了，而是通过容器注入，也就是容器会把StudentRepository自动注入到StudentService当中。

## 架构

### 实体层

![Entity](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190705141612498-192467374.png)

学生实体类StudentEntity：

```csharp
namespace Ray.EssayNotes.AutoFac.Model
{
    /// <summary>学生实体</summary>
    public class StudentEntity
    {
        /// <summary>唯一标识</summary>
        public long Id { get; set; }
        /// <summary>姓名</summary>
        public string Name { get; set; }
        /// <summary>成绩</summary>
        public int Grade { get; set; }
    }
}
```

### 仓储层

![Repository](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190705141745503-486248562.png)

IStudentRepository接口：

```csharp
using Ray.EssayNotes.AutoFac.Model;

namespace Ray.EssayNotes.AutoFac.Repository.IRepository
{
    /// <summary>学生仓储interface</summary>
    public interface IStudentRepository
    {
        string GetName(long id);
    }
}
```

StudentRepository仓储类：

```chsarp
using Ray.EssayNotes.AutoFac.Model;
using Ray.EssayNotes.AutoFac.Repository.IRepository;

namespace Ray.EssayNotes.AutoFac.Repository.Repository
{
    /// <summary>
    /// 学生仓储
    /// </summary>
    public class StudentRepository : IStudentRepository
    {
        public string GetName(long id)
        {
            return "学生张三";//造个假数据返回
        }
    }
}
```

### Service层

![Service](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190705141852070-1256475620.png)

IStudentService接口

```csharp
namespace Ray.EssayNotes.AutoFac.Service.IService
{
    /// <summary>
    /// 学生逻辑处理interface
    /// </summary>
    public interface IStudentService
    {
        string GetStuName(long id);
    }
}
```

StudentService类:

```csharp
using Ray.EssayNotes.AutoFac.Repository.IRepository;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IService;

namespace Ray.EssayNotes.AutoFac.Service.Service
{
    /// <summary>
    /// 学生逻辑处理
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        /// <summary>
        /// 构造注入
        /// </summary>
        /// <param name="studentRepository"></param>
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public string GetStuName(long id)
        {
            var stu = _studentRepository.Get(id);
            return stu.Name;
        }
    }
}

```

其中构造函数是一个有参的函数，参数是学生仓储，这个后面依赖注入时会用。

### AutoFac容器

![Container](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190705142010578-1627792237.png)

需要先通过Nuget导入Autofac包：
![Autoface package](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190705143325598-1976348318.png)

```csharp
using System;
using System.Reflection;
//
using Autofac;
using Autofac.Core;
//
using Ray.EssayNotes.AutoFac.Repository.IRepository;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IService;
using Ray.EssayNotes.AutoFac.Service.Service;

namespace Ray.EssayNotes.AutoFac.Infrastructure.Ioc
{
    /// <summary>
    /// 控制台程序容器
    /// </summary>
    public static class Container
    {
        /// <summary>
        /// 容器
        /// </summary>
        public static IContainer Instance;

        /// <summary>
        /// 初始化容器
        /// </summary>
        /// <returns></returns>
        public static void Init()
        {
            //新建容器构建器，用于注册组件和服务
            var builder = new ContainerBuilder();
            //自定义注册
            MyBuild(builder);
            //利用构建器创建容器
            Instance = builder.Build();
        }

        /// <summary>
        /// 自定义注册
        /// </summary>
        /// <param name="builder"></param>
        public static void MyBuild(ContainerBuilder builder)
        {
            builder.RegisterType<StudentRepository>().As<IStudentRepository>();
            builder.RegisterType<StudentService>().As<IStudentService>();
        }
    }
}

```

其中：

* public static IContainer Instance
为单例容器
* Init()方法
用于初始化容器，即往容器中添加对象，我们把这个添加的过程称为**注册**（Register）。
ContainerBuilder为AutoFac定义的**容器构造器**，我们通过使用它往容器内注册对象。
* MyBuild(ContainerBuilder builder)方法
我们具体注册的实现函数。RegisterType是AutoFac封装的一种最基本的注册方法，传入的泛型（StudentService）就是我们欲添加到容器的对象；As函数负责绑定注册对象的**暴露类型**，一般是以其实现的接口类型暴露，这个暴露类型是我们后面去容器内查找对象时使用的搜索标识，我们从容器外部只有通过暴露类型才能找到容器内的对象。

### 主程序

![Program](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190705142126846-1286011074.png)

需要先Nuget导入AutoFac程序包：
![Autofac package](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190705143325598-1976348318.png)

```csharp
using System;
//
using Autofac;
//
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Service.IService;


namespace Ray.EssayNotes.AutoFac.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Container.Init();//初始化容器，将需要用到的组件添加到容器中

            PrintStudentName(10001);

            Console.ReadKey();
        }

        /// <summary>
        /// 输出学生姓名
        /// </summary>
        /// <param name="id"></param>
        public static void PrintStudentName(long id)
        {
            //从容器中解析出对象
            IStudentService stuService = Container.Instance.Resolve<IStudentService>();
            string name = stuService.GetStuName(id);
            Console.WriteLine(name);
        }
     }
 }
```

进入Main函数，先调用容器的初始化函数，该函数执行成功后，StudentRepository和StudentService就被注册到容器中了。
然后调用打印学生姓名的函数，其中Resolve()方法是AutoFac封装的容器的解析方法，传入的泛型就是之前注册时的暴露类型，下面可以详细看下这一步到底发生了哪些事情：

1. 容器根据暴露类型解析对象

也就是容器会根据暴露类型IStudentService去容器内部找到其对应类（即StudentService），找到后会试图实例化一个对象出来。

1. 实例化StudentService

AutoFac容器在解析StudentService的时候，会调用StudentService的构造函数进行实例化。

1. 构造注入

AutoFac容器发现StudentService的构造函数需要一个IStudnetRepository类型的参数，于是会自动去容器内寻找，根据这个暴露类型找到对应的StudnetRepository后，自动将其注入到了StudentService当中

经过这几步，一个简单的基于依赖注入的程序就完成了。

---

## 运行结果

我们将控制台程序设置为启动项目，点击运行，如图调用成功：

![Console](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190705145101450-1330667943.png)

如果把调试断点加在容器初始化函数里，可以很清晰的看到哪些对象被注册到了容器里：
![Debug](https://img2018.cnblogs.com/blog/1327955/201907/1327955-20190705145627740-971128318.png)
