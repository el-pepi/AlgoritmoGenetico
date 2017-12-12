using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public Tank owner;

	float lifeTime = 3f;
	void Start () {
		GetComponent<Rigidbody2D> ().velocity = transform.up * 10;
	}

	void Update () {
		lifeTime -= Time.deltaTime;
		if (lifeTime <= 0) {
			owner.misses++;
			gameObject.SetActive (false);

		}
	}

	void OnTriggerEnter2D(Collider2D col){
		owner.hits++;
		gameObject.SetActive (false);
	}
}
