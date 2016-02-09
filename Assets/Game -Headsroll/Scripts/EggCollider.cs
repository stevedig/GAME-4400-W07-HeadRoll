using UnityEngine;
using System.Collections;

public class EggCollider : MonoBehaviour {

  //  PlayerScript myPlayerScript;
	GuiScript myGuiScript;
	public AudioClip[] sounds;
	
	//Automatically run when a scene starts
    void Awake()
    {
		GameObject cam = GameObject.FindWithTag ("GameController");
		myGuiScript = cam.GetComponent<GuiScript>();
    }

    //Triggered by Unity's Physics
	void OnTriggerEnter(Collider theCollision)
    {
        //In this game we don't need to check *what* we hit; it must be the eggs
        GameObject collisionGO = theCollision.gameObject;
        Destroy(collisionGO);
		//audio.Play();
	    myGuiScript.addScore();//.theScore++;
		playsfx();
    }
	
	private void playsfx()
	{
		if(!GetComponent<AudioSource>().isPlaying)
		{ 
			int snd = Random.Range(0,sounds.Length);
		    GetComponent<AudioSource>().clip=sounds[snd];
			if(GetComponent<AudioSource>().clip != null)
			{
			    GetComponent<AudioSource>().Play(); 
			}
        }

	}
	
}
