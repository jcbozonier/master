using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SimpleMaths;
using SpecUnit;

namespace Specs
{
    [TestFixture]
    public class When_using_network_with_two_learned_patterns : using_context
    {
        [Test]
        public void Should_recognize_pattern_one()
        {
            Network.Recognize(Pattern1).ShouldEqual(Pattern1);
        }

        [Test]
        public void Should_recognize_pattern_two()
        {
            Network.Recognize(Pattern2).ShouldEqual(Pattern2);
        }

        [Test]
        public void Should_not_recognize_pattern_one_as_pattern_two()
        {
            Network.Recognize(Pattern1).ShouldNotEqual(Pattern2);
        }

        [Test]
        public void Should_not_recognize_pattern_two_as_pattern_one()
        {
            Network.Recognize(Pattern2).ShouldNotEqual(Pattern1);
        }
    }

    public class using_context
    {
        protected HopfieldPattern Pattern1;
        protected HopfieldPattern Pattern2;
        protected HopfieldNetwork Network;

        [TestFixtureSetUp]
        public void Setup()
        {
            Pattern1 = new HopfieldPattern(new[] { 0.0, 1.0, 0.0, 1.0 });
            Pattern2 = new HopfieldPattern(new[] { 1.0, 0.0, 0.0, 1.0 });

            Network = new HopfieldNetwork(4);

            Network.Train(Pattern1);
            Network.Train(Pattern2);
        }
    }
}
