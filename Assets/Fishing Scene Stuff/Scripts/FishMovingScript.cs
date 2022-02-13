using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovingScript : MonoBehaviour
{
    public float speed;
    Vector3 randPosition;
    Vector3 myVector;
    public Rigidbody rb;
    public Vector3 movement;
    public Animator an;
    public AudioSource audio;
    public AudioClip splashClip;

    public GameObject splashEffect;

    public bool inWater;

    public float fishSpeed;


    void Start()
    {
        inWater = false;
        rb = this.GetComponent<Rigidbody>();
        speed = Random.Range(32f,50f);
        audio = this.GetComponent<AudioSource>();

        an = this.GetComponent<Animator>();
        an.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        fishSpeed = rb.velocity.magnitude;
        if(fishSpeed < 0.5 && inWater == true)
        {
            StartCoroutine(killStuck());
        }

        
        //movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        movement = Vector3.back;
        
        if(inWater == false)
        {
            audio.enabled = false;
        }
        else
        {
            audio.enabled = true;
        }
        //transform.position += transform.forward * speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        move(movement);

        if (inWater == false && rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (15f - 1) * Time.deltaTime;
        }
    }

    void move(Vector3 direction)
    {
        if(inWater == true) {
            rb.AddForce(direction * speed);
            rb.rotation = Quaternion.Euler(0, 180, 0);
            
        }
        
    }

    IEnumerator killStuck()
    {
        yield return new WaitForSeconds(10);
        if (fishSpeed < 0.5 && inWater == true)
        {
            Destroy(this.gameObject);
        }
        yield break;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "endWall")
        {
            Destroy(this.gameObject);
        }
        if (other.tag == "water")
        {
            inWater = true;
            rb.useGravity = false;
            an.enabled = true;
            GameObject instantiatedObj = (GameObject)Instantiate(splashEffect, transform.position, Quaternion.Euler(0, 160, 0));
            Destroy(instantiatedObj, 2f);
            //audio.PlayOneShot(splashClip);

        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "water")
        {
            rb.useGravity = true;
            inWater = false;
            an.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "water")
        {
            rb.useGravity = false;
            inWater = true;
            an.enabled = true;
        }
    }
}
