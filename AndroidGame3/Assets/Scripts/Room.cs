using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int block;
    public int time;
    public Transform startPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetBlock()
    {
        return block;
    }
    public int GetTime()
    {
        return time;
    }
    public Vector3 StartPos()
    {
        return startPos.position;
    }

}
