using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class RunToRandomPoint : MonoBehaviour
{
    Vector3 targetPos;
    float MovementSpeed = 5.0f;
    float CloseEnough = 5.0f;
    public NavMeshAgent nav;
 
    IEnumerator Start()
    {
   

        //   nav.transform.position.x
        targetPos = nav.transform.position;
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            targetPos = new Vector3(nav.transform.position.x + (Random.Range(-10 * nav.transform.localScale.x, 10 * nav.transform.localScale.x)), nav.transform.position.y, nav.transform.position.z + (Random.Range(-5 * nav.transform.localScale.z, 5 * nav.transform.localScale.z)));
         //   nav.SetDestination(targetPos);
            while ((nav.transform.position - targetPos).magnitude > CloseEnough )
            {
                yield return new WaitForSeconds(0.0f);
            }
        }
    }

    /*
        void OnCollisionEnter(Collision collision)
        {
        Start();
       // Update();
        Debug.Log(" " + collision.gameObject.name);
      //  targetPos = new Vector3(nav.transform.position.x + (Random.Range(-10 * nav.transform.localScale.x, 10 * nav.transform.localScale.x)), nav.transform.position.y, nav.transform.position.z + (Random.Range(-5 * nav.transform.localScale.z, 5 * nav.transform.localScale.z)));
    }
        */

    void Update()
    {

        if ((nav.transform.position - targetPos).magnitude > CloseEnough )
        {
            nav.transform.LookAt(targetPos);
            nav.SetDestination(targetPos);
          //  nav.transform.position += (targetPos - nav.transform.position).normalized * MovementSpeed * Time.deltaTime;
        }
    }
}
