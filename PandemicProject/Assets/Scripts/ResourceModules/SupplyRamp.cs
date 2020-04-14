using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyRamp : ResourceRamp
{
	[SerializeField] public Player temporary;
	[SerializeField] public Lift lift;

	void Start()
    {
		lift.onDeactivation += GenerateWaste;
	}

    void Update()
    { }

	private void OnMouseDown()
	{
		TryCatchingDice(temporary.selection);
	}

	public override void Activate()
	{
		lift.Activate(GetHigherValue());
	}

	public void GenerateWaste()
	{
		Debug.Log("generate waste here");

		Empty();
	}
}
