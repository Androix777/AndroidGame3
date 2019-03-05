using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static int Score = 0;
    public Text text;
    public int time = 900;
    public int LvlScore;
    bool end = false;
    public GameObject Hero;

    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time--;
        }
        
        if (text != null) text.text = (int)(time / 100) + "." + time % 100;

        if (time <= 0 && !end)
        {
            Hero.GetComponent<MovementGG>().stopGame = true;
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
