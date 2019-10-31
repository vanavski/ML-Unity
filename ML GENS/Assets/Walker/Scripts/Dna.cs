using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dna : MonoBehaviour {

    List<int> genes = new List<int>();
    int dnaLength = 0;
    int maxValue = 0;

    public Dna(int length, int value)
    {
        dnaLength = length;
        maxValue = value;
        SetRandom();
    }

    public void SetRandom()
    {
        genes.Clear();
        for (int i = 0; i < dnaLength; i++)
        {
            genes.Add(Random.Range(0, maxValue));
        }
    }

    public void SetInt(int pos, int value)
    {
        genes[pos] = value;
    }

    public void Combine(Dna d1, Dna d2)
    {
        for(int i = 0; i < dnaLength; i++)
        {
            if (i < dnaLength / 2)
            {
                genes[i] = d1.genes[i];
            }
            else
            {
                genes[i] = d2.genes[i];
            }

        }
    }

    public void Mutate()
    {
        genes[Random.Range(0, dnaLength)] = Random.Range(0, maxValue);
    }

    public int GetGene(int pos)
    {
        return genes[pos];
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
