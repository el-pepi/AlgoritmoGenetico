using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneticAlg {

	float scoreSum = 0;
	float mutateChanse;
	float mutateAmount;

	float totalScore;

	public Agent[] CreatePopulation(int size,int gens,int actions,float maxTime,float mutationChanse,float mutationAmount){
		Agent[] population = new Agent[size];

		for (int i = 0; i < size; i++) {
			Gen[] g = new Gen[gens];
			for (int j = 0; j < gens; j++) {
				g [j] = new Gen (Random.Range(0,actions),Random.Range(0f,maxTime));
			}
			Chromosome c = new Chromosome (g);
			population [i] = new Agent (c);
		}
		mutateChanse = mutationChanse;
		mutateAmount = mutationAmount;
		return population;
	}

	public Agent[] Evolve(Agent[] agents,int elites){
		//Agent[] toReturn = new Agent[agents.Length];

		List<Agent> toReturn = new List<Agent> ();

		AgentComparer comparer = new AgentComparer();
		List<Agent> ags = new List<Agent> (agents);
		ags.Sort (comparer);

		/*for (int i = 0; i < ags.Count; i++) {
			Debug.Log (ags[i].chromosome.score);
		}*/

		for (int i = 0; i < elites; i++) {
			toReturn.Add(ags [i]);
		}
		totalScore = 0;
		foreach (Agent a in agents) {
			totalScore += a.chromosome.score;
		}

		while (toReturn.Count < agents.Length) {
			Agent a = RoulletePick (ags.ToArray());
			Agent b = RoulletePick (ags.ToArray());
			toReturn.Add (MixAgents(a,b));

			if (toReturn.Count < agents.Length) {
				toReturn.Add (MixAgents(b,a));
			}
		}

		return toReturn.ToArray ();
	}

	Agent MixAgents(Agent a, Agent b){
		if (a == null) {
			Debug.LogError ("wtf");
		}
		if (b == null) {
			Debug.LogError ("wtfbbb");
		}
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
		Mutate (child);
		return child;
	}

	void Mutate(Agent a){
		for (int i = 0; i < a.chromosome.gens.Length; i++) {
			if (Random.Range (0f, 100f) < mutateChanse) {
				a.chromosome.gens[i].Time += Random.Range (-mutateAmount, mutateAmount);
			}
		}
	}


	Agent RoulletePick(Agent[] agents){
		float rand = Random.Range (0f,totalScore-1);
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
		return a.chromosome.score > b.chromosome.score ? -1 : a.chromosome.score < b.chromosome.score ? 1 : 0;
	}
}