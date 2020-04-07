using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;

namespace Ray.EssayNotes.AutoFac.Infrastructure.Ioc.Model
{
    public class ModelComponentRegistration
    {

        /// <summary>
        /// Gets a unique identifier for this component (shared in all sub-contexts.)
        /// This value also appears in Services.
        /// </summary>
        public Guid Id { get; set; }


        public string ActivatorName { get; set; }
        /// <summary>
        /// Gets the activator used to create instances.
        /// </summary>
        public object Activator { get; set; }


        /// <summary>
        /// Gets the services provided by the component.
        /// </summary>
        public IEnumerable<object> Services { get; set; }



        public string LifetimeName { get; set; }
        /// <summary>
        /// Gets the lifetime associated with the component.
        /// </summary>
        //public object Lifetime { get; set; }



        public string SharingName { get; set; }
        /// <summary>
        /// Gets a value indicating whether the component instances are shared or not.
        /// </summary>
        //public InstanceSharing Sharing { get; set; }



        public string OwnershipName { get; set; }
        /// <summary>
        /// Gets a value indicating whether the instances of the component should be disposed by the container.
        /// </summary>
        //public InstanceOwnership Ownership { get; set; }



        /// <summary>
        /// Gets additional data associated with the component.
        /// </summary>
        public IDictionary<string, object> Metadata { get; set; }


        public void SetSupplyFields(IComponentRegistration componentRegistration)
        {
            this.ActivatorName = componentRegistration.Activator.ToString();
            this.LifetimeName = componentRegistration.Lifetime.ToString();
            this.OwnershipName = componentRegistration.Ownership.ToString();
            this.SharingName = componentRegistration.Sharing.ToString();
        }
    }
}
