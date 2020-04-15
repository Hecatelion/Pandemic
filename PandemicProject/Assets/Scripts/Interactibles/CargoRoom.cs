using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoRoom : Room
{
	public Bay bay;
    void Start()
    {
		bay = FindObjectOfType<Bay>();
    }

    void Update()
    {
        
    }

	public void ReceiveSupply(Supply _supply)
	{
		_supply.SendToRoom(this);
		bay.Catch(_supply);
	}
}
