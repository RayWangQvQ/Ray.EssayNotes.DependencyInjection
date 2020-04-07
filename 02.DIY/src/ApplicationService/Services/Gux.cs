using ApplicationService.IServices;
using Ray.DependencyInjection;
using Ray.DependencyInjection.Attributes;
using Ray.DependencyInjection.Enums;

namespace ApplicationService.Services
{
    [MapTo(typeof(IGux), LifetimeEnum.Root)]
    public class Gux : Base, IGux { }
}
