using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuralNetwork {

    Layer[] layers;
    public float score;

    List<Neuron> allNeurons = new List<Neuron>();

    public NeuralNetwork(int[] layerAmounts)
    {
        layers = new Layer[layerAmounts.Length];

        for (int i = 0; i < layerAmounts.Length; i++)
        {
            layers[i] = new Layer(layerAmounts[i]);
        }
        for (int i = 0; i < layerAmounts.Length-1; i++)
        {
            layers[i].SetNextLayer(layers[i+1]); ;
        }
        for (int i = 0; i < layerAmounts.Length; i++)
        {
            allNeurons.AddRange(layers[i].Neurons);
        }
    }

    public void SetInput(float[] inputs)
    {
        Neuron[] inputNeurons = layers[0].Neurons;
        for (int i = 0; i < inputs.Length; i++)
        {
            inputNeurons[i].value = inputs[i];
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
            toReturn[i] = outputNeurons[i].value;
        }

        return toReturn;
    }
    
    public void SetWeights(Dictionary<int, float[]> weights)
    {
        for (int i = 0; i < allNeurons.Count; i++)
        {
            allNeurons[i].weights = weights[i];
            //layers[i].SetWeights(weights[i]);
        }
    }

    public Dictionary<int,float[]> GetWeights()
    {
        Dictionary<int, float[]> toReturn = new Dictionary<int, float[]>();

        for (int i = 0; i < allNeurons.Count; i++)
        {
            toReturn.Add(i, allNeurons[i].weights);
        }

        return toReturn;
    }

    public void Mutate(float chanse,float amount)
    {
        if (Random.Range(0f, 100f) < chanse)
        {
            allNeurons[Random.Range(0, allNeurons.Count)].Mutate(amount);
        }
    }
}
