using UnityEngine;

public class InitCube : MonoBehaviour {

	public float s;
	public float c;

	// Use this for initialization
	void Awake () {
		s = Random.Range(0.1f,2.0f);
		transform.localScale *= s;
		c = Random.Range(0.0f, 1.0f);

		GetComponent<Renderer>().material.color = new Color(0,0,c);
	}
}
