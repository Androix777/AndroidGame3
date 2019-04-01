using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]

public class PesronData
{   
    public int Room = 0;
    public int MusicActive = 0;
}



public class GameController : MonoBehaviour
{
    const int MAXIMUMROOMLVL = 9;

    public PesronData characterData = new PesronData();
    public bool TestMode;
    int roomlvl = 0;
    public float second;
    public static bool MusicActive;
    public static int Score = 0;
    public Text textTime;
    public float time = 0;
    public float LvlScore = 0;
    public int block;
    bool end = true;
    public GameObject Hero;
    public GameObject Room;
    public GameObject Music;
    public Text enterRoom;
    public GameObject menuTest;
    public GameObject menuUser;
    public Text menuUserLvl;
    public GameObject musicOn, musicOff;
    public GameObject progressBar,startPanel;
    void Start()
    {
        
        characterData = LoadCharacter(0);
        roomlvl = characterData.Room;
        if (characterData.MusicActive == 1)
        {
            MusicActive = true;
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
        else
        {
            MusicActive = false;
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        if(!TestMode) LoadNextLvl();

    }
    // Update is called once per frame
    void Update()
    {
        

        Music.GetComponent<AudioSource>().mute = !MusicActive;
        if (MusicActive)
        {
            characterData.MusicActive = 1;
        }
        else
        {
            characterData.MusicActive = 0;
        }

        if (time <= 0)
        {
            EndTime();
        }
        else
        {
            time -= Time.deltaTime;
            if (!end)
            {
                LvlScore = (Score * 100f / block);
                if (LvlScore > 100) { LvlScore = 100; }
                if (textTime != null) { textTime.text = Math.Round(time, 2) + ""; }
                progressBar.transform.GetChild(0).GetComponent<Image>().fillAmount = LvlScore / 100;

            }
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

    public void LoadNextLvl()
    {
        string room = "";
        room = (roomlvl).ToString();
        CreateRoom(room);
        
        time = Room.GetComponent<Room>().GetTime();
        block = Room.GetComponent<Room>().GetBlock();
        Score = 0;
        LvlScore = 0;
        Hero.GetComponent<MovementGG>().StartGameHero();
    }

    public void StartNextLvl()
    {
        if (end)
        {
            textTime.gameObject.SetActive(true);
            progressBar.SetActive(true);
            startPanel.SetActive(false);
            end = false;

        }
        
    }

    public void DeadHero()
    {
        time = 0;
        end = true;      
        Hero.GetComponent<MovementGG>().StopGameHero();
        menuUserLvl.text = "Level " + roomlvl;
        StartCoroutine(OpenMenu());
        progressBar.SetActive(false);
        textTime.gameObject.SetActive(false);
        SaveCharacter(characterData, 0);
    }

    public void EndTime()
    {
        menuUserLvl.text = "Level " + roomlvl + " " + (int)LvlScore + "%";
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
        progressBar.SetActive(false);
        textTime.gameObject.SetActive(false);       
        characterData.Room = roomlvl;
        SaveCharacter(characterData, 0);
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
        PlayerPrefs.SetInt("Music" + characterSlot, data.MusicActive);
        PlayerPrefs.Save();
    }

    static PesronData LoadCharacter(int characterSlot)
    {
        PesronData loadedCharacter = new PesronData();
        loadedCharacter.Room = PlayerPrefs.GetInt("LastRoom" + characterSlot);
        loadedCharacter.MusicActive = PlayerPrefs.GetInt("Music" + characterSlot);
        return loadedCharacter;
    }
}

