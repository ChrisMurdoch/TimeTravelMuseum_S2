using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFallSound : MonoBehaviour
{
    public AudioSource fallSource;
    // Start is called before the first frame update
    void Start()
    {
        fallSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            fallSource.Play();
        }
    }
}
