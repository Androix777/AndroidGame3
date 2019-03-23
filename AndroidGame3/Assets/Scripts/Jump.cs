using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public int time, timer;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        if (timer > 0) timer--;
        else
        {
            timer = time;
            gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up*force);
        }
    }
}
