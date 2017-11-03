using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Agent {

	public Chromosome chromosome;
	public UnityEvent onFinish;

	public Agent(Chromosome chromosome){
		this.chromosome = chromosome;
		onFinish = new UnityEvent ();
	}
}
