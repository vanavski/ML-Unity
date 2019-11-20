using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Population manager for labirint
/// </summary>
public class MazePM : MonoBehaviour {

    #region fields
    public GameObject botPrefab;
    public GameObject startPos;
    public int populationSize = 50;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    public float TrialTime = 5;
    int generation = 1;

    GUIStyle guiStyle = new GUIStyle();

    #endregion

    #region methods like Runners
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
            GameObject bot = Instantiate(botPrefab, startPos.transform.position, transform.rotation);
            bot.GetComponent<BrainMaze>().Init();
            population.Add(bot);
        }
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        GameObject offspring = Instantiate(botPrefab, startPos.transform.position, transform.rotation);
        BrainMaze brain = offspring.GetComponent<BrainMaze>();
        if (Random.Range(0, 100) == 1)
        {
            brain.Init();
            brain.dna.Mutate();
        }
        else
        {
            brain.Init();
            brain.dna.Combine(parent1.GetComponent<BrainMaze>().dna, parent2.GetComponent<BrainMaze>().dna);
        }
        return offspring;
    }

    void BreedNewPopulation()
    {
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<BrainMaze>().distanceTravelled).ToList();
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

    // Update is called once per frame
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
