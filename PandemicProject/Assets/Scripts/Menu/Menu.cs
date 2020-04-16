using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    { }

	public void UI_Play()
	{
		TheSettings.instance.nbPlayer = (int)TheSettings.instance.slider.value;
		TheSettings.instance.sandtimerDuration = (TheSettings.instance.field.text != "") ? int.Parse(TheSettings.instance.field.text) : 120;
		SceneManager.LoadScene(1);
	}

	public void UI_Quit()
	{
		Application.Quit();
	}
}
