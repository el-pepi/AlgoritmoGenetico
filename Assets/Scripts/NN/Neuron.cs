using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron : MonoBehaviour {

    float value;
    float weight;

    public float Value
    {
        get
        {
            return value;
        }

        set
        {
            this.value = value;
        }
    }

    public float Weight
    {
        get
        {
            return weight;
        }

        set
        {
            weight = value;
        }
    }

    public void Calculate(Neuron[] inputs)
    {
        float val = 0;
        foreach(Neuron n in inputs)
        {
            val += n.value * n.weight;
        }

        val = 1 / (1 +Mathf.Pow(Mathf.Exp(1) , (val / 1)));
    }
}
