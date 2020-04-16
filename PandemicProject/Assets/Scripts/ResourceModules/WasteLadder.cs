using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteLadder : MonoBehaviour
{
	public int wasteLevel = 0;

	[SerializeField] GameObject wastePawn;
	[SerializeField] GameObject pawnPositionsObj;
	List<Vector3> pawnPositions = new List<Vector3>();

	DiceThrower diceThrower;

    void Start()
    {
		// init pawnPositions
		foreach (Transform t in pawnPositionsObj.transform)
		{
			if (t != pawnPositionsObj.transform)
			{
				pawnPositions.Add(t.position);
			}
		}

		diceThrower = FindObjectOfType<DiceThrower>();

		SetWasteLevel(0);
	}

    void Update()
    { }

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

		// move waste pawn
		wastePawn.transform.position = pawnPositions[wasteLevel];

		// lose check
		if (wasteLevel > 10) TheGameManager.instance.Lose();
	}
}
