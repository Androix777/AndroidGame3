using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public enum BlockType {white,red };
    public BlockType typeBlock = BlockType.red;
    public Material[] materials;
	// Use this for initialization
	void Start () {
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
    }
	
	// Update is called once per frame
	void Update () {
        

		if (transform.position.y < 0)
        {
            GameController.Score++;
            Destroy(gameObject, 0);
        }


	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ExitArea" && typeBlock == BlockType.white)
        {
            GameController.Score++;
            Destroy(gameObject, 0);
        }
    }
}
