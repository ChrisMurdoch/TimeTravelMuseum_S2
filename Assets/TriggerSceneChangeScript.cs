using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneChangeScript : MonoBehaviour
{
    public GameObject levelChanger;

    private bool sceneChanged;

    void Start()
    {
        sceneChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!sceneChanged && (other.tag == "Player" || other.transform.root.tag == "Player"))
        {
            if(this.gameObject.tag == "door" && SceneManager.GetActiveScene().buildIndex != 1) //enter main hub from any scene
            {
                levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(1);
            }

            else if (this.gameObject.tag == "door") //go back to tutorial from main hub
            {
                levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(0);
            }

            else if(this.gameObject.tag == "trap") //enter fishing scene from main hub
            {
                levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(2);
            }

            else if (this.gameObject.tag == "axe") //enter axe scene from main hub
            {
                levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(3);
            }

            else if (this.gameObject.tag == "atlatl") //enter spear scene from main hub
            {
                Debug.Log("trigger scene change");
                levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(4);
            }

            else if (this.gameObject.tag == "hoe") //enter hoe scene from main hub
            {
                levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(5);
            }

            sceneChanged = true;

        }
    }
}
