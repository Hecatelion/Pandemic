using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyRamp : ResourceRamp
{
	[SerializeField] public Lift lift;

	WasteLadder wasteLadder;

	new void Start()
	{
		base.Start();

		wasteLadder = FindObjectOfType<WasteLadder>();

		lift.onDeactivation += GenerateWaste;
	}

    void Update()
    { }

	private void OnMouseDown()
	{
		TryCatchingDice(TheGameManager.instance.curPlayer.selection);
	}

	public override void Activate()
	{
		lift.Activate(GetHigherValue());
	}

	public void GenerateWaste()
	{
		wasteLadder.StartWasteGeneration(GetDice());

		Empty();
	}
}
