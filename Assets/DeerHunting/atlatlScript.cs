using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atlatlScript : MonoBehaviour
{
    //controller must be moving at least this velocity to throw when button is pressed
    [SerializeField]
    private float minThrowVelocity; 
    private bool loaded;
    private bool arcRendering;

    private Vector3 neutralPos;
    
    private GameObject currentSpear;
    private LaunchArcRenderer lr;

    public OVRInput.RawButton posButtonR;
    public OVRInput.RawButton throwButtonR;
    public OVRInput.RawButton posButtonL;
    public OVRInput.RawButton throwButtonL;

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

            currentSpear.transform.position = this.transform.position;
            currentSpear.transform.rotation = this.transform.rotation;
            
            //check if the atlatl is grabbed
            if (transform.parent != null) {
            

                //use left hand buttons
                if (transform.parent.name == "CustomHandLeft") {

                    // positioning / render arc
                    if(OVRInput.GetDown(posButtonL)) {

                        RenderArc();     
                    }
                    //cancel if positioning button is pressed again
                    else if(arcRendering && OVRInput.GetDown(posButtonR)) {
                        EndArcRender();
                    }

                    //throw spear
                    if(arcRendering && OVRInput.GetDown(throwButtonL)) {

                        //check controller vel > some threshold
                        //launch spear
                    }

                    

                //use right hand buttons
                } else if (transform.parent.name == "CustomHandRight") {

                    if(OVRInput.GetDown(posButtonR)) {

                        RenderArc();     
                    }

                     else if(arcRendering && OVRInput.GetDown(posButtonR)) {
                        EndArcRender();
                    }

                    if(arcRendering && OVRInput.GetDown(throwButtonR)) {

                        //check controller vel > some threshold
                        //launch spear
                    }

                   
                }
            }
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
    private void RenderArc ()
    {
        Debug.Log("arc render called");
        float angle = this.transform.rotation.x;

        Vector3 controllerPos = new Vector3();

        //if this is the first time this is called for this throw
        if (!arcRendering) {

            neutralPos = this.transform.position;
            controllerPos = neutralPos;

        } else {

            controllerPos = this.transform.position;
        }

        float backDist = neutralPos.z - controllerPos.z;
        float velocity = CalculateVelocity(backDist);

        lr.MakeArcMesh(velocity, angle);
        arcRendering = true;
    }

    private void EndArcRender() {
        Debug.Log("arc end called");
        lr.ClearArc();
        arcRendering = false;
    }

    private float CalculateVelocity(float backDist) 
    {
        float velocity = backDist * 3; //edit to better formula later
        return velocity;
    }
}
