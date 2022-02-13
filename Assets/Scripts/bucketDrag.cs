using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bucketDrag : MonoBehaviour
{
    private Vector3 mOffset;
    private float mZCoord;
    int score;
    public Text scoreText;
    void Start()
    {
        score = 0;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mOffset = gameObject.transform.position - GetMousePos();
    }

    private Vector3 GetMousePos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    private void OnMouseDrag()
    {
        transform.position = GetMousePos() + mOffset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(this.gameObject.tag == "bucketDeposit")
        {
            if (other.tag == "fish")
            {
                Destroy(other.gameObject);
                score++;
                print(score);
                scoreText.text = "Score: " + score;
            }
        }
        
    }
}
