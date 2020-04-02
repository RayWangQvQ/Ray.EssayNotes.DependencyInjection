using System;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Infrastructure.Ioc;
using Ray.EssayNotes.AutoFac.Repository.Repository;
using Ray.EssayNotes.AutoFac.Service.IAppService;
using Ray.EssayNotes.AutoFac.Service.AppService;
using System.Collections.Generic;
using Autofac.Core;
using System.Reflection;

namespace Ray.EssayNotes.AutoFac.ConsoleApp.Test.TestRegister
{
    public class TestRegister09 : TestRegisterBase
    {
        public override string Title => "扫描程序集注册";

        /// <summary>
        /// 扫描程序集注册
        /// 【RegisterAssemblyTypes】
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        protected override Autofac.ContainerBuilder RegisterFunc(Autofac.ContainerBuilder builder)
        {
            Assembly[] assemblies = Infrastructure.Helpers.ReflectionHelper.GetAllAssemblies();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(cc => cc.Name.EndsWith("Repository") |//筛选程序集内所有具象类（concrete classes）
                            cc.Name.EndsWith("Service"))
                .Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
                .PublicOnly()//只要public访问权限的
                .Except<TeacherRepository>()//排除某类型
                //.As(x => x.GetInterfaces()[0])//反射出其实现的接口，默认以第一个接口类型暴露
                .AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）

            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IBaseRepository<>));

            return builder;
        }
    }
}
