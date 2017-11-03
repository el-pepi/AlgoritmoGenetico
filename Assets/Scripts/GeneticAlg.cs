using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlg {

	float scoreSum = 0;

	public Agent[] CreatePopulation(int size,int gens,int actions,float maxTime){
		Agent[] population = new Agent[size];

		for (int i = 0; i < size; i++) {
			Gen[] g = new Gen[gens];
			for (int j = 0; j < gens; j++) {
				g [j] = new Gen (Random.Range(0,actions),Random.Range(0f,maxTime));
			}
			Chromosome c = new Chromosome (g);
			population [i] = new Agent (c);
		}

		return population;
	}

	public Agent[] Evolve(Agent[] agents,int elites){
		Agent[] toReturn = new Agent[agents.Length];

		AgentComparer comparer = new AgentComparer();
		List<Agent> ags = new List<Agent> (agents);
		ags.Sort (comparer);

		for (int i = 0; i < elites; i++) {
			toReturn [i] = ags [i];
		}

		for (int i = 0; i < agents.Length; i++) {
			scoreSum += agents [i].chromosome.score;
		}

		for (int i = elites; i < agents.Length ; i++) {
			Agent a = RoulletePick (agents);
			Agent b = RoulletePick (agents);
			toReturn [i] = MixAgents (a,b);
			if (i+1 < agents.Length) {
				i++;
				toReturn [i] = MixAgents (b,a);
			}
		}
		return toReturn;
	}

	Agent MixAgents(Agent a, Agent b){
		Gen[] g = new Gen[a.chromosome.gens.Length];
		for (int i = 0; i < g.Length; i++) {
			if (i < g.Length / 2) {
				g [i] = a.chromosome.gens [i];
			} else {
				g [i] = b.chromosome.gens [i];
			}
		}
		Chromosome c = new Chromosome (g);
		Agent child = new Agent (c);
		return child;
	}


	Agent RoulletePick(Agent[] agents){
		float rand = Random.Range (0f,scoreSum);
		scoreSum = 0;
		for (int i = 0; i < agents.Length; i++) {
			scoreSum += agents [i].chromosome.score;
			if (scoreSum >= rand) {
				return agents [i];
			}
		}
		return null;
	}
}

public class AgentComparer : IComparer<Agent>{
	public int Compare(Agent a,Agent b){
		return a.chromosome.score > b.chromosome.score ? 1 : a.chromosome.score < b.chromosome.score ? -1 : 0;
	}
}