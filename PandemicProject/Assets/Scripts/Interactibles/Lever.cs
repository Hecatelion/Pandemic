using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactible
{
	[SerializeField] public ResourceRamp ramp;

    void Start()
    { }

    void Update()
    { }

	private void OnMouseDown()
	{
		if (ramp.IsActivable())
		{
			ramp.Activate();
		}
	}
}
