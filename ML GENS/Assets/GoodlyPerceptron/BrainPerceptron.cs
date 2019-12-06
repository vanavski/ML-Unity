using System.Collections.Generic;
using UnityEngine;

public class BrainPerceptron : MonoBehaviour {
    
    // basic artificial neural network
    //we can mix up activation functions
    //better use Relu or Leaky ReLu on hidden layers and sigmoid on output layer

	ANN ann;
	double sumSquareError = 0;

	void Start () {
		ann = new ANN(2, 1, 1, 2, 0.8);
		
		List<double> result;
		
		for(int i = 0; i < 5000; i++)
		{
			sumSquareError = 0;
			result = Train(1, 1, 0);
			sumSquareError += Mathf.Pow((float)result[0] - 0,2); // idk what happened here. Need to check the end of 2nd video
			result = Train(1, 0, 1);
			sumSquareError += Mathf.Pow((float)result[0] - 1,2);
			result = Train(0, 1, 1);
			sumSquareError += Mathf.Pow((float)result[0] - 1,2);
			result = Train(0, 0, 0);
			sumSquareError += Mathf.Pow((float)result[0] - 0,2);
		}
		Debug.Log("SSE: " + sumSquareError); //if error near the 1, so we need more iterations or change values 

		result = Train(1, 1, 0);
		Debug.Log(" 1 1 " + result[0]);
		result = Train(1, 0, 1);
		Debug.Log(" 1 0 " + result[0]);
		result = Train(0, 1, 1);
		Debug.Log(" 0 1 " + result[0]); // u can change it to printf SUMBELLA!1
		result = Train(0, 0, 0);
		Debug.Log(" 0 0 " + result[0]);
	}

    /// <summary>
    /// Set parameters for training
    /// </summary>
    /// <param name="input1"></param>
    /// <param name="input2"></param>
    /// <param name="output"></param>
    /// <returns></returns>
    List<double> Train(double input1, double input2, double output) //can change it for more inputs and outputs. 
        //for example how to onload data from DB here and work with it and after that save it
	{
		List<double> inputs = new List<double>();
		List<double> outputs = new List<double>();
		inputs.Add(input1);
		inputs.Add(input2);
		outputs.Add(output);
		return (ann.Go(inputs,outputs));
	}
}
