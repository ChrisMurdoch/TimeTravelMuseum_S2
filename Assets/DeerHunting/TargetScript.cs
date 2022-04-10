using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{


    //plays death animation and destroys itself
    public void KillTarget() {

        //play death anim
        Destroy(this.gameObject);
    }
}
