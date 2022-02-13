using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class scoreKeeper : MonoBehaviour
{
    public int score;
    public Text scoreText;
    public GameObject levelChanger;
    public GameObject fishScoreText;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(score >= 10)
        {
            Debug.Log("Send Player to Museum");
            levelChanger.GetComponent<LevelChangeScript>().FadeToLevel(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "fish")
        {
            //fish deposited
            Destroy(other.gameObject, 0.3f);
            score++;
            print(score);
            fishScoreText.GetComponent<TextMesh>().text = score + "/10 Fish collected";
            scoreText.text = "Score: " + score;
            Debug.Log("Destroyed object");
        }
    }
}
