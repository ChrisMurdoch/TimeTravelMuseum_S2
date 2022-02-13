using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerSceneChangeScript : MonoBehaviour
{
    public GameObject levelChanger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(this.gameObject.tag == "door" && SceneManager.GetActiveScene().buildIndex == 0)
            {
                levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(1);
            }
            if(this.gameObject.tag == "trap" && SceneManager.GetActiveScene().buildIndex == 1)
            {
                levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(2);
            }
            if (this.gameObject.tag == "door" && SceneManager.GetActiveScene().buildIndex == 1)
            {
                levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(0);
            }
            if (this.gameObject.tag == "door" && SceneManager.GetActiveScene().buildIndex == 2)
            {
                levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(1);
            }
            //if (SceneManager.GetActiveScene().buildIndex == 2)
            //{
            //    levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(1);
            //}

        }
    }
}
