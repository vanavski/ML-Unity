using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Create new RGB population every elapsed time and breed it by time to die
/// </summary>
public class PopulationManager : MonoBehaviour
{
    #region fields
    public GameObject personPrefab;
    public int populationSize = 10;
    public List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    int trialTime = 10;
    int generation = 1;
    GUIStyle guiStyle = new GUIStyle();

    #endregion

    #region methods
    /// <summary>
    /// Show generation and time
    /// </summary>
    private void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial time: " + (int)elapsed, guiStyle);
    }

    /// <summary>
    /// Instantiate persons and add to list
    /// </summary>
    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0); //create random position
            GameObject person = Instantiate(personPrefab, pos, Quaternion.identity); //create an object

            //random RGB
            person.GetComponent<DNA>().r = Random.Range(0.0f, 1.0f);
            person.GetComponent<DNA>().g = Random.Range(0.0f, 1.0f);
            person.GetComponent<DNA>().b = Random.Range(0.0f, 1.0f);
            person.GetComponent<DNA>().size = Random.Range(0.5f, 1.0f);
            population.Add(person);
        }
    }

    /// <summary>
    /// Mix RGB from 2 persons
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    GameObject Breed(GameObject p1, GameObject p2)
    {
        Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-10f, 10f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = p1.GetComponent<DNA>();
        DNA dna2 = p1.GetComponent<DNA>();

        //mix RGB from 2 persons
        offspring.GetComponent<DNA>().r = Random.Range(0, 10) < 5 ? dna1.r : dna2.r;
        offspring.GetComponent<DNA>().g = Random.Range(0, 10) < 5 ? dna1.g : dna2.g;
        offspring.GetComponent<DNA>().b = Random.Range(0, 10) < 5 ? dna1.b : dna2.b;
        offspring.GetComponent<DNA>().size = Random.Range(0, 10) < 5 ? dna1.size : dna2.size;
        return offspring;
    }

    /// <summary>
    /// Sort populations by time to die and breed it
    /// </summary>
    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();
        population.Clear();

        for (var i = sortedList.Count / 2 - 1; i < sortedList.Count - 1; i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }
    
    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }
    #endregion
}
