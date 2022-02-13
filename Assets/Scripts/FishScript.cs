using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    float speed;
    Vector3 randPosition;
    Vector3 myVector;
    
    void Start()
    {

        speed = Random.Range(1.2f,2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "endWall")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "water")
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
