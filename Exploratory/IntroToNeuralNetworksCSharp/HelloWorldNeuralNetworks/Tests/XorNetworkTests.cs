using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelloWorldNeuralNetworks;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class XorNetworkTests
    {
        [Test]
        public void Both_inputs_low()
        {
            var neuronA = new InputNeuron(0, 1);
            var neuronB = new InputNeuron(0, 1);

            var hiddenLayerA = new Neuron(1.5, -1);
            var hiddenLayerB = new Neuron(.5, 1);

            var result = new OutputNeuron(.5, 1, 0);

            var network = new Network();

            network.Connect(neuronA, hiddenLayerA);
            network.Connect(neuronA, hiddenLayerB);

            network.Connect(neuronB, hiddenLayerA);
            network.Connect(neuronB, hiddenLayerB);

            network.Connect(hiddenLayerA, result);
            network.Connect(hiddenLayerB, result);

            Assert.IsFalse(network.Execute());
        }

        [Test]
        public void Both_inputs_high()
        {
            var neuronA = new InputNeuron(1, 1);
            var neuronB = new InputNeuron(1, 1);

            var hiddenLayerA = new Neuron(1.5, -1);
            var hiddenLayerB = new Neuron(.5, 1);

            var result = new OutputNeuron(.5, 1, 0);

            var network = new Network();

            network.Connect(neuronA, hiddenLayerA);
            network.Connect(neuronA, hiddenLayerB);

            network.Connect(neuronB, hiddenLayerA);
            network.Connect(neuronB, hiddenLayerB);

            network.Connect(hiddenLayerA, result);
            network.Connect(hiddenLayerB, result);

            Assert.IsFalse(network.Execute());
        }

        [Test]
        public void Both_inputs_different()
        {
            var neuronA = new InputNeuron(0, 1);
            var neuronB = new InputNeuron(1, 1);

            var hiddenLayerA = new Neuron(1.5, -1);
            var hiddenLayerB = new Neuron(.5, 1);

            var result = new OutputNeuron(.5, 1, 0);

            var network = new Network();

            network.Connect(neuronA, hiddenLayerA);
            network.Connect(neuronA, hiddenLayerB);

            network.Connect(neuronB, hiddenLayerA);
            network.Connect(neuronB, hiddenLayerB);

            network.Connect(hiddenLayerA, result);
            network.Connect(hiddenLayerB, result);

            Assert.IsTrue(network.Execute());
        }
    }
}
