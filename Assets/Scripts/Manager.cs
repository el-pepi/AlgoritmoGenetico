using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public int agentAmount = 10;
	public int gensPerChromosome = 10;
	public int eliteAmount = 3;
	public int actions = 4;

	public float mutationChanse = 10f;
	public float mutationAmount = 0.3f;

	public Transform end;
	public GameObject shipPrefab;
	List<GameObject> ships = new List<GameObject> ();

	Agent[] agents;
	GeneticAlg genAlg;
	int finishedAgents = 0;


	void Start () {
		genAlg = new GeneticAlg ();

		agents = genAlg.CreatePopulation (agentAmount,gensPerChromosome,actions,1f,mutationChanse,mutationAmount);

		foreach (Agent a in agents) {
			a.onFinish.AddListener (AgentFinished);
		}

		CreateObjects ();
	}

	void AgentFinished(){
		finishedAgents++;

		if (finishedAgents >= agents.Length) {
			finishedAgents = 0;

			Invoke ("FinishedSimulation",3f);
		}
	}

	void FinishedSimulation(){
		agents = genAlg.Evolve (agents,eliteAmount);

		foreach (Agent a in agents) {
			a.onFinish.RemoveAllListeners ();
			a.onFinish.AddListener (AgentFinished);
		}

		DestroyObjects ();
		CreateObjects ();
	}

	void DestroyObjects(){
		foreach (GameObject s in ships) {
			Destroy (s);
		}
		ships.Clear ();
	}

	void CreateObjects(){
		foreach (Agent a in agents) {
			GameObject g = Instantiate (shipPrefab,transform.position,Quaternion.identity);
			g.GetComponent<Ship> ().agent = a;
			g.GetComponent<Ship> ().end = end;
			ships.Add (g);
		}
	}
}
