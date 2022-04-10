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
    private Transform lastPlayerTransform;
    private GameObject currentHand;

    public Transform notchTransform;
    public GameObject player;
    public GameObject leftHand;
    public GameObject rightHand;
    public OVRInput.RawButton posButtonR;
    public OVRInput.RawButton throwButtonR;
    public OVRInput.RawButton posButtonL;
    public OVRInput.RawButton throwButtonL;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        loaded = false;
        arcRendering = false;
        lr = GetComponentInChildren<LaunchArcRenderer>();
        if(lr != null)
            Debug.Log("LR FOUND");
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpear == null)
            loaded = false;
        if(loaded) {
            
            currentSpear.transform.position = notchTransform.transform.position;
            currentSpear.transform.rotation = notchTransform.transform.rotation;
            
            //check if the atlatl is grabbed
            if (transform.parent != null) {
            

                //use left hand buttons
                if (transform.parent.name == "CustomHandLeft") {

                    currentHand = leftHand;

                    // positioning / render arc
                    if(OVRInput.GetDown(posButtonL)) {

                        RenderArc();     
                    }
                    //cancel if positioning button is pressed again
                    else if(arcRendering && OVRInput.GetDown(posButtonL)) {
                        EndArcRender();
                    }

                    //throw spear
                    if(arcRendering && OVRInput.GetDown(throwButtonL)) {

                        //check controller vel > some threshold
                        ThrowSpear();
                    }

                    

                //use right hand buttons
                } else if (transform.parent.name == "CustomHandRight") {

                    currentHand = rightHand;

                    if(OVRInput.GetDown(posButtonR)) {

                        RenderArc();     
                    }

                     else if(arcRendering && OVRInput.GetDown(posButtonR)) {
                        EndArcRender();
                    }

                    if(/*arcRendering &&*/ OVRInput.GetDown(throwButtonR)) {

                        //check controller vel > some threshold
                        ThrowSpear();
                    }

                   
                }

                if(arcRendering) {

                    if(player.transform.position != lastPlayerTransform.position || player.transform.rotation != lastPlayerTransform.rotation)
                        EndArcRender(); //automatically stop rendering when player moves
                    else RenderArc(); //update & render every frame
                }
            }
        }

        lastPlayerTransform = player.transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "spear" && !loaded) {
            currentSpear = other.gameObject;

            if(currentSpear.GetComponent<spearScript>().thrown == false) {
                currentSpear.transform.SetParent(this.transform);
                loaded = true;
                Debug.Log("LOADED SPEAR");
            }
        }
    }

    private void ThrowSpear() 
    {
        Debug.Log("THROW SPEAR");
        currentSpear.transform.SetParent(null);
        Rigidbody rb = currentSpear.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        Transform spearTip = currentSpear.transform.Find("spearTipPos");
        rb.AddForceAtPosition(-transform.right * 2500f, spearTip.position);
        currentSpear.GetComponent<spearScript>().thrown = true;
        loaded = false;
    }

    //call function to render line, give velocity and angle info based on controller pos
    private void RenderArc ()
    {
    //     Debug.Log("arc render called");
    //     Vector3 forwardAngles = currentSpear.transform.localEulerAngles;
    //     Debug.Log("ANGLES = " + forwardAngles);
    //     float angle = CalculateAngle(forwardAngles);
    //     Debug.Log("FORWARD ROT = " + angle);

    //     Vector3 controllerPos = new Vector3();

    //     //if this is the first time this is called for this throw
    //     if (!arcRendering) {
    //         neutralPos = this.transform.position;
    //         neutralPos = Vector3.Scale(neutralPos, Vector3.forward);
    //         controllerPos = neutralPos;

    //     } else {

    //         controllerPos = this.transform.position;
    //         controllerPos = Vector3.Scale(controllerPos, Vector3.forward);
    //     }

    //     //using formula for 3d distance
    //     //neutral = pt 2 bc should be in front of controller
    //     float backDist = Mathf.Sqrt(Mathf.Pow(neutralPos.x - controllerPos.x, 2.0f) + 
	// 		                Mathf.Pow(neutralPos.y - controllerPos.y, 2.0f) + 
	// 		                Mathf.Pow(neutralPos.z - controllerPos.z, 2.0f));
    //     if(backDist < 0.0f)
    //         backDist = 0.0f; //eliminate negative distance
    //     float velocity = CalculateVelocity(backDist);

    //     lr.MakeArcMesh(velocity, angle);
    //     arcRendering = true;
    }

    private void EndArcRender() {
        Debug.Log("arc end called");
        lr.ClearArc();
        arcRendering = false;
    }

    private float CalculateVelocity(float backDist) 
    {
        Debug.Log("BACKDIST = " + backDist);
        float velocity = Mathf.Pow(3f, 1.0f + backDist); //edit to better formula later
        return velocity;
    }

    private float CalculateAngle(Vector3 v) 
    {
        //magnitude of v * magnitude of (0, v.y, 0)
        float mag = v.y * Mathf.Sqrt(Mathf.Pow(v.x, 2) + Mathf.Pow(v.y, 2) + Mathf.Pow(v.z, 2));
        Debug.Log("MAG = " + mag);
        float dotProd = Mathf.Pow(v.y, 2); //dot product of vectors
        Debug.Log("DOTPROD = " + dotProd);
        float cos = dotProd / mag;
        Debug.Log("COS = " + cos);
        float angle = Mathf.Acos(cos); //angle between the vectors
        return angle; //returns "forward" rotation
    }

}
