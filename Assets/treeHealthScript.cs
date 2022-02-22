using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeHealthScript : MonoBehaviour
{

    public int maxHits;
    private int currentHits;


    // Update is called once per frame
    void Update()
    {
        if(currentHits >= maxHits)
        {
            Debug.Log("tree dead");
            Destroy(this.gameObject); //replace with falling tree animation
        }
    }


    // need to add function to carve wedge
    public void DamageTree() {
        currentHits++;
        Debug.Log(currentHits + " hits");
    }
}
