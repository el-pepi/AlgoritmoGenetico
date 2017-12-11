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
		agent.chromosome.score = 5000;
	}

	void FixedUpdate () {
		if (rb.velocity.y < 0) {
			agent.chromosome.score -= -10 * Time.deltaTime;
		}
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
		//foreach (ContactPoint2D cp in c.contacts) {
			//Debug.Log (cp.relativeVelocity.magnitude);
			//agent.chromosome.score -= cp.relativeVelocity.magnitude;
		Debug.LogWarning(c.relativeVelocity.magnitude);
		if (Mathf.Abs( c.relativeVelocity.y) > 5.5f) {
				explosion.SetActive (true);
				//agent.chromosome.score = -100;
				EndSimulation ();
				return;
			}
		//}
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

		agent.onFinish.Invoke ();
		enabled = false;
	}

	public void SetScore(){
		agent.chromosome.score -=Mathf.Pow( Vector3.Distance (transform.position,end.position),3f);
		Debug.Log (agent.chromosome.score,gameObject);
	}
}
