using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftButton : MonoBehaviour
{
	[SerializeField] public Lift lift;

	void Start()
    {
        
    }

    void Update()
    {
	
    }

	private void OnMouseDown()
	{
		lift.TrySendingSupply();
	}
}
