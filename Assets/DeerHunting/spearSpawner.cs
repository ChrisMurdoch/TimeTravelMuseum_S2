using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearSpawner : MonoBehaviour
{
    public GameObject currentSpear;
    public GameObject spearPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpear == null) {
            SpawnNewSpear();
        }
    }

    void SpawnNewSpear() {
        currentSpear = Instantiate(spearPrefab, this.transform.position, this.transform.rotation);
    }
}
