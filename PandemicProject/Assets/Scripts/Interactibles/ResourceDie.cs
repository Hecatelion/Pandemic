using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDie : Interaclible, IPickable
{
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public override void Interact()
	{
		(this as IPickable).Pick();
	}

	void IPickable.Pick()
	{
		// pick
	}
}
