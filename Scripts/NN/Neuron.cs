﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron {

    public float value;
    public float[] weights;
    

    public void Calculate(Neuron[] inputs,int index)
    {
        value = 0;
        foreach(Neuron n in inputs)
        {
            value += n.value * n.weights[index];
        }

        value = 1 / (1 +Mathf.Pow(Mathf.Exp(1) , (value / 1)));
    }

    public void Mutate(float amount)
    {
        weights[Random.Range(0, weights.Length)] += Random.Range(-amount,amount);
    }
}