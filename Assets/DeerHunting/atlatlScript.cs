using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atlatlScript : MonoBehaviour
{

    private bool loaded;
    private bool arcRendering;
    private GameObject currentSpear;
    private LaunchArcRenderer lr;

    // Start is called before the first frame update
    void Start()
    {
        loaded = false;
        arcRendering = false;
        lr = GetComponentInChildren<LaunchArcRenderer>();
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
            Debug.Log("transform.forward = (" + transform.forward.x + ", " + transform.forward.y + ", " + transform.forward.z + ")" );
            //if (hand position is behind neutral) {
                //BeginArcRender(neutralPos, controllerPos, angle);
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

    //call function to render line, give velocity and angle info based on controller pos
    private void BeginArcRender (Vector3 neutralPos, Vector3 controllerPos, float angle)
    {
        //calculate controllerPos distance from neutralPos
        //float backDist = controllerPos(forward) - neutralPos(forward) normalized?
        //float velocity = CalculateVelocity(backDist);
        // get angle atlatl is held at 

        //lr.RenderArc(velocity, angle, z)
        arcRendering = true;
    }

    private void EndArcRender() {
        lr.ClearArc();
        arcRendering = false;
    }

    private float CalculateVelocity(float backDist) 
    {
        float velocity = backDist * 3; //edit to better formula later
        return velocity;
    }
}
