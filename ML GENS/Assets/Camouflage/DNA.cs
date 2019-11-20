using UnityEngine;

/// <summary>
/// RGB, size, time to die
/// </summary>
public class DNA : MonoBehaviour {

    #region fields
    public float r;
    public float g;
    public float b;
    public float size;

    bool isDead = false;
    public float timeToDie = 0;
    SpriteRenderer sRenderer;
    Collider2D sCollider;
    #endregion

    #region methods
    private void OnMouseDown()
    {
        isDead = true;
        timeToDie = PopulationManager.elapsed;
        Debug.Log("Dead at: " + timeToDie);
        sRenderer.enabled = false;
        sCollider.enabled = false;
    }
    
    void Start () {
        sRenderer = GetComponent<SpriteRenderer>();
        sCollider = GetComponent<Collider2D>();
        sRenderer.color = new Color(r, g, b);
        transform.localScale = new Vector3(size, size, size);
	}
    #endregion
}