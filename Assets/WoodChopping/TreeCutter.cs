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
    private AudioSource chopSource;

    public TextMeshPro scoreText;
    public TextTypingScript textTrigger;
    

    // Start is called before the first frame update
    void Start()
    {
        canTrigger = true;
        numTreesCut = 0;
        axeMinigameFinished = false;
        chopSource = this.gameObject.GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other) {
        
        GameObject hitObject = other.gameObject;
        Debug.Log("COLLISION with " + hitObject.name);

        if(hitObject.tag == "cuttableTree" && canTrigger)
        {
            Debug.Log("hit object tag correct & canTrigger");
            if(hitObject.GetComponent<treeHealthScript>() != null) {
                Debug.Log("FOUND TREEHEALTH SCRIPT");
                hitObject.GetComponent<treeHealthScript>().DamageTree();
            }
            
            chopSource.Play(); //play chopping audio when tree is hit
            canTrigger = false;
        }
    } 

    void OnTriggerExit(Collider other) {
        Debug.Log("EXIT COLLIDER");
        canTrigger = true;
    }

    public void IncreaseNumTreesCut () {
        numTreesCut++;
        scoreText.text = numTreesCut + " / 8 trees cut";

        if (numTreesCut >= 8) { 
            Debug.Log("GAME FINISHED");
            axeMinigameFinished = true;

            //stop coroutine and clear text to prevent overlapping
            StopCoroutine(textTrigger.textCoroutine);
            textTrigger.txt.text = "";
            textTrigger.textCoroutine = StartCoroutine(textTrigger.PlayText());
        }
    }

    public bool AxeMinigameFinished {
        get { return axeMinigameFinished; }
        set { axeMinigameFinished = value; }
    }
}
