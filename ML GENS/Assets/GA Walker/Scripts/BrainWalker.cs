using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainWalker : MonoBehaviour {

    private int DNALength = 2;
    public float timeAlive;
    public float timeWalking;
    public Dna dna;
    public GameObject eyes;
    bool alive = true;
    bool seeGround = true;

    //public GameObject ethanPrefab;
    //GameObject ethan;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "dead")
        {
            alive = false;
            timeAlive = 0;
            timeWalking = 0;
        }
    }

    //private void OnDestroy()
    //{
    //    Destroy(ethan);
    //}

    public void Init()
    {
        dna = new Dna(DNALength, 3);
        timeAlive = 0;
        alive = true;

        //ethan = Instantiate(ethanPrefab, transform.position, transform.rotation);
        //ethan.GetComponent<UnityStandardAssets.Characters.ThirdPerson.AICharacterControl>().target = transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (!alive) return;

        Debug.DrawRay(eyes.transform.position, eyes.transform.forward * 10, Color.red, 10);
        seeGround = false;
        RaycastHit hit;
        if (Physics.Raycast(eyes.transform.position, eyes.transform.forward * 10, out hit))
            if(hit.collider.gameObject.tag == "platform")
            {
                seeGround = true;
            }
        timeAlive = WalkerPM.elapsed;

        float turn = 0;
        float move = 0;

        if(seeGround)
        {
            if (dna.GetGene(0) == 0)
            {
                move = 1;
                timeWalking++;
            }
            else if (dna.GetGene(0) == 1) turn = -90;
            else if (dna.GetGene(0) == 2) turn = 90;

            //switch (dna.GetGene(0))
            //{
            //    case 0: move = 1;
            //        break;
            //    case 1: turn = -90;
            //        break;
            //    case 2: turn = 90;
            //        break;
            //}
        }
        else
        {
            if (dna.GetGene(1) == 0)
            {
                move = 1;
                timeWalking++;
            }
            else if (dna.GetGene(1) == 1) turn = -90;
            else if (dna.GetGene(1) == 2) turn = 90;

            //switch (dna.GetGene(1))
            //{
            //    case 0: move = 1;
            //        break;
            //    case 1: turn = -90;
            //        break;
            //    case 2: turn = 90;
            //        break;
            //}
        }

        transform.Translate(0, 0, move * 0.1f);
        transform.Rotate(0, turn, 0);
	}
}
