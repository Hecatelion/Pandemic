using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TheSettings : MonoBehaviour
{
	public static TheSettings instance;

	public int nbPlayer = 0;
	public float sandtimerDuration = 120;

	public Slider slider;
	public InputField field;

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
		instance.field = FindObjectOfType<InputField>();
	}
}
