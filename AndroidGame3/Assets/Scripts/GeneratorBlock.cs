using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorBlock : MonoBehaviour {
    public GameObject Prefab;
	// Use this for initialization
	void Start () {
		
	}

    public void CreateCube()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject cube = Instantiate(Prefab);
            cube.transform.SetParent(transform);
            cube.transform.position = new Vector3(Random.Range(-9f, 9f), Random.Range(5, 10), Random.Range(-19f, 19f));
        }
    }
    // Update is called once per frame
    void Update () {
		
	}
}
