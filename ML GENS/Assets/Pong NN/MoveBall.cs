using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour {

    Vector3 ballSartPosition;
    Rigidbody2D rb;
    float speed = 400;
    public AudioSource blip;
    public AudioSource blop;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        ballSartPosition = this.transform.position;
        ResetBall();
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
            ResetBall();
    }

    public void ResetBall()
    {
        this.transform.position = ballSartPosition;
        rb.velocity = Vector3.zero;
        Vector3 direction = new Vector3(Random.Range(100, 300), Random.Range(-100, 100), 0).normalized;
        rb.AddForce(direction * speed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "backwall")
            blop.Play();
        else blip.Play();
    }
}
