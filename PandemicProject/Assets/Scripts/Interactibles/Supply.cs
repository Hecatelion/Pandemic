using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Supply : Interaclible, IPickable
{
	[SerializeField] ResourceType type = ResourceType.None;

    void Start()
    { }

    void Update()
    { }

	override public void Interact()
	{
		(this as IPickable).Pick();
	}

	void IPickable.Pick()
	{
		// pick
	}
}
