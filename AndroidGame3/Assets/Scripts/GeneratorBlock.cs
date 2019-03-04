using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GeneratorBlock : MonoBehaviour {
    public GameObject Prefab;
    public static int Score = 0;
    public Text text ;
    public int time = 900;
    public int LvlScore;
    bool end = false;

	void Start () {
		
	}

    public void CreateCube()
    {
        for (int i = 0; i < 100; i++)
        {
            GameObject cube = Instantiate(Prefab,transform.position,transform.rotation);
            cube.transform.position = new Vector3(Random.Range(-9f, 9f), Random.Range(5, 10), Random.Range(-19f, 19f));
            cube.SetActive(true);
        }
        
    }
    // Update is called once per frame
    void Update () {
        if (time > 0)
        {
            time--;
        }        
        text.text = (int)(time / 100)+"." + time % 100;

        if (time <= 0 && !end)
        {
            if (Score >= (int)LvlScore * 0.95f)
            {

            }
            else if (Score >= (int)LvlScore * 0.85f)
            {

            }
            else if (Score >= (int)LvlScore * 0.75f)
            {

            }
            end = true;
        }
	}



}
