using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Store gene with dna length and count if  abilities and genetic algorithm functionallity
/// </summary>
public class Dna : MonoBehaviour {

    #region fields
    List<int> genes = new List<int>();
    int dnaLength = 0;
    int maxValue = 0; //count of abilities
    #endregion

    #region methods
    public Dna(int length, int maxValue)
    {
        dnaLength = length;
        this.maxValue = maxValue;
        SetRandom();
    }

    /// <summary>
    /// Set random ability
    /// </summary>
    public void SetRandom()
    {
        genes.Clear();
        for (int i = 0; i < dnaLength; i++)
        {
            genes.Add(Random.Range(0, maxValue));
        }
    }

    /// <summary>
    /// Set value for specific gene by position
    /// </summary>
    /// <param name="pos"></param>
    /// <param name="value"></param>
    public void SetInt(int pos, int value)
    {
        genes[pos] = value;
    }

    /// <summary>
    /// Mix gens
    /// </summary>
    /// <param name="d1"></param>
    /// <param name="d2"></param>
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

    /// <summary>
    /// Some random mutate value in gens
    /// </summary>
    public void Mutate()
    {
        genes[Random.Range(0, dnaLength)] = Random.Range(0, maxValue);
    }

    public int GetGene(int pos)
    {
        return genes[pos];
    }
#endregion
}
