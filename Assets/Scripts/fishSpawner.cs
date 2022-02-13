using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishSpawner : MonoBehaviour
{
    public Rigidbody fish;
    Vector3 randPosition;
    void Start()
    {
        StartCoroutine(timedSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator timedSpawn()
    {
        Instantiate(fish, transform.position, Quaternion.Euler(0, Random.Range(135, 205), 0));
        yield return new WaitForSeconds(Random.Range(3, 10));
        StartCoroutine(timedSpawn());

    }
}

