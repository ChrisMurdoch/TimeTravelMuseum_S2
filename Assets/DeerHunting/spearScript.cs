using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearScript : MonoBehaviour
{

    public bool thrown;

    void Start() {
        thrown = false;
    }

    //called when spear tip hits something
    void OnTriggerEnter (Collider other) {

        Transform root = other.gameObject.transform.root;
        //don't count collisions with player or atlatl
        if (root.gameObject.tag != "Player" && other.gameObject.tag != "atlatl") {
            if (other.gameObject.tag == "spearTarget") { //make sure object is valid spear target
                other.gameObject.GetComponent<TargetScript>().KillTarget(); //kill the target
            }
           Destroy(this.gameObject); //destroy spear after it hits something
        }
    }
}
