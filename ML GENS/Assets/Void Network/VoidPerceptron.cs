using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class VoidTrainingSet
{
    public double[] input;
    public double output;
}

/// <summary>
/// Simple perceptron for void a ball
/// </summary>
public class VoidPerceptron : MonoBehaviour
{
    #region fields
    List<TrainingSet> ts = new List<TrainingSet>();
    double[] weights = { 0, 0 };
    double bias = 0;
    double totalError = 0;

    public GameObject npc;
    #endregion

    #region methods
    public void SendInput(double i1, double i2, double o)
    {
        //react
        double result = CalcOutput(i1, i2);
        Debug.Log(result);
        if (result == 0) //duck for cover
        {
            npc.GetComponent<Animator>().SetTrigger("Crouch");
            npc.GetComponent<Rigidbody>().isKinematic = false;
        }
        else
        {
            npc.GetComponent<Rigidbody>().isKinematic = true;
        }

        //learn from it for next time
        TrainingSet s = new TrainingSet();
        s.input = new double[2] { i1, i2 };
        s.output = o;
        ts.Add(s);
        Train();
    }

    /// <summary>
    /// logistic regression
    /// </summary>
    /// <param name="weights"></param>
    /// <param name="inputs"></param>
    /// <returns></returns>
    double DotProductBias(double[] weights, double[] inputs)
    {
        if (weights == null || inputs == null)
            return -1;

        if (weights.Length != inputs.Length)
            return -1;

        double d = 0;
        for (int x = 0; x < weights.Length; x++)
        {
            d += weights[x] * inputs[x];
        }

        d += bias;

        return d;
    }

    double CalcOutput(int i)
    {
        return ActivationFunction(DotProductBias(weights, ts[i].input));
    }

    /// <summary>
    /// Get binary system via values
    /// </summary>
    /// <param name="i1"></param>
    /// <param name="i2"></param>
    /// <returns></returns>
    double CalcOutput(double i1, double i2)
    {
        double[] inp = new double[] { i1, i2 };
        return ActivationFunction(DotProductBias(weights, inp));
    }

    double ActivationFunction(double dp)
    {
        if (dp > 0) return 1;
        return 0;
    }

    /// <summary>
    /// random all weights and bias
    /// </summary>
    void InitializeWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-1.0f, 1.0f);
        }
        bias = Random.Range(-1.0f, 1.0f);
    }

    /// <summary>
    /// reset weights and bias via error
    /// </summary>
    /// <param name="j"></param>
    void UpdateWeights(int j)
    {
        double error = ts[j].output - CalcOutput(j);
        totalError += Mathf.Abs((float)error);
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = weights[i] + error * ts[j].input[i];
        }
        bias += error;
    }

    void Train()
    {
        for (int t = 0; t < ts.Count; t++)
        {
            UpdateWeights(t);
        }
    }

    void LoadWeights()
    {
        string path = Application.dataPath + "/weights.txt";
        if (File.Exists(path))
        {
            var sr = File.OpenText(path);
            string line = sr.ReadLine();
            string[] w = line.Split(',');
            weights[0] = System.Convert.ToDouble(w[0]);
            weights[1] = System.Convert.ToDouble(w[1]);
            bias = System.Convert.ToDouble(w[2]);
            Debug.Log("loading");
        }
    }

    void SaveWeights()
    {
        string path = Application.dataPath + "/weights.txt";
        var sr = File.CreateText(path);
        sr.WriteLine(weights[0] + "," + weights[1] + "," + bias);
        sr.Close();
    }

    void Start()
    {
        InitializeWeights();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            InitializeWeights();
            ts.Clear();
        }
        else if (Input.GetKeyDown("s"))
            SaveWeights();
        else if (Input.GetKeyDown("l"))
            LoadWeights();
    }

    #endregion
}