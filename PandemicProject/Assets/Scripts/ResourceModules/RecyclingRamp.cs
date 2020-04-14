using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclingRamp : ResourceRamp
{
	[SerializeField] WasteLadder wasteLadder;

    void Start()
    { }

    void Update()
    { }

	public override void Activate()
	{
		wasteLadder.wasteLevel -= GetHigherValue();
		Empty();
	}
}
