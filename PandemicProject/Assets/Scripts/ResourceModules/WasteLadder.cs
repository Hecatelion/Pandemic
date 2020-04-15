using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteLadder : MonoBehaviour
{
	public int wasteLevel = 0;

	DiceThrower diceThrower;

    void Start()
    {
		diceThrower = FindObjectOfType<DiceThrower>();

		SetWasteLevel(0);
	}

    void Update()
    {
		// if (wasteLevel > 10) Lose();
		_ = 0;
    }

	public void StartWasteGeneration(List<ResourceDie> _dice)
	{
		diceThrower.onThrowEnd += GenerateWaste;

		diceThrower.Throw(_dice);
	}

	void GenerateWaste(List<ResourceDie> _dice)
	{
		diceThrower.onThrowEnd -= GenerateWaste;

		int amount = 0;

		foreach (var die in _dice)
		{
			if (die.faceType == ResourceType.Food ||
				die.faceType == ResourceType.Water ||
				die.faceType == ResourceType.Plane)
			{
				amount++;
			}
		}

		SetWasteLevel(amount);
	}

	public void SetWasteLevel(int _level)
	{
		wasteLevel = _level;
		Debug.Log(wasteLevel);
		
		// move waste pawn
	}
}
