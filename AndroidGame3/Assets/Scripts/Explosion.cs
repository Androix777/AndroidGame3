using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float range,force, modifier;
    public GameObject Particle;
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
        if (collision.transform.tag == "Player")
        {
            GameObject part = Instantiate(Particle, transform.position, Quaternion.Euler(-90, 0, 0)) as GameObject;
            part.transform.SetParent(GameObject.FindGameObjectWithTag("Room").transform);
            explose();
            Destroy(gameObject, 0);
        }
    }

    public void explose()
    {
        Collider[] obj = Physics.OverlapSphere(transform.position, range);
        foreach (Collider col in obj)
        {
            if (col.tag == "WhiteBlock" || col.tag == "RedBlock")
            {
                Rigidbody rb = col.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(force, transform.position, range, modifier);
            }
        }
    }
}
