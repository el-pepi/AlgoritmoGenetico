using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layer {
    Neuron[] neurons;

    public Layer(int neuronAmount)
    {
        neurons = new Neuron[neuronAmount + 1];

        for (int i = 0; i < neurons.Length; i++)
        {
            neurons[i] = new Neuron();
            if ( i == neurons.Length - 1)
            {
                neurons[i].Value = 1;
            }
        }
    }

    public Neuron[] Neurons
    {
        get
        {
            return neurons;
        }

        set
        {
            neurons = value;
        }
    }

    public void Update(Layer input)
    {
        for (int i = 0; i < neurons.Length-1; i++)
        {
            neurons[i].Calculate(input.Neurons);
        }
    }

    public void SetWeights(float[] weights)
    {
        for (int i = 0; i < neurons.Length; i++)
        {
            neurons[i].Weight = weights[i];
        }
    }

    public float[] GetWeights()
    {
        float[] toReturn = new float[neurons.Length];

        for (int i = 0; i < neurons.Length; i++)
        {
            toReturn[i] = neurons[i].Weight;
        }

        return toReturn;
    }
}
