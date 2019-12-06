﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainPong2 : MonoBehaviour
{

    public GameObject paddle;
    public GameObject ball;
    public bool human = false;
    public string backwallTag = "backwallr";
    public Text score;
    Rigidbody2D brb;
    float yvel;
    float paddleMaxSpeed = 15;
    public float numSaved = 0;
    public float numMissed = 0;

    ANNPong ann;

    void Start()
    {
        ann = new ANNPong(6, 1, 1, 4, 0.05);
        brb = ball.GetComponent<Rigidbody2D>();
    }


    List<double> Run(double bx, double by, double bvx, double bvy, double px, double py, double pv, bool train)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();
        inputs.Add(bx);
        inputs.Add(by);
        inputs.Add(bvx);
        inputs.Add(bvy);
        inputs.Add(px);
        inputs.Add(py);
        outputs.Add(pv);
        if (train)
            return (ann.Train(inputs, outputs));
        else
            return (ann.CalcOutput(inputs, outputs));
    }

    // Update is called once per frame
    void Update()
    {
        if (!human)
        {
            float posy = Mathf.Clamp(paddle.transform.position.y + (yvel * Time.deltaTime * paddleMaxSpeed), 8.8f, 17.4f);
            paddle.transform.position = new Vector3(paddle.transform.position.x, posy, paddle.transform.position.z);
            List<double> output = new List<double>();
            int layerMask = 1 << 9;
            RaycastHit2D hit = Physics2D.Raycast(ball.transform.position, brb.velocity, 1000, layerMask);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "tops") //reflect off top
                {
                    Vector3 reflection = Vector3.Reflect(brb.velocity, hit.normal);
                    hit = Physics2D.Raycast(hit.point, reflection, 1000, layerMask);
                }
                if (hit.collider != null && hit.collider.gameObject.tag == backwallTag)
                {
                    float dy = (hit.point.y - paddle.transform.position.y);

                    output = Run(ball.transform.position.x,
                                    ball.transform.position.y,
                                    brb.velocity.x, brb.velocity.y,
                                    paddle.transform.position.x,
                                    paddle.transform.position.y,
                                    dy, true);
                    yvel = (float)output[0];

                }
            }
            else
                yvel = 0;
        }
        score.text = numMissed + "";
    }
}
