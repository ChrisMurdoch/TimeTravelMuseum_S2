using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeChopSound : MonoBehaviour
{
    public AudioSource chopSource;
    // Start is called before the first frame update
    void Start()
    {
        chopSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    { 
        
        if (collision.gameObject.tag == "Tree")
        {
           chopSource.Play(); 
        }
    }
}
