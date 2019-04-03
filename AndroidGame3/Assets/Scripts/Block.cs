using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {
    public GameObject Particle;
    public enum BlockType {white,red,black};
    public BlockType typeBlock = BlockType.red;
    public Material[] materials;
    public GameObject music;
	// Use this for initialization
	void Start () {
        gameObject.name = GameController.indexation()+"";
        SelectColor();
    }
	
	// Update is called once per frame
	void Update () {


	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "ExitArea")
        {
            if (!GameController.end)
            {
                GameObject part = Instantiate(Particle, transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
                part.name = gameObject.name + "0";
                Debug.Log(gameObject.name + "dead");
                part.GetComponent<ParticleSystemRenderer>().material = materials[(int)typeBlock];
                part.transform.SetParent(GameObject.FindGameObjectWithTag("Room").transform);
                if (GameController.MusicActive)
                {
                    GameObject musicObj = Instantiate(music, transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
                    part.transform.SetParent(GameObject.FindGameObjectWithTag("Room").transform);
                }

                Destroy(gameObject, 0);
            }
        }
    }

    private void OnDestroy()
    {
        if (!GameController.end)
        {
            GameController.Score++;
        }
        


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
