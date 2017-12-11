using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {

    Layer[] layers;

    public NeuralNetwork(int[] layerAmounts)
    {
        layers = new Layer[layerAmounts.Length];

        for (int i = 0; i < layerAmounts.Length; i++)
        {
            layers[i] = new Layer(layerAmounts[i]);
        }
    }

    public void SetInput(float[] inputs)
    {
        Neuron[] inputNeurons = layers[0].Neurons;
        for (int i = 0; i < inputs.Length; i++)
        {
            inputNeurons[i].Value = inputs[i];
        }
    }

    public void Update()
    {
        for (int i = 1; i < layers.Length; i++)
        {
            layers[i].Update(layers[i-1]);
        }
    }

    public float[] GetOutput()
    {
        Neuron[] outputNeurons = layers[layers.Length - 1].Neurons;
        float[] toReturn = new float[outputNeurons.Length-1];

        for (int i = 0; i < toReturn.Length; i++)
        {
            toReturn[i] = outputNeurons[i].Value;
        }

        return toReturn;
    }

    void SetWeights(Dictionary<int, float[]> weights)
    {
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].SetWeights(weights[i]);
        }
    }

    Dictionary<int,float[]> GetWeights()
    {
        Dictionary<int, float[]> toReturn = new Dictionary<int, float[]>();

        for (int i = 0; i < layers.Length; i++)
        {
            toReturn.Add(i, layers[i].GetWeights());
        }

        return toReturn;
    }
}
