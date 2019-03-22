using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public enum BlockType {white,red,black};
    public BlockType typeBlock = BlockType.red;
    public Material[] materials;
	// Use this for initialization
	void Start () {
        SelectColor();
    }
	
	// Update is called once per frame
	void Update () {
        

		if (transform.position.y < 0)
        {          
            Destroy(gameObject, 0);
        }


	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ExitArea" )
        {
            
            Destroy(gameObject, 0);
        }
    }

    private void OnDestroy()
    {
        GameController.Score++;
    }
    public void SelectColor()
    {
        if (typeBlock == BlockType.red)
        {
            gameObject.tag = "RedBlock";
            gameObject.GetComponent<MeshRenderer>().material = materials[1];

        }
        else if (typeBlock == BlockType.white)
        {
            gameObject.tag = "WhiteBlock";
            gameObject.GetComponent<MeshRenderer>().material = materials[0];
        }
        else if (typeBlock == BlockType.black)
        {
            gameObject.tag = "BlackBlock";
            gameObject.GetComponent<MeshRenderer>().material = materials[2];
        }
    }
}
