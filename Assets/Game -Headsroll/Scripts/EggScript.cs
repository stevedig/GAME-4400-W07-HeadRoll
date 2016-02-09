using UnityEngine;
using System.Collections;

public class EggScript : MonoBehaviour {
	 //public Camera lookattarget;
    void Awake()
    {
        //rigidbody.AddForce(new Vector3(0, -100, 0), ForceMode.Force);
    }

    //Update is called by Unity every frame
	void Update () {
        float fallSpeed = 2 * Time.deltaTime;
        transform.position -= new Vector3(0, fallSpeed, 0);
		
		//Vector3 targetDir = Camera.current.transform.position - transform.position;
        //Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, fallSpeed, 0.0F);
        // Debug.DrawRay(transform.position, newDir, Color.red);
        //transform.rotation = Quaternion.LookRotation(newDir);
		//Camera.current.transform.Rotate(0f,20f,0f
	//	transform.RotateAround(Vector3.zero, Vector3.right, 20 * Time.deltaTime);
		//Vector3 targetDir = new Vector3(0f,180f,0f);
		transform.Rotate(-90f * Time.deltaTime,90f * Time.deltaTime,0f);
        if (transform.position.y < -1 || transform.position.y >= 20)
        {
            //Destroy this gameobject (and all attached components)
            Destroy(gameObject);
        }
		  
		
		
	}
}
