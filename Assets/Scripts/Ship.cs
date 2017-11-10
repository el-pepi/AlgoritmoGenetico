using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

	public Agent agent;

	int actualAction = -1;
	float actualTime = 0;
	Rigidbody2D rb;

	public float force = 5f;


	public Transform end;

	GameObject explosion;


	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		explosion = transform.GetChild (0).gameObject;
		explosion.SetActive (false);
	}

	void FixedUpdate () {
		actualTime -= Time.deltaTime;
		if (actualTime <= 0) {
			FinishAction ();
			if (!enabled) {
				return;
			}
		}

		switch (agent.chromosome.gens [actualAction].Action) {
		case 1:
			rb.AddForce (Vector2.right * force);
			break;
		case 2:
			rb.AddForce (Vector2.left * force);
			break;
		case 3:
			rb.AddForce (Vector2.up * force * 2);
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D c){
		if (!enabled)
			return;
		foreach (ContactPoint2D cp in c.contacts) {
			//Debug.Log (cp.relativeVelocity.magnitude);
			//agent.chromosome.score -= cp.relativeVelocity.magnitude;
			//if (cp.relativeVelocity.magnitude > 5.5f) {
				//explosion.SetActive (true);
				//EndSimulation ();
				//agent.chromosome.score -= 20;
				return;
			//}
		}
	}

	void FinishAction(){
		actualAction++;
		if (actualAction >= agent.chromosome.gens.Length) {
			EndSimulation ();
		} else {
			actualTime = agent.chromosome.gens[actualAction].Time;
		}
	}

	void EndSimulation(){
		agent.chromosome.score =100- Vector3.Distance (transform.position,end.position);

		Debug.Log (agent.chromosome.score);
		agent.onFinish.Invoke ();
		enabled = false;
	}
}
