using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    private Animator DeerMovement;

    public bool dead; //used to check when the game is over

    void Start() {
        DeerMovement = GetComponent<Animator>();
        dead = false;
    }

    public void KillTarget() {

        //play death anim
        DeerMovement.SetBool("DeathBool", true);
        DeerMovement.SetBool("WalkBool", false);
        dead = true;
    }
}
