using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingRamp : ResourceRamp
{
	WasteLadder wasteLadder;

    new void Start()
	{
		base.Start();

		wasteLadder = FindObjectOfType<WasteLadder>();
	}

    void Update()
    { }

	private void OnMouseDown()
	{
		TryCatchingDice(TheGameManager.instance.curPlayer.selection);
	}

	public override void Activate()
	{
		int wasteLevel = wasteLadder.wasteLevel;
		wasteLevel -= GetHigherValue();
		if (wasteLevel < 0) wasteLevel = 0;

		wasteLadder.SetWasteLevel(wasteLevel);

		PutDiceOnCard();
		Empty();
	}
}
