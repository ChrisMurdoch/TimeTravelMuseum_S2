using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private Animator DeerMovement;

    void start() {
        DeerMovement = GetComponent<Animator>();
    }

    public void KillTarget() {

        //play death anim
        DeerMovement.SetBool("DeathBool", true);
        DeerMovement.SetBool("WalkBool", false);
    }
}
