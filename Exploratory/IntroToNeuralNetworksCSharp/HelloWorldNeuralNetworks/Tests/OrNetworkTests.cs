using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelloWorldNeuralNetworks;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class OrNetworkTests
    {
        [Test]
        public void Both_inputs_low()
        {
            var neuronA = new InputNeuron(0, 1);
            var neuronB = new InputNeuron(0, 1);
            var neuronC = new OutputNeuron(.9, 1, 0);

            var network = new Network();

            network.Connect(neuronA, neuronC);
            network.Connect(neuronB, neuronC);

            Assert.IsFalse(network.Execute());
        }

        [Test]
        public void Both_inputs_high()
        {
            var neuronA = new InputNeuron(1, 1);
            var neuronB = new InputNeuron(1, 1);
            var neuronC = new OutputNeuron(.9, 1, 0);

            var network = new Network();

            network.Connect(neuronA, neuronC);
            network.Connect(neuronB, neuronC);

            Assert.IsTrue(network.Execute());
        }

        [Test]
        public void Both_inputs_different()
        {
            var neuronA = new InputNeuron(0, 1);
            var neuronB = new InputNeuron(1, 1);
            var neuronC = new OutputNeuron(.9, 1, 0);

            var network = new Network();

            network.Connect(neuronA, neuronC);
            network.Connect(neuronB, neuronC);

            Assert.IsTrue(network.Execute());
        }
    }
}
