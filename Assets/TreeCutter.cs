using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCutter : MonoBehaviour
{

    private bool canTrigger;

    // Start is called before the first frame update
    void Start()
    {
        canTrigger = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        
        GameObject hitObject = other.gameObject;
        Debug.Log("COLLISION with " + hitObject.name);

        if(hitObject.tag == "cuttableTree")
        {
            Debug.Log("tree hit");
            //reference damage script for this gameobject
            // add 1 hit of damage

            
            if(hitObject.GetComponent<treeHealthScript>() != null) {
                Debug.Log("found tree script");
                hitObject.GetComponent<treeHealthScript>().DamageTree();
            }

            canTrigger = false;
        }
    } 

    void OnTriggerExit(Collider other) {

        canTrigger = true;
    }
}
