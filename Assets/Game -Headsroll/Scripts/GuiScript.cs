using UnityEngine;
using System.Collections;
[ExecuteInEditMode()] 
public class GuiScript : MonoBehaviour {
	//game vars..
	private int theScore = 0;
	//private bool running = false;
	
	//for txt
	public Font font;
	private float xpos= 0;
	private float ypos = 0;
	private float width = 0;
	private float height = 0;
	private GUIStyle loadstyle = null;
	private GUIStyle timerstyle = null;
	private GUIStyle scorestyle = null;
	private GUIStyle gameoverstyle = null;
	private GUIStyle countdownstyle = null;
	private GUIStyle timebonusstyle = null;
	//private bool fadegui = false;
	
	//timer countdown for game
	private float startTime;
	private float restSeconds;
	private int roundedRestSeconds;
	private float displaySeconds;
	private float displayMinutes;
	public int CountDownSeconds=60;
	public GameObject joy;
	private float Timeleft;
	string timetext;
	private float txtalpha = .8f;
	public enum GameStates 
	{
		loadscreen,
		playing,
		GameOver
	}
	
	public GameStates state = GameStates.loadscreen;
	//for calls to tell other scripts to start
	PlayerScript myPlayerScript;
	SpawnerScript mySpawnerScript;
	int nextbonus = 5;
	
	int fs = 10;
	public float fontmulti = .08f;
	//utility
	private bool m_Fading = false;
	AudioSource audio1;
	AudioSource audio2;
	AudioSource audio3;
	AudioSource audio4;
	public int timebonusadd = 7;
	public AudioClip[] sounds;
	
	public void addScore()
	{
		theScore++;
		playsfx(0);
		if(theScore == nextbonus)
		{
			playsfx(1);
			nextbonus +=5;
			CountDownSeconds += timebonusadd;
			showtimebonus();
		//	StartBumpSize(40,.015f,timebonusstyle);
		//	StartBumpSize(40,.015f,scorestyle);
		//	StartBumpSize(70,.015f,timerstyle);
		}
	}
    //Automatically run when a scene starts
    void Awake()
    {
		//other scripts to start..
		GameObject respawn = GameObject.FindWithTag ("Respawn");
		mySpawnerScript = respawn.GetComponent<SpawnerScript>();
        GameObject player = GameObject.FindWithTag ("Player");
		myPlayerScript = player.GetComponent<PlayerScript>();
	}

	// Use this for initialization
	void Start ()
	{
		xpos = 0;//Screen.width * .5f;
		ypos = Screen.height * .2f;
		width = Screen.width;
		height = Screen.height * .1f;
		
		Color tmpcolor = Color.red;
		tmpcolor.a = 0.8f;
		
		loadstyle = new GUIStyle();
		loadstyle.font = font;
		
		float fs1 =   Screen.width * fontmulti;					
		fs = (int)fs1;
		Debug.Log ("fs = " + fs);
		loadstyle.fontSize = fs;
		loadstyle.normal.textColor = tmpcolor;
		loadstyle.alignment = TextAnchor.MiddleCenter;
		
		timerstyle = new GUIStyle();
		timerstyle.font = font;
		timerstyle.fontSize = fs;
		timerstyle.normal.textColor =tmpcolor;
		timerstyle.alignment = TextAnchor.MiddleRight;
		
		scorestyle = new GUIStyle();
		scorestyle.font = font;
		scorestyle.normal.textColor = tmpcolor;
		scorestyle.fontSize = fs;
		scorestyle.alignment = TextAnchor.MiddleLeft;
		startTime=Time.deltaTime;
		
		gameoverstyle = new GUIStyle();
		gameoverstyle.font = font;
		gameoverstyle.fontSize = fs;
		gameoverstyle.normal.textColor = tmpcolor;
		gameoverstyle.alignment = TextAnchor.MiddleCenter;
		
		countdownstyle = new GUIStyle();
		countdownstyle.font = font;
		countdownstyle.fontSize = fs * 2;
		tmpcolor.a = 0.5f;
		countdownstyle.normal.textColor = tmpcolor;
		countdownstyle.alignment = TextAnchor.MiddleCenter;
		
		timebonusstyle = new GUIStyle();
		timebonusstyle.font = font;
		timebonusstyle.fontSize = fs;
		tmpcolor.a = 0.8f;
		timebonusstyle.normal.textColor = tmpcolor;
		timebonusstyle.alignment = TextAnchor.MiddleCenter;
		AudioSource[] asrcs = gameObject.GetComponents<AudioSource>();
		audio1 = asrcs[0];
		audio2 = asrcs[1];
		audio3 = asrcs[2];
		audio4 = asrcs[3];
		joy.SetActive(false);
	}
	

	int lastcount = 0;
	 void OnGUI()
    {
		if(state == GameStates.playing)
		{
			
			//here we are playing	
		//	if(m_Fading)
		//	{
		//		ypos = Screen.height * .1f;
		//	
		//		GUI.Box(new Rect(xpos,ypos,width,height), "Heads will Roll",loadstyle);
		//		//loadstyle.fontSize -= 20;
		//		GUI.Box(new Rect(xpos,ypos + height  ,width,height), "tap screen to Start",loadstyle);
		//	}
			
			
			ypos = Screen.height - fs;

			Color regcol = scorestyle.normal.textColor;
			scorestyle.normal.textColor = Color.black;
			GUI.Box(new Rect(xpos+2,ypos+2,width,height), "Score: " + theScore,scorestyle);
			scorestyle.normal.textColor = regcol;


			GUI.Box(new Rect(xpos,ypos,width,height), "Score: " + theScore,scorestyle);
			
			//if(theScore > 10)  CountDownSeconds += 20;
			
			//timer
	        Timeleft= Time.time-startTime;
 			restSeconds = CountDownSeconds-(Timeleft);
 			roundedRestSeconds=Mathf.CeilToInt(restSeconds);
			displaySeconds = roundedRestSeconds % 60;
			displayMinutes = (roundedRestSeconds / 60)%60;			
 
			timetext = (displayMinutes.ToString()+":");
			if (displaySeconds > 9)
			{
    			timetext = timetext + displaySeconds.ToString();
			}
			else 
			{
    			timetext = timetext + "0" + displaySeconds.ToString();
			}
			regcol = timerstyle.normal.textColor;
			timerstyle.normal.textColor = Color.black;
			GUI.Label(new Rect(xpos+2 ,0+2,width,height), timetext,timerstyle);	
			timerstyle.normal.textColor = regcol;

			GUI.Label(new Rect(xpos ,0,width,height), timetext,timerstyle);

			if((int)displayMinutes == 0 && (int) displaySeconds <= 5)
			{
				 regcol = countdownstyle.normal.textColor;
				countdownstyle.normal.textColor = Color.black;
				GUI.Label(new Rect(2 ,2,Screen.width ,Screen.height * .7f), displaySeconds.ToString(),countdownstyle);	
				countdownstyle.normal.textColor = regcol;

				GUI.Label(new Rect(0 ,0,Screen.width ,Screen.height * .7f), displaySeconds.ToString(),countdownstyle);	
				if(lastcount != displaySeconds)
				{
					playsfx(2);
		//			StartBumpSize( 30,.01f,countdownstyle);
					lastcount = (int)displaySeconds;
				}
			}
			
			if(_showtimebonus)
			{
				 regcol = timebonusstyle.normal.textColor;
				timebonusstyle.normal.textColor = Color.black;
				GUI.Label(new Rect(2 ,2,Screen.width ,Screen.height * .6f), "time bonus!",timebonusstyle);	
				timebonusstyle.normal.textColor = regcol;


				GUI.Label(new Rect(0 ,0,Screen.width ,Screen.height * .6f), "time bonus!",timebonusstyle);	
				
			}
			
			if((int)displayMinutes == 0 && (int) displaySeconds == 0)
			{
				state = GameStates.GameOver;
				mySpawnerScript.started = false;
				myPlayerScript.started = false;
		//		StartFade(.0f,.02f,gameoverstyle);
				playsfx(3);
				joy.SetActive(false);
				
			}
		}
		else if (state == GameStates.loadscreen)
		{
			//Here we are on the load screen..

			ypos = Screen.height * .1f;

			Color regcol = loadstyle.normal.textColor;
			loadstyle.normal.textColor = Color.black;
			GUI.Box(new Rect(xpos+2,ypos+2,width,height), "Heads will Roll",loadstyle);
			//loadstyle.fontSize -= 20;
			GUI.Box(new Rect(xpos +2,ypos + height +2  ,width,height), "Click to Start",loadstyle);
			loadstyle.normal.textColor = regcol;

			GUI.Box(new Rect(xpos,ypos,width,height), "Heads will Roll",loadstyle);
			//loadstyle.fontSize -= 20;
			GUI.Box(new Rect(xpos,ypos + height  ,width,height), "Click to Start",loadstyle);
			
			if(GUI.Button(new Rect(0,0,Screen.width,Screen.height), "",loadstyle))
			{
				//ready to run
				state = GameStates.playing;//running = true;
				mySpawnerScript.started = true;
				myPlayerScript.started = true;
				startTime=Time.time;
				CountDownSeconds=60;	
				theScore = 0;
				joy.SetActive(true);
				//StartFade(.01f,0f,loadstyle);
				//StartSize(loadstyle.fontSize * 10,.001f,loadstyle);
				//StartFade(.0f,.03f,scorestyle);
				//StartFade(.0f,.03f,timerstyle);
			}	
		}
		else if (state == GameStates.GameOver)
		{
			
			ypos = Screen.height * .1f;

			Color regcol = timebonusstyle.normal.textColor;
			gameoverstyle.normal.textColor = Color.black;
			GUI.Box(new Rect(xpos +2 ,ypos +2 ,width,height), "Game Over",gameoverstyle);
			//	lodastyle.fontSize -= 20;
			GUI.Box(new Rect(xpos+2,ypos+ height+2,width,height), "Score: "+ theScore,gameoverstyle);	

			gameoverstyle.normal.textColor = regcol;


			GUI.Box(new Rect(xpos,ypos,width,height), "Game Over",gameoverstyle);
		//	lodastyle.fontSize -= 20;
			GUI.Box(new Rect(xpos,ypos+ height,width,height), "Score: "+ theScore,gameoverstyle);	
			
			//highscore..
			int prevhigh = 0;
			prevhigh = PlayerPrefs.GetInt("highscore");
			//Debug.Log ("prevhigh =  " + prevhigh );
			if(theScore > prevhigh)
			{
				prevhigh = theScore;
				PlayerPrefs.SetInt("highscore",theScore);
			}
			gameoverstyle.normal.textColor = Color.black;
			GUI.Box(new Rect(xpos + 2,2 + ypos+ height*2,width,height), "High Score: "+prevhigh ,gameoverstyle);
			
			GUI.Box(new Rect(xpos +2, 2 + ypos + (height *3) ,width,height), "tap screen to Start",gameoverstyle);

			gameoverstyle.normal.textColor = regcol;
			GUI.Box(new Rect(xpos,ypos+ height*2,width,height), "High Score: "+prevhigh ,gameoverstyle);
			
			GUI.Box(new Rect(xpos,ypos + (height *3) ,width,height), "tap screen to Start",gameoverstyle);
			
			if(GUI.Button(new Rect(0,0,Screen.width,Screen.height), "",gameoverstyle))
			{
				//ready to run
				state = GameStates.playing;//running = true;
				mySpawnerScript.started = true;
				myPlayerScript.started = true;
				startTime=Time.time;
				CountDownSeconds=60;
				theScore = 0;	
				nextbonus = 5;
				joy.SetActive(true);
			}
		}
    }
	
	
	//utility
	private IEnumerator Fade(float aFadeOutTime, float aFadeInTime, GUIStyle txt)
	{
		//float t = 0.0f;
		bool fadein = true;
		bool fadeout = true;
		if(aFadeOutTime > 0f)
		{
			while (fadeout)
			{
				Color tmp =txt.normal.textColor;
				//t = tmp.a;
			//	yield return new WaitForEndOfFrame();
				yield return new WaitForSeconds (aFadeOutTime);
			//	t = Mathf.Clamp01(t + Time.deltaTime / aFadeOutTime);
				
				tmp.a-=.01f;
				txt.normal.textColor = tmp;
				if(tmp.a <= 0)fadeout = false;
				//DrawQuad(aColor,t);
			}
		}
		else
		{
			if(txt.normal.textColor.a != 0)
			{
				Color tmp =txt.normal.textColor;
				tmp.a = 0f;
				txt.normal.textColor = tmp;
			}
		}
		if(aFadeInTime > 0f)
		{
			while (fadein)
			{
				Color tmp =txt.normal.textColor;
				yield return new WaitForSeconds (aFadeInTime);
				//t = tmp.a;
				//yield return new WaitForEndOfFrame();
				//t = Mathf.Clamp01(t - Time.deltaTime / aFadeInTime);
				tmp.a += .01f;
				txt.normal.textColor = tmp;
				if(tmp.a >= txtalpha)fadeout = false;
				
				//DrawQuad(aColor,t);
			}
		}
		m_Fading = false;
	}

	private void StartFade(float aFadeOutTime, float aFadeInTime,GUIStyle txt)
	{
		
		m_Fading = true;
		StartCoroutine(Fade(aFadeOutTime, aFadeInTime, txt));
	}
	
	
	
	private IEnumerator Size(int _size, float aTime,GUIStyle txt)
	{
		//float t = 0.0f;
		bool resizing = true;
		
		if(aTime > 0f)
		{
			while (resizing)
			{
				yield return new WaitForSeconds (aTime);
				
				if(txt.fontSize > _size)
					txt.fontSize-= 1;
				else
					txt.fontSize+= 1;
				if(txt.fontSize == _size)resizing = false;
			}
		}
		//m_Fading = false;
	}

	private void StartSize(int _size, float aTime,GUIStyle txt)
	{
		StartCoroutine(Size(_size ,aTime, txt));
	}
	
	
	private IEnumerator bumpSize(int _size,float aTime,GUIStyle txt)
	{
		//float t = 0.0f;
		bool resizing = true;
		int orgsize = txt.fontSize;
		bool fordir = true;
		
		if(aTime > 0f)
		{
			while (resizing)
			{
				yield return new WaitForSeconds (aTime);
				if(fordir)
				{
					txt.fontSize+= 1;
					if(txt.fontSize > orgsize + _size)fordir = false;
				}
				else
				{
					txt.fontSize-= 1;
					if(txt.fontSize == orgsize)resizing = false;
				}
			}
		}
		//m_Fading = false;
	}

	private void StartBumpSize(int _size,float aTime,GUIStyle txt)
	{
		StartCoroutine(bumpSize(_size,aTime, txt));
	}
	
	bool _showtimebonus = false;
	private IEnumerator timebonus()
	{
		_showtimebonus = true;
		yield return new WaitForSeconds (1);
		_showtimebonus = false;
		
	}

	private void showtimebonus()
	{
		StartCoroutine(timebonus());
	}
	
	private void playsfx()
	{
		if(!audio1.isPlaying)
		{ 
			int snd = Random.Range(0,sounds.Length);
		    audio1.clip=sounds[snd];
			if(audio1.clip != null)
			{
			    audio1.Play(); 
			}
        }
		else if(!audio2.isPlaying)
		{ 
			int snd = Random.Range(0,sounds.Length);
		    audio2.clip=sounds[snd];
			if(audio2.clip != null)
			{
			    audio2.Play(); 
			}
        }
	
	}
	private void playsfx(int index)
	{
	
		Debug.Log(index);
		if(!audio1.isPlaying)
		{ 
			Debug.Log("playing OnGUI src 1");
			audio1.clip=sounds[index];
			if(audio1.clip != null)
			{
				
			    audio1.Play(); 
			}
        }
		else if(!audio2.isPlaying)
		{ 
			Debug.Log("playing OnGUI src 2");
			audio2.clip=sounds[index];
			if(audio2.clip != null)
			{
			    audio2.Play(); 
			}
        }
		else if(!audio3.isPlaying)
		{ 
			Debug.Log("playing OnGUI src 3");
			audio3.clip=sounds[index];
			if(audio3.clip != null)
			{
			    audio3.Play(); 
			}
        }
		else if(!audio4.isPlaying)
		{ 
			Debug.Log("playing OnGUI src 4");
			audio4.clip=sounds[index];
			if(audio4.clip != null)
			{
			    audio4.Play(); 
			}
        }
		else
		{
			Debug.Log("no open src");
			
		}

	}
}
