using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNShip : NNAgent {
    
    Rigidbody2D rb;

    public float force = 5f;



    GameObject explosion;

	public float up;
	public float down;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        explosion = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        explosion.SetActive(false);
        nn.score = 10000;

    }

    void FixedUpdate()
    {


        float[] inputs = new float[4];
		inputs[0] = rb.velocity.normalized.x;
		inputs[1] = rb.velocity.normalized.y;
		inputs[2] = (end.position - transform.position).normalized.x;
		inputs[3] = (end.position - transform.position).normalized.y;

        nn.SetInput(inputs);
        nn.Update();

        float[] outputs = nn.GetOutput();

		up = outputs [2];
		down = outputs [3];

		if (transform.position.y > end.position.y) {
			if (Mathf.Abs (end.position.x - transform.position.x) < 4 && rb.velocity.y < 0) {
				nn.score += 100 * Time.deltaTime;
			}
		} else {
			if (rb.velocity.y > 0) {
				nn.score += 100 * Time.deltaTime;
			}
		}

		if (transform.position.x < end.position.x) {
			if (rb.velocity.x < 0) {
				nn.score -= 100 * Time.deltaTime;
			}
		} else {
			if (rb.velocity.x > 0) {
				nn.score -= 100 * Time.deltaTime;
			}
		}

        rb.AddForce(Vector2.right * outputs[0] * force);
		rb.AddForce(Vector2.left * outputs[1] * force);
		rb.AddForce(Vector2.up * outputs[2] * force * 3f);
		rb.AddForce(Vector2.down * outputs[3] * force * 1.5f);

    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (!enabled)
            return;

        //Debug.LogWarning(c.relativeVelocity.magnitude);
        if (Mathf.Abs(c.relativeVelocity.y) > 5.5f)
        {
            explosion.SetActive(true);
            enabled = false;
            return;
        }
    }

	public override void SetScore()
    {
        nn.score -= Mathf.Pow(Vector3.Distance(transform.position, end.position), 3f);
        if (nn.score < 0)
        {
            nn.score = 0;
        }
    }

	public override void Restart()
    {
        rb.velocity = Vector2.zero;
		nn.score = 10000;
        enabled = true;
        explosion.SetActive(false);
    }
}
