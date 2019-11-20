using UnityEngine;

/// <summary>
/// Set direction for character
/// </summary>
public class BrainMaze : MonoBehaviour {

    #region fields
    private int DNALength = 2;
    public Dna dna;
    public GameObject eyes;
    bool seeWall = true;
    Vector3 startPosition;
    public float distanceTravelled = 0;
    bool alive = true;

    #endregion

    #region methods

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dead")
        {
            alive = false;
            distanceTravelled = 0;
        }
    }

    public void Init()
    {
        dna = new Dna(DNALength, 360);
        startPosition = transform.position;
    }
    
    void Update()
    {
        if (!alive) return;

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, Color.red, 10);
        seeWall = false;
        RaycastHit hit;
        if (Physics.SphereCast(eyes.transform.position, 0.1f, eyes.transform.forward, out hit, 0.5f))
            if (hit.collider.gameObject.tag == "wall")
                seeWall = true;
    }

    /// <summary>
    /// set translate, rotate and distance travelled
    /// </summary>
    void FixedUpdate()
    {
        if (!alive) return;

        float h = 0;
        float v = dna.GetGene(0);

        if (seeWall)
            h = dna.GetGene(1);

        transform.Translate(0, 0, v * 0.001f);
        transform.Rotate(0, h, 0);
        distanceTravelled = Vector3.Distance(startPosition, transform.position);
    }

    #endregion
}
