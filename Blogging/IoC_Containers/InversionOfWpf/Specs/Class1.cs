using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InversionOfWpf.BusinessObjects;
using InversionOfWpf.Container;
using NUnit.Framework;

namespace Specs
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Notifier_Test()
        {
            // We're not really testing anything here but!
            // it's a great illustration of how we can get rid of the UI.
            new NotifierModel().Notify("You've been clicked!");
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            TestableContainerBootStrapper.BootstrapStructureMap();

        }
    }
}
