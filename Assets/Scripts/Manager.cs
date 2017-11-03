using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

	public int agentAmount = 10;
	public int gensPerChromosome = 10;
	public int eliteAmount = 3;
	public int actions = 4;

	Agent[] agents;
	GeneticAlg genAlg;
	int finishedAgents = 0;


	void Start () {
		genAlg = new GeneticAlg ();

		agents = genAlg.CreatePopulation (agentAmount,gensPerChromosome,3,5f);

		foreach (Agent a in agents) {
			a.onFinish.AddListener (AgentFinished);
		}
	}

	void AgentFinished(){
		finishedAgents++;

		if (finishedAgents >= agents.Length) {
			finishedAgents = 0;
			agents = genAlg.Evolve (agents,eliteAmount);
		}
	}
}
