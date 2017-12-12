using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : NNAgent {

	public GameObject bulletPref;

	public int hits;
	public int misses;

	float shootTimer = 1f;

	float angle=0;

	List<GameObject> bullets = new List<GameObject>();

	public float val=0;

	void Update () {
		shootTimer -= Time.deltaTime;
		if (shootTimer <= 0) {
			shootTimer = 1f;
			GameObject g = Instantiate (bulletPref,transform.position,transform.rotation);
			g.GetComponent<Bullet> ().owner = this;
			bullets.Add (g);
		}

		float[] input = new float[3];
		input [0] = (end.position - transform.position).normalized.x;
		input [1] = (end.position - transform.position).normalized.y;
		input [2] = angle;
		//input [3] = transform.position.x;
		//input [4] = transform.position.y;
		nn.SetInput (input);
		nn.Update ();

		val = nn.GetOutput ()[0];
		angle += ((val - 0.5f)*2)*5;
		//angle -= output [1]*5;

		transform.rotation = Quaternion.Euler (0,0,angle);
	}

	public override void Restart ()
	{
		angle = Random.Range (0f,360f);
		hits = misses = 0;
		while (bullets.Count > 0) {
			Destroy (bullets[0]);
			bullets.RemoveAt (0);
		}
		base.Restart ();
	}

	public override void SetScore ()
	{
		nn.score = hits;
		base.SetScore ();
	}
}
