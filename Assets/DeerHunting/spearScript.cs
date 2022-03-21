using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearScript : MonoBehaviour
{

    //called when spear tip hits something
    void OnTriggerEnter (Collider other) {

        //don't count collisions with player or atlatl
        if (other.gameObject.tag != "Player" && other.gameObject.tag != "atlatl") {
            if (other.gameObject.tag == "spearTarget") { //make sure object is valid spear target
                other.gameObject.GetComponent<TargetScript>().KillTarget(); //kill the target
            }
           // Destroy(this.gameObject); //destroy spear after it hits something
        }
    }
}
