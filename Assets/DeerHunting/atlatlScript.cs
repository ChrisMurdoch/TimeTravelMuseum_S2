using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atlatlScript : MonoBehaviour
{
    [SerializeField] private float velFactor; //number to multiply calculated velocity by
    private bool loaded; //whether the atlatl has been loaded with a spear
    private Vector3 currentVelocity; 
    private GameObject currentSpear; //spear that is currently loaded (null if not loaded)
    private GameObject currentHand; //which hand is holding the atlatl (customHandLeft or customHandRight)
    private Vector3 oldPosition; //holds the atlatl's last position for calculating velocity

    public Transform trackingSpace; //get "trackingspace" to make sure player's rotation is applied to thrown spear's velocity
    public GameObject leftHand; //player's left hand (customHandLeft)
    public GameObject rightHand; //player's right hand (customHandRight)
    public Transform notchTransform; //transform of the end of the atlatl, where spear should be placed
    public Transform spearTip;
    public OVRInput.RawButton throwButtonR; //which button throws spear if holding with right hand
    public OVRInput.RawButton throwButtonL; //which button throws spear if holding with left hand

    // Start is called before the first frame update
    void Start()
    {
        loaded = false; //start with unloaded atlatl
        oldPosition = transform.position; //initialize oldTPosition to first position
        currentVelocity = Vector3.zero; //start with velocity at zero
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = CalculateVelocity(); //calculate velocity every frame (to make sure oldPosition is always updated)

        if(currentSpear == null)
            loaded = false; //atlatl becomes unloaded when spear despawns

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
        currentSpear.transform.SetParent(null); //remove spear from atlatl
        Rigidbody srb = currentSpear.GetComponent<Rigidbody>();
        srb.isKinematic = false; //make sure spear isn't kinematic

        float velocityValue = (Mathf.Abs(currentVelocity.x) + Mathf.Abs(currentVelocity.y) + Mathf.Abs(currentVelocity.z)) / 3.0f;
        //add force of the current velocity times your scaling factor
        srb.AddForceAtPosition(velocityValue * trackingSpace.transform.forward, spearTip.position); //apply velocity from controller movement

        //Transform spearTip = currentSpear.transform.Find("spearTipPos");
        //rb.AddForceAtPosition(-transform.right * 2500f, spearTip.position);
        currentSpear.GetComponent<spearScript>().thrown = true;
        loaded = false; //spear unloaded
    }

   

    private Vector3 CalculateVelocity() 
    {
        Vector3 newPosition = transform.position; 
        Vector3 difference = newPosition - oldPosition; //find how far atlatl has moved between frames
        Vector3 velocity = (difference / Time.deltaTime); //divide by time to get velocity
        oldPosition = newPosition; //update oldPosition
        return velocity * velFactor; //return velocity * a factor to scale by
    }
}
