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
    public static bool end = true;
    public GameObject Hero;
    public GameObject Room;
    public GameObject Music;
    public Text enterRoom;
    public GameObject menuTest;
    public GameObject menuUser;
    public Text menuUserLvl;
    public GameObject musicOn, musicOff;
    public GameObject progressBar,startPanel;
    public int Scorenumber = 0;
    public int ind;
    void Start()
    {   
        characterData = LoadCharacter(0);

        roomlvl = characterData.Room;

        if (characterData.MusicActive == 1)
        {
            MusicActive = true;
            musicOn.SetActive(true);
            musicOff.SetActive(false);
            Debug.Log("Error 2");
        }
        else
        {
            MusicActive = false;
            musicOn.SetActive(false);
            musicOff.SetActive(true);

        }
        if(!TestMode) LoadNextLvl();
        
    }

    void Update()
    {
        ind = indexBlock;

        Scorenumber = Score;

        Music.GetComponent<AudioSource>().mute = !MusicActive;
        if (MusicActive)
        {
            characterData.MusicActive = 1;
        }
        else
        {
            characterData.MusicActive = 0;
        }
        if (!end)
        {
            if (time <= 0)
            {
                EndTime();
            }
            else
            {
                time -= Time.deltaTime;
                LvlScore = (Score * 100f / block);
                if (textTime != null) { textTime.text = Math.Round(time, 2) + ""; }
                progressBar.transform.GetChild(0).GetComponent<Image>().fillAmount = LvlScore / 100;
                if (LvlScore >= 100)
                {
                    EndTime();
                }
            }
        }
        

    }

    public bool CreateRoom(int roomLvl)
    {
        GetComponent<GeneratorRoom>().GenerationRoom(roomLvl);
        Hero.transform.position = new Vector3(0,0.75f,-15);
        return true;
    }

    public void StartLvl()
    {
        indexBlock = 0;
        CreateRoom(Convert.ToInt32(enterRoom.text));

        end = false;
        time = 10000;
        block = indexBlock;
        Score = 0;
        LvlScore = 0;
        menuTest.SetActive(false);
        Hero.GetComponent<MovementGG>().StartGameHero();
        
    }

    public void LoadNextLvl()
    {
        indexBlock = 0;
       
        CreateRoom(roomlvl);
        
        time = 30;
        
        Score = 0;
        LvlScore = 0;
        
        StartCoroutine(StartHero());
    }

    public void StartNextLvl()
    {
        if (end)
        {
            Score = 0;
            textTime.gameObject.SetActive(true);
            progressBar.SetActive(true);
            startPanel.SetActive(false);
            end = false;
            block = indexBlock;
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
        menuUserLvl.text = "Level " + roomlvl + " " + (int)LvlScore + "%" + " " + (int)Score + " " +  (int)block + "";
        if (LvlScore >= 75)
        {
            roomlvl++;
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
    IEnumerator StartHero()
    {
        yield return new WaitForSeconds(0.1f);

        Hero.GetComponent<MovementGG>().StartGameHero();
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

    static int indexBlock = 0;
    public static int indexation()
    {
        indexBlock++;
        return indexBlock;
    }



}

