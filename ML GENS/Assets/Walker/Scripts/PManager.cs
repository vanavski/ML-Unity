using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Runners manager. Instantiate characters and teach it alive more time via directions.
/// </summary>
public class PManager : MonoBehaviour
{
    #region fields
    public GameObject botPrefab;
    public int populationSize = 50;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float TrialTime = 5;
    int generation = 1;

    GUIStyle guiStyle = new GUIStyle();

    #endregion

    #region methods
    /// <summary>
    /// Show generation and time
    /// </summary>
    private void OnGUI()
    {
        guiStyle.fontSize = 25;
        guiStyle.normal.textColor = Color.white;
        GUI.BeginGroup(new Rect(10, 10, 250, 150));
        GUI.Box(new Rect(0, 0, 140, 140), "Stats", guiStyle);
        GUI.Label(new Rect(10, 25, 20, 30), "Gen: " + generation, guiStyle);
        GUI.Label(new Rect(10, 50, 200, 30), string.Format("Time: {0:0.00}", elapsed), guiStyle);
        GUI.Label(new Rect(10, 75, 200, 30), "Population: " + population.Count, guiStyle);
        GUI.EndGroup();
    }

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 startingPos = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y, transform.position.z + Random.Range(-2, 2));
            GameObject bot = Instantiate(botPrefab, startingPos, transform.rotation);
            bot.GetComponent<Brain>().Init();
            population.Add(bot);
        }
    }

    /// <summary>
    /// Mix direction from 2 persons
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 startingPos = new Vector3(transform.position.x + Random.Range(-2, 2), transform.position.y, transform.position.z + Random.Range(-2, 2));
        GameObject offspring = Instantiate(botPrefab, startingPos, transform.rotation);
        Brain brain = offspring.GetComponent<Brain>();
        if (Random.Range(0, 100) == 1)
        {
            brain.Init();
            brain.dna.Mutate();
        }
        else
        {
            brain.Init();
            brain.dna.Combine(parent1.GetComponent<Brain>().dna, parent2.GetComponent<Brain>().dna);
        }
        return offspring;
    }

    /// <summary>
    /// Sort populations by time to die and breed it
    /// </summary>
    void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Brain>().timeAlive).ToList();
        population.Clear();
        for (int i = (sortedList.Count / 2) - 1; i < sortedList.Count - 1; i++)
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
        if (elapsed >= TrialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }
    #endregion
}