using UnityEngine;

[System.Serializable]
public class GraphTrainingSet
{
    public double[] input;
    public double output;
}

public class GraphPerceptron : MonoBehaviour
{
    #region fields
    public GraphTrainingSet[] trainingSet;
    double[] weights = { 0, 0 };
    double bias = 0;

    public SimpleGrapher simpleGraph;
    #endregion

    #region methods
    double DotProductBias(double[] v1, double[] v2)
    {
        if (v1 == null || v2 == null)
            return -1;

        if (v1.Length != v2.Length)
            return -1;

        double d = 0;
        for (int x = 0; x < v1.Length; x++)
        {
            d += v1[x] * v2[x];
        }

        d += bias;

        return d;
    }

    double CalcOutput(int i)
    {
        double dp = DotProductBias(weights, trainingSet[i].input);
        if (dp >= 0) return (1);
        return (0);
    }

    double CalcOutput(double i1, double i2)
    {
        double[] inp = new double[] { i1, i2 };
        double dp = DotProductBias(weights, inp);
        if (dp >= 0) return (1);
        return (0);
    }

    void UpdateWeights(int j)
    {
        double error = trainingSet[j].output - CalcOutput(j);
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = weights[i] + error * trainingSet[j].input[i];
        }
        bias += error;
    }

    void InitialiseWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-1.0f, 1.0f);
        }
        bias = Random.Range(-1.0f, 1.0f);
    }

    void Train(int epochs)
    {
        InitialiseWeights();
        for (int e = 0; e < epochs; e++)
        {
            for (int t = 0; t < trainingSet.Length; t++)
            {
                UpdateWeights(t);
                Debug.Log("W1: " + (weights[0]) + "W2: " + (weights[1]) + "B: " + bias);
            }
        }

    }

    void DrawAllPoints()
    {
        for (int t = 0; t < trainingSet.Length; t++)
        {
            if (trainingSet[t].output == 0)
                simpleGraph.DrawPoint((float)trainingSet[t].input[0], (float)trainingSet[t].input[1], Color.magenta);
            else
                simpleGraph.DrawPoint((float)trainingSet[t].input[0], (float)trainingSet[t].input[1], Color.green);
        }
    }

    void Start()
    {
        simpleGraph.Init();
        DrawAllPoints();
        Train(200);
        simpleGraph.DrawRay((float)(-(bias / weights[1]) / (bias / weights[0])), (float)(-bias / weights[1]), Color.red);

        if (CalcOutput(0.3, 0.9) == 0)
            simpleGraph.DrawPoint(0.3f, 0.9f, Color.red);
        else
            simpleGraph.DrawPoint(0.3f, 0.9f, Color.yellow);

        if (CalcOutput(0.8, 0.1) == 0)
            simpleGraph.DrawPoint(0.8f, 0.1f, Color.red);
        else
            simpleGraph.DrawPoint(0.8f, 0.1f, Color.yellow);

    }
    #endregion
}
