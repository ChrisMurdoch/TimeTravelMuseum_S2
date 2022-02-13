using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishSpawnerScript : MonoBehaviour
{
    //public GameObject fish;
    Vector3 randPosition;
    public GameObject[] fishList;
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
        Instantiate(fishList[Random.Range(0, fishList.Length)], transform.position, Quaternion.Euler(0, 180, 0));
        yield return new WaitForSeconds(Random.Range(10, 25));
        StartCoroutine(timedSpawn());

    }
}

