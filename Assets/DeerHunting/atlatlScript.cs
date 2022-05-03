using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atlatlScript : MonoBehaviour
{
    public float testVelocity; //value used for testing throws
    [SerializeField] private float forwardVelocityFactor; //number to multiply calculated velocity by
    [SerializeField] private float upwardVelocityFactor;
    private bool loaded; //whether the atlatl has been loaded with a spear
    private GameObject currentSpear; //spear that is currently loaded (null if not loaded)
    private GameObject currentHand; //which hand is holding the atlatl (customHandLeft or customHandRight)
    private GameObject prevHand; //which hand was holding the atlatl last frame
    private Vector3 COMRight; //right controller's center of mass
    private Vector3 COMLeft; //left controller's center of mass

    public Transform trackingSpace; //get "trackingspace" to make sure player's rotation is applied to thrown spear's velocity
    public GameObject leftHand; //player's left hand (customHandLeft)
    public GameObject rightHand; //player's right hand (customHandRight)
    public Transform notchTransform; //transform of the end of the atlatl, where spear should be placed
    public Transform spearTip;
    public OVRInput.RawButton throwButtonR; //which button throws spear if holding with right hand
    public OVRInput.RawButton throwButtonL; //which button throws spear if holding with left hand

    //velocity storage parameters
    private Vector3[] velocityFrames; //holds hand's linear velocity from last few frames
    private Vector3[] angularFrames; //holds hand's angular velocity from last few frames
    [SerializeField] private int framesToStore; //how many frames of velocity to store
    private int currentVelocityStep; //which velocity frame we are storing


    // Start is called before the first frame update
    void Start()
    {
        loaded = false; //start with unloaded atlatl

        //get center of mass for each controller
        COMRight = rightHand.GetComponent<Rigidbody>().centerOfMass;
        COMLeft = leftHand.GetComponent<Rigidbody>().centerOfMass;

        //set up velocity frame storage
        velocityFrames = new Vector3[framesToStore];
        angularFrames = new Vector3[framesToStore];
        currentVelocityStep = 0;
    }

    // Update is called once per frame
    void Update()
    {
        prevHand = currentHand; //store hand previously used
        //currentVelocity = CalculateVelocity(); //calculate velocity every frame (to make sure oldPosition is always updated)

        if(currentSpear == null)
            loaded = false; //atlatl becomes unloaded when spear despawns
    }

    void FixedUpdate()
    {
        //reset velocity frames when you switch hands
        if(prevHand != currentHand)
            ResetVelocityFrames();

        VelocityUpdate(); //add to velocity frames

        if(loaded) {
            
            //keep spear on atlatl
            currentSpear.transform.position = notchTransform.transform.position;
            currentSpear.transform.rotation = notchTransform.transform.rotation;
            
            //check if the atlatl is grabbed
            if (transform.parent != null) {
            

                //use left hand buttons
                if (transform.parent.name == "CustomHandLeft") {

                    currentHand = leftHand;

                    //throw spear
                    if(OVRInput.GetDown(throwButtonL)) {

                        ThrowSpear();
                    }

                    

                //use right hand buttons
                } else if (transform.parent.name == "CustomHandRight") {

                    currentHand = rightHand;

                    if(OVRInput.GetDown(throwButtonR)) {

                        ThrowSpear();
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "spear" && !loaded) {
            currentSpear = other.gameObject;
            spearTip = currentSpear.transform.Find("spearTipPos").transform;

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

        //set up spear for throwing
        currentSpear.transform.SetParent(null); //remove spear from atlatl
        Rigidbody srb = currentSpear.GetComponent<Rigidbody>();
        srb.isKinematic = false; //make sure spear isn't kinematic

        Vector3 linearVelocity;
        Vector3 angularVelocity;

        if(currentHand == rightHand) {
            linearVelocity = trackingSpace.transform.TransformVector(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
            angularVelocity = trackingSpace.transform.TransformVector(OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch));
        } else {
            linearVelocity = trackingSpace.transform.TransformVector(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch));
            angularVelocity = trackingSpace.transform.TransformVector(OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.LTouch));
        }
        Vector3 velocityCross = Vector3.Cross(angularVelocity, currentSpear.transform.position - currentHand.GetComponent<Rigidbody>().centerOfMass);
        Vector3 baseVelocity = linearVelocity + velocityCross;
        srb.velocity = baseVelocity;
        srb.angularVelocity = angularVelocity;
        AverageVelocityFrames(srb);
        ResetVelocityFrames();

        srb.AddForceAtPosition((-currentSpear.transform.right * forwardVelocityFactor) + (currentSpear.transform.up * upwardVelocityFactor), spearTip.position);

    //     float velocityValue = (Mathf.Abs(currentVelocity.x) + Mathf.Abs(currentVelocity.y) + Mathf.Abs(currentVelocity.z)) / 3.0f;
    //     //add force of the current velocity times your scaling factor
    //     //srb.AddForceAtPosition(velocityValue * trackingSpace.transform.forward, spearTip.position); //apply velocity from controller movement
    //     srb.AddForceAtPosition(testVelocity * trackingSpace.transform.forward, spearTip.position); //apply velocity when testing
    //     //Transform spearTip = currentSpear.transform.Find("spearTipPos");
    //     //rb.AddForceAtPosition(-transform.right * 2500f, spearTip.position);
        currentSpear.GetComponent<spearScript>().thrown = true;
        loaded = false; //spear unloaded
    }

    private void VelocityUpdate()
    {
        if(currentHand != null && velocityFrames != null) {
        
            currentVelocityStep++; //move to next storage frame

            if(currentVelocityStep >= framesToStore)
                currentVelocityStep = 0; //reset if all frames full
            
            if(currentHand == rightHand) {
                velocityFrames[currentVelocityStep] = trackingSpace.transform.TransformVector(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
                angularFrames[currentVelocityStep] = trackingSpace.transform.TransformVector(OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.RTouch));
            } else {
                velocityFrames[currentVelocityStep] = trackingSpace.transform.TransformVector(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch));
                angularFrames[currentVelocityStep] = trackingSpace.transform.TransformVector(OVRInput.GetLocalControllerAngularVelocity(OVRInput.Controller.LTouch));
            }
        }
    }

    //clear velocity frame data & reset current step
    private void ResetVelocityFrames()
    {
        for(int i = 0; i < framesToStore; i++)
        {
            velocityFrames[i] = Vector3.zero;
            angularFrames[i] = Vector3.zero;
            currentVelocityStep = 0;
        }
    }

    
    private void AverageVelocityFrames(Rigidbody rb)
    {
        if(velocityFrames != null)
        {
            //get averages for linear and angular velocities
            Vector3 avgVelocity = GetVectorAverage(velocityFrames);
            Vector3 avgAngular = GetVectorAverage(angularFrames);
            
            //set rigidbody values to averages
            if(avgVelocity != null)
                rb.velocity = avgVelocity;
            if(avgAngular != null)
                rb.angularVelocity = avgAngular;
        }
    }

    private Vector3 GetVectorAverage(Vector3[] vectors)
    {
        Vector3 total = new Vector3();
        float numVectors = (float)vectors.Length;
        int temp = 1;
        foreach(Vector3 v in vectors)
        {
            Debug.Log("VelFrame " + temp + " = " + v);
            total += v;
            temp++;
        }

        return new Vector3(total.x / numVectors, total.y / numVectors, total.z / numVectors);
    }
}
