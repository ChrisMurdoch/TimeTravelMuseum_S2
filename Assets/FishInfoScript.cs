using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FishInfoScript : MonoBehaviour
{
	public Image panel;
	public TextMeshProUGUI txt;
	string story;



	void Awake()
	{

		Color tempColor = panel.color;
		tempColor.a = 0f;
		panel.color = tempColor;
		

		
	}

    private void OnTriggerEnter(Collider other)
    {
		if(other.tag == "Player")
        {
			story = txt.text;
			txt.text = "";
			StartCoroutine(FadeTextToFullAlpha(1f, txt));
			StartCoroutine(FadeToFullAlpha(1f, panel));
			StartCoroutine("PlayText");
			FadeToZeroAlpha(1f, panel);
			FadeTextToZeroAlpha(1f, txt);
		}
		
	}

  //  private void OnTriggerExit(Collider other)
  //  {
		//if(other.tag == "Player")
  //      {
		//	FadeToZeroAlpha(1f, panel);
		//	FadeTextToZeroAlpha(1f, txt);
		//}

  //  }
    IEnumerator PlayText()
	{
		foreach (char c in story)
		{
			txt.text += c;
			yield return new WaitForSeconds(0.06f);
		}
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
