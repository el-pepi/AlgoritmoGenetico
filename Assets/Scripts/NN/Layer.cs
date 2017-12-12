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
                neurons[i].value = 1;
            }
            //neurons[i].Weight = Random.Range(0f,1f);
        }
    }

	public void SetNextLayer(Layer l,float min)
    {
        for (int i = 0; i < neurons.Length; i++)
        {
            neurons[i].weights = new float[l.neurons.Length];
            for (int j = 0; j < neurons[i].weights.Length; j++)
            {
                neurons[i].weights[j] = Random.Range(min, 1f);
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

	public void Update(Layer input,float pendiente)
    {
        for (int i = 0; i < neurons.Length-1; i++)
        {
			neurons[i].Calculate(input.Neurons,i,pendiente);
        }
    }
    /*
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
    }*/
}
