using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NNShip : MonoBehaviour {
    
    Rigidbody2D rb;

    public float force = 5f;


    public Transform end;

    GameObject explosion;

    public NeuralNetwork nn;

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
        if (rb.velocity.y < 0)
        {
            nn.score -= -10 * Time.deltaTime;
        }
        float[] inputs = new float[4];
        inputs[0] = rb.velocity.x;
        inputs[1] = rb.velocity.y;
        inputs[2] = end.position.x - rb.position.x;
        inputs[3] = end.position.y - rb.position.y;

        nn.SetInput(inputs);
        nn.Update();

        float[] outputs = nn.GetOutput();

        rb.AddForce(Vector2.right * outputs[0] * force);
        rb.AddForce(Vector2.left * outputs[1] * force);
        rb.AddForce(Vector2.up * outputs[2] * force *2);


        /*actualTime -= Time.deltaTime;
        if (actualTime <= 0)
        {
            FinishAction();
            if (!enabled)
            {
                return;
            }
        }*/

        /*switch (agent.chromosome.gens[actualAction].Action)
        {
            case 1:
                rb.AddForce(Vector2.right * force);
                break;
            case 2:
                rb.AddForce(Vector2.left * force);
                break;
            case 3:
                rb.AddForce(Vector2.up * force * 2);
                break;
        }*/

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

    public void SetScore()
    {
        nn.score -= Mathf.Pow(Vector3.Distance(transform.position, end.position), 3f);
        if (nn.score < 0)
        {
            nn.score = 0;
        }
        //Debug.Log(agent.chromosome.score, gameObject);
    }

    public void Restart()
    {
        rb.velocity = Vector2.zero;
        nn.score = 0;
        enabled = true;
        explosion.SetActive(false);
    }
}
