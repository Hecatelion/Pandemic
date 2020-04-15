using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplySlot : MonoBehaviour
{
	public Supply supply;
	public bool isFree = true;

	void Start()
    {
        
    }

    void Update()
    {

	}

	public void Catch(Supply _supply)
	{
		supply = _supply;

		supply.AssignTo(transform);
		supply.AllowPhysics(false);
		supply.Unlock();

		isFree = false;
	}

	public void Empty()
	{
		supply = null;
		isFree = true;
	}
}
