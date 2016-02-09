using UnityEngine;
using System.Collections;

public class SpawnerScript : MonoBehaviour {

    public Transform HeadPrefab;
	private bool _started = false;
    private float nextHeadTime = 0.0f;
    private float dropRate = 2.5f;
//	GameObject _sfxObject = new GameObject();
//	AudioSource _sfxPlayer;
//	AudioClip clip1;

	public AudioClip[] sounds;
	
	void Awake()
	{
//		_sfxPlayer = (AudioSource)_sfxObject.AddComponent(typeof(AudioSource));
//		clip1 = (AudioClip)Resources.Load("headchop_01", typeof(AudioClip));  
	}
	
	public bool started
    {
        get
		{ 
			return _started;
		}
        set
		{
			nextHeadTime = 0.0f;
    		dropRate = 2.5f;
			_started = value; 
			if(!_started)
			{
				GameObject[] gos;
        		gos = GameObject.FindGameObjectsWithTag("Enemy");
        
        		foreach (GameObject go in gos)
				{
					Destroy(go);
				}
			}
		}
    }
	
	void Start() 
	{

    }

	
	void Update ()
	{
		if(_started)
		{
	        if (nextHeadTime < Time.time)
	        {
	            SpawnEgg();
	            nextHeadTime = Time.time + dropRate;
	
	            //Speed up the spawnrate for the next egg
	            dropRate *= .9f;
				//Debug.Log("drop rate = " + dropRate);
				
	            if(dropRate <  .2f)dropRate = 2f * .9f;
	        }
		}
	}

    void SpawnEgg()
    {
        float addXPos = Random.Range(-1.6f, 1.6f);
        Vector3 spawnPos = transform.position + new Vector3(addXPos,0,0);
        Instantiate(HeadPrefab, spawnPos, Quaternion.identity);
		
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
