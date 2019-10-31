using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof(ThirdPersonCharacter))]
public class Brain : MonoBehaviour {

    public int DNALength = 1;
    public float timeAlive;
    public Dna dna;

    private ThirdPersonCharacter mCharacter;
    private Vector3 move;
    private bool jump;
    private bool alive = true;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dead")
            alive = false;
    }

    public void Init()
    {
        dna = new Dna(DNALength, 6);
        mCharacter = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;
    }

    public void FixedUpdate()
    {
        float h = 0;
        float v = 0;
        bool crouch = false;
        switch (dna.GetGene(0))
        {
            case 0: v = 1;
                break;
            case 1: v = -1;
                break;
            case 2:
                h = -1;
                break;
            case 3:
                h = 1;
                break;
            case 4:
                jump = true;
                break;
            case 5:
                crouch = true;
                break;
        }

        move = v * Vector3.forward + h * Vector3.right;
        mCharacter.Move(move, crouch, jump);
        jump = false;
        if (alive) timeAlive += Time.deltaTime;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
