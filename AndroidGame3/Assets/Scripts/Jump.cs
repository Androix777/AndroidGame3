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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "Ground")
        {
            if (timer > 0) { timer--; }
            else
            {
                timer = time;
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force);
            }
        }
    }

    

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "Ground")
        {
            if (timer > 0) { timer--; }
            else
            {
                timer = time;
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * force);
            }
        }
    }
}
