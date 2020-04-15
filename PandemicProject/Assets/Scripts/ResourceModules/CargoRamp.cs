using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargoRamp : ResourceRamp
{
	Bay bay;

    void Start()
    {
		bay = FindObjectOfType<Bay>();
    }

    void Update()
    {

	}

	private void OnMouseDown()
	{
		TryCatchingDice(TheGameManager.instance.curPlayer.selection);
	}

	public override bool IsActivable()
	{
		return base.IsActivable() && bay.HasValidSupplies();
	}

	public override void Activate()
	{
		bay.SendSupplies();

		ReturnDiceToOwner();
		Empty();
	}
}
