using System.Collections.Generic;

public class Neuron {

	public int numInputs; //how many inputs come in neuron
	public double bias; //extra weight
	public double output; 
	public double errorGradient;
	public List<double> weights = new List<double>();
	public List<double> inputs = new List<double>(); //all of inputs

	public Neuron(int nInputs)
	{
		bias = UnityEngine.Random.Range(-1.0f,1.0f);
		numInputs = nInputs;
		for(int i = 0; i < nInputs; i++)
			weights.Add(UnityEngine.Random.Range(-1.0f,1.0f));
	}
}
