using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementGG : MonoBehaviour {
    public bool stopGame = false;
	public float Force = 5, Speed = 10;
	float startMouseX = 0, startMouseY = 0, startGGX = 0, startGGY = 0, deltaX = 0, deltaY = 0, newPosX = 0, newPosY = 0;
	void Start () {
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "RedBlock")
        {

            Destroy(gameObject, 0);
        }
    }

    void Update () {


        if (Input.touchCount > 0 && !stopGame)
        {

		    if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
			    startMouseX = Input.GetTouch(0).position[0]/(Screen.height);
			    startMouseY = Input.GetTouch(0).position[1]/(Screen.height);
			    startGGX = gameObject.transform.position[0];
			    startGGY = gameObject.transform.position[2];
		    }
		
		    if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
		    {
			    deltaX = Input.GetTouch(0).position[0]/(Screen.height) - startMouseX;
			    deltaY = Input.GetTouch(0).position[1]/(Screen.height) - startMouseY;
			    newPosX = startGGX + deltaX * Speed;
			    newPosY = startGGY + deltaY * Speed;

			    gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

			    gameObject.GetComponent<Rigidbody>().AddForce(
				    (new Vector3(newPosX, 1, newPosY) - transform.position).normalized * 
				    (Force * (new Vector3(newPosX, 1, newPosY) - transform.position).magnitude) ,
				    ForceMode.VelocityChange);		
		    }

            if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.touchCount > 0)
            {
                startMouseX = Input.GetTouch(1).position[0] / (Screen.height);
                startMouseY = Input.GetTouch(1).position[1] / (Screen.height);
                startGGX = gameObject.transform.position[0];
                startGGY = gameObject.transform.position[2];
            }
	    }
    }

    void FixedUpdate () {
		
	}
}