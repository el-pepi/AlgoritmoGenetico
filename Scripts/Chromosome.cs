using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Chromosome {
	public Gen[] gens;

	public float score;

	public Chromosome(Gen[] gens){
		this.score = 0;
		this.gens = gens;
	}
}
