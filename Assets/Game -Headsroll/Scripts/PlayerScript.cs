using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    private bool _started = false;
   
	private float swipeSpeed = 0.05F;
	private float inputX;
	//private float inputY;
	
	private Vector2 leftFingerPos = Vector2.zero;
	private Vector2 leftFingerLastPos = Vector2.zero;
	private Vector2 leftFingerMovedBy = Vector2.zero;
	public float slideMagnitudeX  = 0.0f;
	public float slideMagnitudeY  = 0.0f;

	
	
	public bool started
    {
        get
		{ 
			return _started;
		}
        set
		{
			_started = value;
			
		}
    }
	
	void Update() 
	{
	
		if (Input.touchCount == 1) 
		{
			Touch touch = Input.GetTouch(0);
			if (touch.phase == TouchPhase.Began)
	    	{
	            leftFingerPos = Vector2.zero;
	            leftFingerLastPos = Vector2.zero;
	            leftFingerMovedBy = Vector2.zero;
				slideMagnitudeX = 0;
				slideMagnitudeY = 0;
				// record start position
				leftFingerPos = touch.position;
			}
			else if (touch.phase == TouchPhase.Moved)
			{
			    leftFingerMovedBy = touch.position - leftFingerPos; // or Touch.deltaPosition : Vector2 
			                                                        // The position delta since last change.
	
	            leftFingerLastPos = leftFingerPos;
			    leftFingerPos = touch.position;
			    // slide horz
			    slideMagnitudeX = leftFingerMovedBy.x / Screen.width;
				// slide vert
				slideMagnitudeY = leftFingerMovedBy.y / Screen.height;
			}
			else if (touch.phase == TouchPhase.Stationary)
			{
				leftFingerLastPos = leftFingerPos;
				leftFingerPos = touch.position;
				slideMagnitudeX = 0.0f;
				slideMagnitudeY = 0.0f;
			}
			else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
			{
	
	            slideMagnitudeX = 0.0f;
			    slideMagnitudeY = 0.0f;
			}
		}
	
	}
		
	
	void FixedUpdate ()
	{
		if(_started)
		{
			//pc
	        //These two lines are all there is to the actual movement..
	        float moveInput = Input.GetAxis("Horizontal") * Time.deltaTime * 3; 
	        transform.position += new Vector3(moveInput, 0, 0);
	
			//ipod
		//	if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
		//	{
    	//	//	touchDeltaPosition = ;						 
    	//		inputX += Input.GetTouch(0).deltaPosition.x * swipeSpeed;
    	//		//inputY += touchDeltaPosition.y * swipeSpeed;
    	//		//Debug.Log("X, Y: " + touchDeltaPosition.x	+ ", " + touchDeltaPosition.y);
		//		transform.position += new Vector3(inputX, 0, 0);
		//	}	
			transform.position += new Vector3(slideMagnitudeX, 0, 0);
			
	        //Restrict movement between two values
	        if (transform.position.x <= -2.5f || transform.position.x >= 2.5f)
	        {
	            float xPos = Mathf.Clamp(transform.position.x, -2.5f, 2.5f); //Clamp between min -2.5 and max 2.5
	            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
	        }
		}
	}

}
