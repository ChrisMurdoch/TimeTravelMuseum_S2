using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextTypingScript : MonoBehaviour
{
	public Image panel;
	TextMeshProUGUI txt;
	string story;

	public string[] BeginningSentences; 

	public string[] fishingSentences;



	void Awake()
	{
		
		Color tempColor = panel.color;
		tempColor.a = 0f;
		panel.color = tempColor;
		txt = GetComponent<TextMeshProUGUI>();

		story = txt.text;
		txt.text = "";
		StartCoroutine(FadeTextToFullAlpha(1f, txt));
		StartCoroutine(FadeToFullAlpha(1f, panel));
		StartCoroutine("PlayText");
	}

	IEnumerator PlayText()
	{
		yield return new WaitForSeconds(1f);
		if(SceneManager.GetActiveScene().buildIndex == 1)
        {

			foreach (string sentence in BeginningSentences)
			{

				StartCoroutine(FadeTextToFullAlpha(1f, txt));

				story = sentence;
				foreach (char c in story)
				{
					txt.text += c;
					yield return new WaitForSeconds(0.060f);
				}
				yield return new WaitForSeconds(1f);
				txt.text = "";
				StartCoroutine(FadeTextToZeroAlpha(0.060f, txt));
				yield return new WaitForSeconds(1f);
			}
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
			foreach (string sentence in fishingSentences)
			{
				StartCoroutine(FadeTextToFullAlpha(1f, txt));
				story = sentence;
				foreach (char c in story)
				{
					txt.text += c;
					yield return new WaitForSeconds(0.060f);
				}
				yield return new WaitForSeconds(1f);
				txt.text = "";
				StartCoroutine(FadeTextToZeroAlpha(1f, txt));
				yield return new WaitForSeconds(1f);
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
			i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
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
