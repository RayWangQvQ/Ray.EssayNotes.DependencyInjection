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
            //Container.Init();//初始化容器，将需要用到的组件添加到容器中

            //PrintStudentName(10001);
            //PrintTeacherName(99999);
            //PrintBookTitle(123456);

            //TestInstancePerDependency();
            //TestSingleInstance();
            //TestInstancePerLifetimeScope();
            TestInstancePerMatchingLifetimeScope();


            Console.ReadKey();
        }

        /// <summary>
        /// 输出学生姓名
        /// </summary>
        /// <param name="id"></param>
        public static void PrintStudentName(long id)
        {
            //从容器中解析出对象（可以理解为根生命域(the “root lifetime scope”)
            //这种方式解析出的对象如果不能自动释放，其生命周期则与程序同样长，多了之后可能会造成内存溢出，这里只是为了好理解，并不建议使用
            //建议使用下面用的利用子生命域解析的方式
            IStudentService stuService = Container.Instance.Resolve<IStudentService>();
            string name = stuService.GetStuName(id);
            Console.WriteLine(name);
        }

        /// <summary>
        /// 输出教师姓名
        /// </summary>
        /// <param name="id"></param>
        public static void PrintTeacherName(long id)
        {
            // 创建一个生命域, 解析对象，使用完后, 自动释放掉所有解析资源
            using (ILifetimeScope scope = Container.Instance.BeginLifetimeScope())
            {
                ITeacherService teacherService = scope.Resolve<ITeacherService>();
                string name = teacherService.GetTeacherName(id);
                Console.WriteLine(name);
            }
        }

        /// <summary>
        /// 输出书本名称
        /// </summary>
        /// <param name="id"></param>
        public static void PrintBookTitle(long id)
        {
            // 创建一个生命域, 解析对象，使用完后, 自动释放掉所有解析资源
            using (ILifetimeScope scope = Container.Instance.BeginLifetimeScope())
            {
                IBookService bookService = scope.ResolveOptional<IBookService>();
                string title = bookService.GetTitle(id);
                Console.WriteLine(title);
            }
        }


        /// <summary>
        /// 生命周期——瞬时实例
        /// </summary>
        public static void TestInstancePerDependency()
        {
            //注册
            Container.Init(x =>
                {
                    x.RegisterType<Model.StudentEntity>().InstancePerDependency();
                    return x;
                }
            );
            using (var scope = Container.Instance.BeginLifetimeScope())
            {
                var stu1 = scope.Resolve<Model.StudentEntity>();
                Console.WriteLine($"第1次打印：{stu1.Name}");
                stu1.Name = "张三";
                Console.WriteLine($"第2次打印：{stu1.Name}");

                var stu2 = scope.Resolve<Model.StudentEntity>();
                Console.WriteLine($"第2次打印：{stu2.Name}");
                //解析了2次，有两个实例，stu1和stu2指向不同的两块内存，彼此之间没有关系
            }
        }

        /// <summary>
        /// 生命周期——单例
        /// </summary>
        public static void TestSingleInstance()
        {
            //注册
            Container.Init(x =>
                {
                    x.RegisterType<Model.StudentEntity>().SingleInstance();
                    return x;
                }
            );
            //解析
            //直接从根域内解析（单例下可以使用）
            var stu1 = Container.Instance.Resolve<Model.StudentEntity>();
            stu1.Name = "张三";
            Console.WriteLine($"第1次打印：{stu1.Name}");

            using (var scope1 = Container.Instance.BeginLifetimeScope())
            {
                var stu2 = scope1.Resolve<Model.StudentEntity>();
                Console.WriteLine($"第2次打印：{stu2.Name}");

                stu1.Name = "李四";
            }
            using (var scope2 = Container.Instance.BeginLifetimeScope())
            {
                var stu3 = scope2.Resolve<Model.StudentEntity>();
                Console.WriteLine($"第3次打印：{stu3.Name}");
            }
            //全局单例，解析3次只产生了同一个实例，指向同一个内存块
        }

        /// <summary>
        /// 生命周期域——周期域内单例
        /// </summary>
        public static void TestInstancePerLifetimeScope()
        {
            //注册
            Container.Init(x =>
                {
                    x.RegisterType<Model.StudentEntity>().InstancePerLifetimeScope();
                    return x;
                }
                );
            //子域一
            using (var scope1 = Container.Instance.BeginLifetimeScope())
            {
                var stu1 = scope1.Resolve<Model.StudentEntity>();
                Console.WriteLine($"第1次打印：{stu1.Name}");

                stu1.Name = "张三";

                var stu2 = scope1.Resolve<Model.StudentEntity>();
                Console.WriteLine($"第2次打印：{stu2.Name}");
                //解析了2次，但2次都是同一个实例（stu1和stu2指向同一个内存块Ⅰ）
            }
            //子域二
            using (var scope2 = Container.Instance.BeginLifetimeScope())
            {
                var stuA = scope2.Resolve<Model.StudentEntity>();
                Console.WriteLine($"第3次打印：{stuA.Name}");

                stuA.Name = "李四";

                var stuB = scope2.Resolve<Model.StudentEntity>();
                Console.WriteLine($"第4次打印：{stuB.Name}");
                //解析了2次，2次都是同一内存块（stuA和stuB指向同一个内存块Ⅱ）
                //但是内存块Ⅰ与内存块Ⅱ不是同一块内存
            }
        }

        /// <summary>
        /// 生命周期域——指定周期域内单例
        /// </summary>
        public static void TestInstancePerMatchingLifetimeScope()
        {
            //注册
            Container.Init(x =>
                {
                    x.RegisterType<Model.StudentEntity>().InstancePerMatchingLifetimeScope("myTag");
                    return x;
                }
            );
            //myScope标签子域一
            using (var myScope1 = Container.Instance.BeginLifetimeScope("myTag"))
            {
                var stu1 = myScope1.Resolve<Model.StudentEntity>();
                stu1.Name = "张三";
                Console.WriteLine($"第1次打印：{stu1.Name}");

                var stu2 = myScope1.Resolve<Model.StudentEntity>();
                Console.WriteLine($"第2次打印：{stu2.Name}");
                //解析了2次，但2次都是同一个实例（stu1和stu2指向同一个内存块Ⅰ）
            }
            //myScope标签子域二
            using (var myScope2 = Container.Instance.BeginLifetimeScope("myTag"))
            {
                var stuA = myScope2.Resolve<Model.StudentEntity>();
                Console.WriteLine($"第3次打印：{stuA.Name}");
                //因为标签域内已注册过，所以可以解析成功
                //但是因为和上面不是同一个子域，所以解析出的实例stuA与之前的并不是同一个实例，指向另一个内存块Ⅱ
            }
            //无标签子域三
            using (var noTagScope = Container.Instance.BeginLifetimeScope())
            {
                try
                {
                    var stuOne = noTagScope.Resolve<Model.StudentEntity>();//会报异常
                    Console.WriteLine($"第4次正常打印：{stuOne.Name}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"第4次异常打印：{e.Message}");
                }
                //因为StudentEntity只被注册到带有myScope标签域内，所以这里解析不到，报异常
            }
        }

        /// <summary>
        /// 生命周期域——每次请求内单例
        /// </summary>
        public static void TestInstancePerRequest()
        {
            //注册
            Container.Init(x =>
                {
                    x.RegisterType<Model.StudentEntity>().InstancePerRequest();
                    //x.RegisterType<Model.StudentEntity>().InstancePerMatchingLifetimeScope(Autofac.Core.Lifetime.MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
                    //x.RegisterType<Model.StudentEntity>().InstancePerMatchingLifetimeScope("AutofacWebRequest");
                    return x;
                }
            );
            
        }
    }
}

