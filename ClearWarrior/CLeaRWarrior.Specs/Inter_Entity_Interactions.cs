using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ClearWarrior.Specs.Inter_Entity_Interactions
{

    [TestFixture]
    public class When_an_entity_tries_to_move_through_another : using_context
    {
        public override void Because()
        {
            throw new System.NotImplementedException();
        }
    }

    public abstract class using_context
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            Context();
            Because();
        }

        public virtual void Context()
        {
            
        }

        public abstract void Because();
    }
}
