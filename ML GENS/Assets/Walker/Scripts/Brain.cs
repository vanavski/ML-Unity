using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

/// <summary>
/// brain of character
/// </summary>
[RequireComponent(typeof(ThirdPersonCharacter))]
public class Brain : MonoBehaviour {

    #region fields
    public int DNALength = 1;
    public float timeAlive;
    public Dna dna;

    private ThirdPersonCharacter mCharacter;
    private Vector3 move;
    private bool jump;
    private bool alive = true;
    #endregion

    #region methods

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dead")
            alive = false;
    }

    /// <summary>
    /// Initialize dna, character and alive values
    /// </summary>
    public void Init()
    {
        dna = new Dna(DNALength, 6);
        mCharacter = GetComponent<ThirdPersonCharacter>();
        timeAlive = 0;
        alive = true;
    }

    /// <summary>
    /// Choose direction by gen value
    /// </summary>
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
#endregion
}
