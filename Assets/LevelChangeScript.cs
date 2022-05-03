using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelChangeScript : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    public Image faderImage;


    void Update()
    {
        
    }

    public void FadeToLevel(int levelIndex)
    {
        Debug.Log("called FadetoLevel");
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
        Color trigger = faderImage.color;
        trigger.a = 1;
        faderImage.color = trigger;

    }

    //public void OnFadeComplete()
    //{
    //    AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);
    //    SceneManager.LoadScene(levelToLoad);
    //}

    public IEnumerator OnFadeComplete()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelToLoad);
        Debug.Log("Loading main scene");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
