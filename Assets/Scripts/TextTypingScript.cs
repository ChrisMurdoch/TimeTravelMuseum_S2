using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextTypingScript : MonoBehaviour
{
	public Image panel;
	public TextMeshProUGUI txt;
	string story;

	public string[] BeginningSentences; 
	public string[] fishingSentences;

	public string[] axeSentences;
	public string[] axeSentencesFinished;
	public TreeCutter axe;
	public LevelChangeScript levelChanger;

	public string[] spearSentences;
	public string[] spearSentencesFinished;
	private GameObject[] spearTargets; //used to track when spear game is finished


	public string[] hoeSentences;

	public Coroutine textCoroutine;

	private bool textFinished;

	void Awake()
	{
		Color tempColor = panel.color;
		tempColor.a = 0f;
		panel.color = tempColor;

		txt = GetComponent<TextMeshProUGUI>();
		textFinished = false;

		story = txt.text;
		txt.text = "";
		StartCoroutine(FadeTextToFullAlpha(1f, txt));
		StartCoroutine(FadeToFullAlpha(.06f, panel));
		textCoroutine = StartCoroutine("PlayText");
	}

	public IEnumerator PlayText()
	{
		yield return new WaitForSeconds(1f);
		if(SceneManager.GetActiveScene().buildIndex == 1) //index for main scene
        {

			foreach (string sentence in BeginningSentences)
			{
				textFinished = false;
				StartCoroutine(FadeTextToFullAlpha(1f, txt));

				story = sentence;
				foreach (char c in story)
				{
					//allows player to skip text loading
					if (OVRInput.Get(OVRInput.Button.One)){
						string leftOver = story.Remove(0, txt.text.ToString().Length); //get the part of string that hasn't been added to text yet
						txt.text += leftOver; //add the rest of the text instantaneously
						break; //end loop
					}

					txt.text += c;
					yield return new WaitForSeconds(0.060f);
				}

				yield return new WaitForSeconds(1);

				textFinished = true;

				while (!OVRInput.Get(OVRInput.Button.One) && textFinished) {
					yield return null; //wait until player presses button;
				}

				StartCoroutine(FadeTextToZeroAlpha(0.060f, txt));
				txt.text = "";
				yield return new WaitForSeconds(0.5f);
			}
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2) //index for fishing scene
        {
			foreach (string sentence in fishingSentences)
			{
				textFinished = false;
				StartCoroutine(FadeTextToFullAlpha(1f, txt));
				story = sentence;

				foreach (char c in story)
				{
					//allows player to skip text loading
					if (OVRInput.Get(OVRInput.Button.One)) {
						string leftOver = story.Remove(0, txt.text.ToString().Length); //get the part of string that hasn't been added to text yet
						txt.text += leftOver; //add the rest of the text instantaneously
						break; //end loop
					}

					txt.text += c;
					yield return new WaitForSeconds(0.060f);
				}

				yield return new WaitForSeconds(1);

				textFinished = true;

				while (!OVRInput.Get(OVRInput.Button.One) && textFinished) {
					yield return null; //wait until player presses button;
				}

				StartCoroutine(FadeTextToZeroAlpha(0.060f, txt));
				txt.text = "";
				yield return new WaitForSeconds(0.5f);
			}
		}

		else if (SceneManager.GetActiveScene().buildIndex == 3) //axe scene index 
		{
			Debug.Log("playing axe scene text");
			if(!axe.AxeMinigameFinished) {
				foreach (string sentence in axeSentences)
				{
					textFinished = false;
					StartCoroutine(FadeTextToFullAlpha(1f, txt));
					story = sentence;

					foreach (char c in story)
					{
						//allows player to skip text loading
						if (OVRInput.Get(OVRInput.Button.One)) {
							string leftOver = story.Remove(0, txt.text.ToString().Length); //get the part of string that hasn't been added to text yet
							txt.text += leftOver; //add the rest of the text instantaneously
							break; //end loop
						}

						txt.text += c;
						yield return new WaitForSeconds(0.060f);
					}

					yield return new WaitForSeconds(1);

					textFinished = true;

					while (!OVRInput.Get(OVRInput.Button.One) && textFinished) {
						yield return null; //wait until player presses button;
					}

					StartCoroutine(FadeTextToZeroAlpha(0.060f, txt));
					txt.text = "";
					yield return new WaitForSeconds(0.5f);
				}

			} else {

				StartCoroutine(FadeToFullAlpha(0.06f, panel));
				foreach (string sentence in axeSentencesFinished)
				{
					textFinished = false;
					StartCoroutine(FadeTextToFullAlpha(1f, txt));
					story = sentence;

					foreach (char c in story)
					{
						//allows player to skip text loading
						if (OVRInput.Get(OVRInput.Button.One)) {
							string leftOver = story.Remove(0, txt.text.ToString().Length); //get the part of string that hasn't been added to text yet
							txt.text += leftOver; //add the rest of the text instantaneously
							break; //end loop
						}

						txt.text += c;
						yield return new WaitForSeconds(0.060f);
					}

					yield return new WaitForSeconds(1);

					textFinished = true;

					while (!OVRInput.Get(OVRInput.Button.One) && textFinished) {
						yield return null; //wait until player presses button;
					}

					StartCoroutine(FadeTextToZeroAlpha(0.060f, txt));
					txt.text = "";
					yield return new WaitForSeconds(0.5f);
				}

				levelChanger.FadeToLevel(1); //exit level when finished displaying text
			}
		}

		else if (SceneManager.GetActiveScene().buildIndex == 4) //spear scene index
		{
			spearTargets = GameObject.FindGameObjectsWithTag("spearTarget");
			Debug.Log("PLAY SPEAR TEXT");
			if(spearTargets.Length > 0) //spear game isn't over, target hasn't been destroyed
			{
				foreach (string sentence in spearSentences)
				{
					textFinished = false;
					StartCoroutine(FadeTextToFullAlpha(1f, txt));
					story = sentence;

					foreach (char c in story)
					{
						//allows player to skip text loading
						if (OVRInput.Get(OVRInput.Button.One)) {
							string leftOver = story.Remove(0, txt.text.ToString().Length); //get the part of string that hasn't been added to text yet
							txt.text += leftOver; //add the rest of the text instantaneously
							break; //end loop
						}

						txt.text += c;
						yield return new WaitForSeconds(0.060f);
					}

					yield return new WaitForSeconds(1);

					textFinished = true;

					while (!OVRInput.Get(OVRInput.Button.One) && textFinished) {
						yield return null; //wait until player presses button;
					}

					StartCoroutine(FadeTextToZeroAlpha(0.060f, txt));
					txt.text = "";
					yield return new WaitForSeconds(0.5f);
				}
			} else {

				Debug.Log("GAME FINISHED");

				StartCoroutine(FadeToFullAlpha(0.06f, panel));
				foreach(string sentence in spearSentencesFinished)
				{
					textFinished = false;
					StartCoroutine(FadeTextToFullAlpha(1f, txt));
					story = sentence;

					foreach (char c in story)
					{
						//allows player to skip text loading
						if (OVRInput.Get(OVRInput.Button.One)) {
							string leftOver = story.Remove(0, txt.text.ToString().Length); //get the part of string that hasn't been added to text yet
							txt.text += leftOver; //add the rest of the text instantaneously
							break; //end loop
						}

						txt.text += c;
						yield return new WaitForSeconds(0.060f);
					}

					yield return new WaitForSeconds(1);

					textFinished = true;

					while (!OVRInput.Get(OVRInput.Button.One) && textFinished) {
						yield return null; //wait until player presses button;
					}

					StartCoroutine(FadeTextToZeroAlpha(0.060f, txt));
					txt.text = "";
					yield return new WaitForSeconds(0.5f);
				}

				levelChanger.FadeToLevel(1); //exit level when finished displaying text
			}
		}

		else if (SceneManager.GetActiveScene().buildIndex == 5) //hoe scene index
		{
			foreach (string sentence in hoeSentences)
			{
				textFinished = false;
				StartCoroutine(FadeTextToFullAlpha(1f, txt));
				story = sentence;

				foreach (char c in story)
				{
					//allows player to skip text loading
					if (OVRInput.Get(OVRInput.Button.One)) {
						string leftOver = story.Remove(0, txt.text.ToString().Length); //get the part of string that hasn't been added to text yet
						txt.text += leftOver; //add the rest of the text instantaneously
						break; //end loop
					}

					txt.text += c;
					yield return new WaitForSeconds(0.060f);
				}

				yield return new WaitForSeconds(1);

				textFinished = true;

				while (!OVRInput.Get(OVRInput.Button.One) && textFinished) {
					yield return null; //wait until player presses button;
				}

				StartCoroutine(FadeTextToZeroAlpha(0.060f, txt));
				txt.text = "";
				yield return new WaitForSeconds(0.5f);
			}
		}

		StartCoroutine(FadeToZeroAlpha(1f, panel));
		StartCoroutine(FadeTextToZeroAlpha(1f, txt));
	}

	public IEnumerator FadeToFullAlpha(float t, Image i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 0.5686275f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + t);
			Debug.Log("New Alpha = " + i.color.a);
			yield return null;
		}
	}

	public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
		while (i.color.a < 1f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
			yield return null;
		}
	}
	public IEnumerator FadeToZeroAlpha(float t, Image i)
	{
		yield return new WaitForSeconds(1f);
		i.color = new Color(i.color.r, i.color.g, i.color.b, 0.5686275f);
		while (i.color.a > 0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}

	public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
	{
		i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
		while (i.color.a > 0f)
		{
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
			yield return null;
		}
	}
}
