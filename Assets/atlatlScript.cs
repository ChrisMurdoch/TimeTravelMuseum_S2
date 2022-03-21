using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atlatlScript : MonoBehaviour
{

    private bool loaded;
    private GameObject currentSpear;

    // Start is called before the first frame update
    void Start()
    {
        loaded = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(loaded) {

            if(OVRInput.Get(OVRInput.Button.One)) 
            {
                ThrowSpear();
            }

            currentSpear.transform.position = this.transform.position;
            currentSpear.transform.rotation = this.transform.rotation;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "spear" && !loaded) {
            currentSpear = other.gameObject;
            currentSpear.transform.SetParent(this.transform);
            loaded = true;
            Debug.Log("LOADED SPEAR");
        }
    }

    private void ThrowSpear() 
    {
        currentSpear.transform.SetParent(null);
        // add force to spear to match reticle/arc
        loaded = false;
    }
}
