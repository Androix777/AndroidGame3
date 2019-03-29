using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSound : MonoBehaviour
{
    public GameObject pic1, pic2;
    public void buttonTap()
    {
        GameController.MusicActive = !GameController.MusicActive;
        pic1.SetActive(!pic1.activeSelf);
        pic2.SetActive(!pic2.activeSelf);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
