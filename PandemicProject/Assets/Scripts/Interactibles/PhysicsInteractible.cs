using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsInteractible : Interactible
{
	public void AssignTo(Transform _parent)
	{
		transform.SetParent(_parent);
		transform.localPosition = Vector3.zero;
	}

	public void AllowPhysics(bool _b = true)
	{
		gameObject.GetComponent<Rigidbody>().isKinematic = !_b;
	}
}
