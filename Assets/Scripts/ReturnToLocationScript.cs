using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToLocationScript : MonoBehaviour
{
    public Transform reset;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(reset.position, transform.position) >= 260f)
        {
            transform.position = reset.position;
            rb.velocity = Vector3.zero;
        }
    }
}
