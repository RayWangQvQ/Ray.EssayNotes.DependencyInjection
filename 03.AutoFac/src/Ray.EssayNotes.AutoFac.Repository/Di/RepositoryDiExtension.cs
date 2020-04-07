using System.Reflection;
using Autofac;
using Ray.EssayNotes.AutoFac.Domain.IRepository;
using Ray.EssayNotes.AutoFac.Repository.Repository;

namespace Ray.EssayNotes.AutoFac.Service.Di
{
    public static class RepositoryDiExtension
    {
        public static Autofac.ContainerBuilder AddRepositories(this Autofac.ContainerBuilder builder)
        {
            Assembly appServiceAssembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(appServiceAssembly)
                .Where(cc => cc.Name.EndsWith("Repository"))//筛选具象类（concrete classes）
                .PublicOnly()//只要public访问权限的
                .Where(cc => cc.IsClass)//只要class型（主要为了排除值和interface类型）
                .AsImplementedInterfaces();//自动以其实现的所有接口类型暴露（包括IDisposable接口）

            //注册泛型仓储
            builder.RegisterGeneric(typeof(BaseRepository<>))
                .As(typeof(IBaseRepository<>));

            return builder;
        }
    }
}
