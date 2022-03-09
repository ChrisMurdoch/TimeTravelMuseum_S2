using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TreeCutter : MonoBehaviour
{

    private bool canTrigger;
    private int numTreesCut;
    private bool axeMinigameFinished;

    public TextMeshPro scoreText;

    // Start is called before the first frame update
    void Start()
    {
        canTrigger = true;
        numTreesCut = 0;
        axeMinigameFinished = false;
    }

    void OnTriggerEnter(Collider other) {
        
        GameObject hitObject = other.gameObject;
        Debug.Log("COLLISION with " + hitObject.name);

        if(hitObject.tag == "cuttableTree")
        {
            
            if(hitObject.GetComponent<treeHealthScript>() != null) {
                hitObject.GetComponent<treeHealthScript>().DamageTree();
            }

            canTrigger = false;
        }
    } 

    void OnTriggerExit(Collider other) {

        canTrigger = true;
    }

    public void IncreaseNumTreesCut () {
        numTreesCut++;
        scoreText.text = numTreesCut + " / 8 trees cut";

        if (numTreesCut >= 8) {
            axeMinigameFinished = true;
        }
    }

    public bool AxeMinigameFinished {
        get { return axeMinigameFinished; }
        set { axeMinigameFinished = value; }
    }
}
