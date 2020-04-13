using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// used as an interface but contains a variable
public abstract class Interaclible : MonoBehaviour
{
	// prevent from being used while other modules are running
	// e.g. don't throw dice while sending supplies
	public bool isLocked = false;

	public abstract void Interact();

	public void Lock()
	{
		isLocked = true;
	}

	public void Unlock()
	{
		isLocked = false;
	}
}
