using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSlot : MonoBehaviour
{
	public ResourceDie die;
	public bool isFree = true;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void Catch(ResourceDie _die)
	{
		die = _die;

		die.AssignTo(transform);
		die.AllowPhysics(false);
		die.Unlock();

		isFree = false;
	}

	public void Empty()
	{
		die = null;
		isFree = true;
	}
}
