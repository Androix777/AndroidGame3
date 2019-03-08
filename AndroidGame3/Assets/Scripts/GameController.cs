using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{
    public static int Score = 0;
    public Text text;
    public int time = 0;
    public int LvlScore;
    public int block;
    bool end = true;
    public GameObject Hero;
    public GameObject Room;

    public Text enterRoom;
    public GameObject menu;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time--;
            if (text != null) text.text = (int)(time / 100) + "." + time % 100;
        }
        
        

        if (time <= 0 && !end)
        {
            LvlScore = (int) (block / (Score + 1) * 100);
            EndTime();
            
        }
    }

    public bool CreateRoom(string roomName)
    {
        
        string path = "Rooms/" + roomName;
        if (Room != null)
        {
            GameObject.Destroy(Room, 0);
        }
        if (Resources.Load(path) != null)
        {
            Room = Instantiate(Resources.Load(path), Vector3.zero, Quaternion.Euler(Vector3.zero)) as GameObject;
            Hero.transform.position = Room.GetComponent<Room>().StartPos();
            return true;
        }
        else return false;
    }

    public void StartLvl()
    {
        string room ="";
        room = enterRoom.text;
        CreateRoom(room);
        end = false;
        time = Room.GetComponent<Room>().GetTime();
        block = Room.GetComponent<Room>().GetBlock();
        menu.SetActive(false);
        Hero.GetComponent<MovementGG>().stopGame = false;
        
    }

    public void DeadHero()
    {
        time = 0;
        end = true;
        menu.SetActive(true);
        Hero.GetComponent<MovementGG>().stopGame = true;
    }

    public void EndTime()
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
        else
        {

        }
        end = true;
        menu.SetActive(true);
        Hero.GetComponent<MovementGG>().stopGame = true;
    }
}
