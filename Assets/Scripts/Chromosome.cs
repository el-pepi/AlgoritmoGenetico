using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome {
	public Gen[] gens;

	public float score = 0;

	public Chromosome(Gen[] gens){
		this.gens = gens;
	}
}
