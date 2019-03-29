using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]

public class PesronData
{   
    public int Room;
    public bool MusicActive;
}

public class GameController : MonoBehaviour
{
    const int MAXIMUMROOMLVL = 9;

    public PesronData characterData;
    public bool TestMode;
    int roomlvl = 0;
    public float second;
    public static bool MusicActive;
    public static int Score = 0;
    public Text textTime;
    public int time = 0;
    public int LvlScore;
    public int block;
    bool end = true;
    public GameObject Hero;
    public GameObject Room;
    public GameObject Music;
    public Text enterRoom;
    public GameObject menuTest;
    public GameObject menuUser;
    public Text menuUserLvl;
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Music.GetComponent<AudioSource>().mute = !MusicActive;

        if (time > 0)
        {
            time--;
            if (textTime != null) textTime.text = (int)(time / 100) + "." + time % 100;
        }
        
        

        if (time <= 0 && !end)
        {
            LvlScore = (int) (Score * 100f / block );
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
            Hero.GetComponent<Rigidbody>().velocity = Vector3.zero;
            Hero.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
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
        Score = 0;
        LvlScore = 0;
        menuTest.SetActive(false);
        Hero.GetComponent<MovementGG>().StartGameHero();
        
    }

    public void StartNextLvl()
    {
        string room = "";
        room = (roomlvl).ToString();
        CreateRoom(room);
        end = false;
        time = Room.GetComponent<Room>().GetTime();
        block = Room.GetComponent<Room>().GetBlock();
        Score = 0;
        LvlScore = 0;
        menuUser.SetActive(false);
        Hero.GetComponent<MovementGG>().StartGameHero();

    }

    public void DeadHero()
    {
        time = 0;
        end = true;      
        Hero.GetComponent<MovementGG>().StopGameHero();
        menuUserLvl.text = "Level " + roomlvl;
        StartCoroutine(OpenMenu());

    }

    public void EndTime()
    {
        if (LvlScore >=  95f)
        {
            if (roomlvl < MAXIMUMROOMLVL) roomlvl++;
        }
        else if (LvlScore  >=  85f)
        {
            if (roomlvl < MAXIMUMROOMLVL) roomlvl++;
        }
        else if (LvlScore >=  75f)
        {
            if (roomlvl < MAXIMUMROOMLVL) roomlvl++;
        }
        else
        {

        }
        end = true;
        menuUserLvl.text = "Level " + roomlvl + " "  + Score +" " + LvlScore ;
        Hero.GetComponent<MovementGG>().StopGameHero();
        StartCoroutine(OpenMenu());
    }

    IEnumerator OpenMenu()
    {
        yield return new WaitForSeconds(second);
        if (TestMode)
        {
            menuTest.SetActive(true);
        }
        else
        {
            menuUser.SetActive(true);
        }
        

    }


    static void SaveCharacter(PesronData data, int characterSlot)
    {
        PlayerPrefs.SetInt("LastRoom" + characterSlot, data.Room);
        PlayerPrefs.Save();
    }

    static PesronData LoadCharacter(int characterSlot)
    {
        PesronData loadedCharacter = new PesronData();
        loadedCharacter.Room = PlayerPrefs.GetInt("LastRoom" + characterSlot);

        return loadedCharacter;
    }
}

