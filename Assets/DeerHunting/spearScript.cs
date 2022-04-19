using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearScript : MonoBehaviour
{
    public bool thrown;

    private spearSpawner spawn;
    void Start() {
        thrown = false;
        spawn = GameObject.Find("spearSpawn").GetComponent<spearSpawner>();
    }

    void LateUpdate()
    {
        if (thrown)
            StartCoroutine(SelfDestruct()); //start timer to destroy spear after it has been thrown
    }

    //called when spear tip hits something
    void OnTriggerEnter (Collider other) {

        Transform root = other.gameObject.transform.root;
        //don't count collisions with player or atlatl
        if (other.gameObject.tag != "Player" && root.gameObject.tag != "Player" && other.gameObject.tag != "atlatl") {
            if (other.gameObject.tag == "spearTarget") { //make sure object is valid spear target
                other.gameObject.GetComponent<TargetScript>().KillTarget(); //kill the target
                spawn.gameOver = true;
            }
           Destroy(this.gameObject); //destroy spear after it hits something
        }
    }

    IEnumerator SelfDestruct() //destroy game object after amount of time 
    {
        yield return new WaitForSeconds(4f);
        Destroy(this.gameObject);
    }
}
