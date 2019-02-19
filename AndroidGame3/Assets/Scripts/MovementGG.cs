using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementGG : MonoBehaviour {

	public float Force = 5, Speed = 10;
	float startMouseX = 0, startMouseY = 0, startGGX = 0, startGGY = 0, deltaX = 0, deltaY = 0, newPosX = 0, newPosY = 0;
	void Start () {
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0))
        {
			startMouseX = Input.mousePosition[0]/(Screen.height);
			startMouseY = Input.mousePosition[1]/(Screen.height);
			startGGX = gameObject.transform.position[0];
			startGGY = gameObject.transform.position[2];
		}
		
		if (Input.GetMouseButton(0))
		{
			deltaX = Input.mousePosition[0]/(Screen.height) - startMouseX;
			deltaY = Input.mousePosition[1]/(Screen.height) - startMouseY;
			newPosX = startGGX + deltaX * Speed;
			newPosY = startGGY + deltaY * Speed;

			gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

			gameObject.GetComponent<Rigidbody>().AddForce(
				(new Vector3(newPosX, 1, newPosY) - transform.position).normalized * 
				(Force * (new Vector3(newPosX, 1, newPosY) - transform.position).magnitude) ,
				ForceMode.VelocityChange);		
		}
	}

	void FixedUpdate () {
		
	}
}