using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	Agent agent;

	int actualAction = -1;
	float actualTime = 0;
	Rigidbody2D rb;

	public float force = 5f;


	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		actualTime -= Time.deltaTime;
		if (actualTime <= 0) {
			FinishAction ();
		}

		switch (agent.chromosome.gens [actualAction].Action) {
		case 1:
			rb.AddForce (Vector3.right,force);
			break;
		case 2:
			rb.AddForce (Vector3.left,force);
			break;
		case 3:
			rb.AddForce (Vector3.up,force);
			break;
		}
	}

	void FinishAction(){
		actualAction++;
		if (actualAction >= agent.chromosome.gens.Length) {
			EndSimulation ();
		}
	}

	void EndSimulation(){
		agent.onFinish.Invoke ();
	}
}
