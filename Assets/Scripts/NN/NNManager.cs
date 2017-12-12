using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNManager : MonoBehaviour {

    public GameObject shipPrefab;
    public Transform platform;
    public int shipAmount = 20;
    public int[] layerSizes;
    public int elites = 3;
    public float mutateChanse = 5f;
    public float mutateAmount = 0.1f;

    public Vector2 playArea;

    List<GameObject> ships = new List<GameObject>();
    List<NeuralNetwork> networks = new List<NeuralNetwork>();

	// Use this for initialization
	void Start () {
        int i = 0;
        while (i < shipAmount)
        {
            i++;

            NeuralNetwork nn = new NeuralNetwork(layerSizes);

            GameObject s = Instantiate(shipPrefab);
            s.GetComponent<NNShip>().nn = nn;
            s.GetComponent<NNShip>().end = platform;
            networks.Add(nn);
            ships.Add(s);
        }
        ReArrange();

        StartCoroutine(Test());
	}
	
    IEnumerator Test()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            FinishedSimulation();
            ReArrange();
        }
    }

    void ReArrange()
    {
        //platform.position = RandomPos();
        foreach(GameObject s in ships)
        {
            s.transform.position = RandomPos();
            s.GetComponent<NNShip>().Restart();
        }
    }

    Vector3 RandomPos()
    {
        return Vector3.zero;
        return new Vector3(Random.Range(-playArea.x, playArea.x), Random.Range(-playArea.y, playArea.y), 0);
    }

    float totalScore = 0;
    void FinishedSimulation()
    {
        totalScore = 0;
        foreach (GameObject g in ships)
        {
            g.GetComponent<NNShip>().SetScore();
        }
        foreach(NeuralNetwork n in networks)
        {
            totalScore += n.score;
        }

        NNShipComparer comparer = new NNShipComparer();
        networks.Sort(comparer);
        List<NeuralNetwork> newNets = new List<NeuralNetwork>();

        for (int i = 0; i < elites; i++)
        {
            newNets.Add(networks[i]);
        }
        while (newNets.Count < shipAmount)
        {
            NeuralNetwork a = RoulletePick(networks.ToArray());
            NeuralNetwork b = RoulletePick(networks.ToArray());

            NeuralNetwork r = MixNets(a, b);

            newNets.Add(r);
            r.Mutate(mutateChanse,mutateAmount);
            if(newNets.Count < shipAmount)
            {
                r = MixNets(b, a);
                r.Mutate(mutateChanse,mutateAmount);
                newNets.Add(r);
            }
        }

        networks = newNets;
        for (int i = 0; i < ships.Count; i++)
        {
            ships[i].GetComponent<NNShip>().nn = networks[i];
        }
    }

    NeuralNetwork RoulletePick(NeuralNetwork[] agents)
    {
        float rand = Random.Range(0f, totalScore - 1);
        float scoreSum = 0;
        for (int i = 0; i < agents.Length; i++)
        {
            scoreSum += agents[i].score;
            if (scoreSum >= rand)
            {
                return agents[i];
            }
        }
        return null;
    }

    NeuralNetwork MixNets(NeuralNetwork a, NeuralNetwork b)
    {
        NeuralNetwork n = new NeuralNetwork(layerSizes);

        Dictionary<int, float[]> w = new Dictionary<int, float[]>();
        
        Dictionary<int, float[]> wA = a.GetWeights();
        Dictionary<int, float[]> wB = b.GetWeights();

        int pivot = Random.Range(0,wA.Keys.Count);

        for (int i = 0; i < wA.Keys.Count; i++)
        {
            if (i < pivot)
            {
                w.Add(i,wA[i]);
            }
            else
            {
                w.Add(i, wB[i]);
            }
        }
        n.SetWeights(w);
        return n;
    }
}

public class NNShipComparer : IComparer<NeuralNetwork>
{
    public int Compare(NeuralNetwork a, NeuralNetwork b)
    {
        return a.score > b.score ? -1 : a.score < b.score ? 1 : 0;
    }
}