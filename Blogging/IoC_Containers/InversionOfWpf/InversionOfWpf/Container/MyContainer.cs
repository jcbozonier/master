using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InversionOfWpf.BusinessObjects;
using StructureMap;

namespace InversionOfWpf.Container
{
    public static class MyContainerBootStrapper
    {
        public static void BootstrapStructureMap()
        {

            // Initialize the static ObjectFactory container

            ObjectFactory.Initialize(x =>
            {
                x.ForRequestedType<INotificationService>().TheDefaultIsConcreteType<DialogNotifier>();
            });

        }
    }

    public static class TestableContainerBootStrapper
    {
        public static void BootstrapStructureMap()
        {

            // Initialize the static ObjectFactory container

            ObjectFactory.Initialize(x =>
            {
                x.ForRequestedType<INotificationService>().TheDefaultIsConcreteType<TestableNotifier>();
            });

        }
    }
}
