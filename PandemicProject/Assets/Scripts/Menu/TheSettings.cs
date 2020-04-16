using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheSettings : MonoBehaviour
{
	public static TheSettings instance;

	public int nbPlayer = 0;
	public Slider slider;

    void Start()
    {
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(instance.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}

		instance.slider = FindObjectOfType<Slider>();
	}
}
