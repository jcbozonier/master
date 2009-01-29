using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelloWorldNeuralNetworks
{
    public class Network
    {
        private List<Neuron> _Neurons;
        private Dictionary<Neuron, List<Neuron>> _Connections;

        public Network()
        {
            _Connections = new Dictionary<Neuron, List<Neuron>>();
        }

        public void Add(Neuron neuron)
        {
            _Neurons.Add(neuron);
        }

        public void Connect(Neuron fromNeuron, Neuron toNeuron)
        {
            if(!_Connections.ContainsKey(fromNeuron))
            {
                _Connections.Add(fromNeuron, new List<Neuron>());
            }
            
            if(!_Connections[fromNeuron].Contains(toNeuron))
            {
                _Connections[fromNeuron].Add(toNeuron);
            }
        }

        public bool Execute()
        {
            var inputs = new List<Neuron>();
            var output = new OutputNeuron(0,0, 0);

            foreach(var pair in _Connections)
            {
                // These are the first inputs that should fire.
                if (pair.Key is InputNeuron)
                {
                    inputs.Add(pair.Key as InputNeuron);
                }

                foreach(var possibleOutput in pair.Value)
                {
                    if (possibleOutput is OutputNeuron)
                        output = possibleOutput as OutputNeuron;
                }
            }

            ProcessAndGetNext(inputs);
            return output.Value >= 1;
        }

        private void ProcessAndGetNext(List<Neuron> nextLayer)
        {
            var processNext = new List<Neuron>();
            foreach(var neuron in nextLayer)
            {
                foreach(var connection in _Connections[neuron])
                {
                    if(!processNext.Contains(connection)) 
                        processNext.Add(connection);
                    connection.ReceiveValue(neuron.Value * neuron.Weight);
                }
            }

            if(processNext.Count > 1) ProcessAndGetNext(processNext);
        }
    }

    public class InputNeuron : Neuron
    {
        public InputNeuron(double value, double weight)
            : base(0, weight)
        {
            Value = value;
        }
    }

    public class OutputNeuron : Neuron
    {
        public OutputNeuron(double threshold, double weight, double value) : base(threshold, weight)
        {
            Value = value;
        }
    }

    public class Neuron
    {
        private double _CacheValue { get; set; }
        public double Value { get; set; }
        public double Threshold { get; set; }
        public double Weight { get; set; }
        public string Name { get; set; }

        public EventHandler ThresholdCrossedEvent;

        public Neuron(double threshold, double weight)
        {
            Threshold = threshold;
            Weight = weight;
        }

        public void ReceiveValue(double value)
        {
            _CacheValue += value;

            if (_CacheValue >= Threshold)
            {
                Value = _CacheValue;
                _CacheValue = 0;
            }
        }

        public bool IsThresholdTriggered()
        {
            return Value >= Threshold;
        }
    }
}
