using UnityEngine;
using System.Collections;

public class RunToRandomPoint : MonoBehaviour
{
    Vector3 targetPos;
    float MovementSpeed = 5.0f;
    float CloseEnough = 1.0f;

    IEnumerator Start()
    {
        targetPos = transform.position;
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            targetPos = new Vector3(transform.position.x + (Random.Range(-30, 30)), transform.position.y, transform.position.z + (Random.Range(-15, 15)));
            while ((transform.position - targetPos).magnitude > CloseEnough)
            {
                yield return new WaitForSeconds(0.0f);
            }
        }
    }

    /*
        void OnCollisionEnter(Collision collision)
        {
            Debug.Log(" " + collision.gameObject.name);
        }
        */

    void Update()
    {

        if ((transform.position - targetPos).magnitude > CloseEnough)
        {
            transform.LookAt(targetPos);
            transform.position += (targetPos - transform.position).normalized * MovementSpeed * Time.deltaTime;
        }
    }
}
