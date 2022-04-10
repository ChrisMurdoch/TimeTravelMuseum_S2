using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearSpawner : MonoBehaviour
{
    public GameObject currentSpear;
    public GameObject spearPrefab;

    public TextTypingScript textTrigger;
    [HideInInspector] public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpear == null) {
            SpawnNewSpear();
        }

        if (gameOver) {
            Debug.Log("GAME OVER");

            //stop coroutine and clear text to prevent overlapping
            StopCoroutine(textTrigger.textCoroutine);
            textTrigger.txt.text = "";
            textTrigger.textCoroutine = StartCoroutine(textTrigger.PlayText());
            gameOver = false; //prevents coroutine from starting over every frame
        }
    }

    void SpawnNewSpear() {
        currentSpear = Instantiate(spearPrefab, this.transform.position, this.transform.rotation);
    }
}
