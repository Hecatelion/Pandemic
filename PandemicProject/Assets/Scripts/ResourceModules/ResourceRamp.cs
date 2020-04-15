using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider))]
public abstract class ResourceRamp : MonoBehaviour
{
	[SerializeField] ResourceType type = ResourceType.None;

	public bool isFull = false;
	[SerializeField] public List<ResourceGroup> resourceGroups = new List<ResourceGroup>();

	void Start()
	{ }

	void Update()
	{ }

	public abstract void Activate();

	public bool IsActivable()
	{
		return GetHigherValue() > 0;
	}

	public int GetHigherValue()
	{
		for (int i = resourceGroups.Count - 1; i >= 0; --i)
		{
			if (resourceGroups[i].isFull)
			{
				return resourceGroups[i].valueProduced;
			}
		}

		return 0;
	}

	public ResourceGroup GetHigherFreeGroup()
	{
		if (this.IsFull()) return null;

		foreach (var group in resourceGroups)
		{
			if (!group.isFull)
			{
				return group;
			}
		}

		Debug.LogError("ResourceRamp.GetHigherFreeGroup() in " + this.name + "failed. Ramp is not full but did not find any not full group.");
		return null;
	}

	public bool IsFull()
	{
		return resourceGroups.Last().isFull;
	}

	public void CatchDice(Selection _selection)
	{
		_selection.TakeDiceOutOfHand();
		GetHigherFreeGroup().Fill(_selection.dice);
		_selection.Flush();
	}

	// check if dice type and amount correspond to excpected ones
	public bool TryCatchingDice(Selection _selection)
	{
		_ = 0; // could be improved by accepting more dice than excpected, and try to fill multiple groups

		if (GetHigherFreeGroup().resourceNeeded != _selection.dice.Count)
		{
			return false;
		}

		foreach (var die in _selection.dice)
		{
			if (die.faceType != type)
			{
				return false;
			}
		}

		CatchDice(_selection);
		return true;
	}

	public void Empty()
	{
		foreach (var group in resourceGroups)
		{
			group.Empty();
		}
	}
}