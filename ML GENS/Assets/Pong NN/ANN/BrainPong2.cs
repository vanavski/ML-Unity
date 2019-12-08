using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrainPong2 : MonoBehaviour
{
    #region Fields
    public GameObject paddle;
    public GameObject ball;
    public bool human = false;
    public string backwallTag = "backwallr";
    public Text score;
    Rigidbody2D brb;
    float yVelocity;
    float paddleMaxSpeed = 15;
    public float numSaved = 0;
    public float numMissed = 0;

    ANNPong ann;
    #endregion

    #region Methods
    void Start()
    {
        ann = new ANNPong(6, 1, 1, 4, 0.05);
        brb = ball.GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Run the perceptron work
    /// </summary>
    /// <param name="ballX"></param>
    /// <param name="ballY"></param>
    /// <param name="ballVelocityX"></param>
    /// <param name="ballVelocityY"></param>
    /// <param name="paddleX"></param>
    /// <param name="paddleY"></param>
    /// <param name="paddleVelocity"></param>
    /// <param name="train"></param>
    /// <returns></returns>
    List<double> Run(double ballX, double ballY, double ballVelocityX, 
        double ballVelocityY, double paddleX, double paddleY, double paddleVelocity, bool train)
    {
        List<double> inputs = new List<double>();
        List<double> outputs = new List<double>();
        inputs.Add(ballX);
        inputs.Add(ballY);
        inputs.Add(ballVelocityX);
        inputs.Add(ballVelocityY);
        inputs.Add(paddleX);
        inputs.Add(paddleY);
        outputs.Add(paddleVelocity);
        if (train)
            return (ann.Train(inputs, outputs));
        else
            return (ann.CalcOutput(inputs, outputs));
    }

    /// <summary>
    /// Calculating of positions for NN
    /// </summary>
    void Update()
    {
        if (!human)
        {
            float posY = Mathf.Clamp(paddle.transform.position.y + (yVelocity * Time.deltaTime * paddleMaxSpeed), 8.8f, 17.4f);
            paddle.transform.position = new Vector3(paddle.transform.position.x, posY, paddle.transform.position.z);
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
                    float deltaY = (hit.point.y - paddle.transform.position.y);

                    output = Run(ball.transform.position.x,
                                    ball.transform.position.y,
                                    brb.velocity.x, brb.velocity.y,
                                    paddle.transform.position.x,
                                    paddle.transform.position.y,
                                    deltaY, true);
                    yVelocity = (float)output[0];

                }
            }
            else
                yVelocity = 0;
        }
        score.text = numMissed + "";
    }
    #endregion
}
