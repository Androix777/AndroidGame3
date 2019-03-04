using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < 0)
        {
            GeneratorBlock.Score++;
            Destroy(gameObject, 0);
        }


	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ExitArea")
        {
            GeneratorBlock.Score++;
            Destroy(gameObject, 0);
        }
    }
}
