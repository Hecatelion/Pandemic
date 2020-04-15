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

	public virtual bool IsActivable()
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

	public bool AreTypeValid(List<ResourceDie> _dice)
	{
		if (type == ResourceType.Any) return true;

		foreach (var die in _dice)
		{
			if (die.faceType != type)
			{
				return false;
			}
		}

		return true;
	}

	public bool TryCatchingDice(Selection _selection)
	{
		_ = 0; // could be improved by accepting more dice than excpected, and try to fill multiple groups

		// check if dice type and amount correspond to excpected ones
		if (GetHigherFreeGroup().resourceNeeded != _selection.dice.Count || 
			!AreTypeValid(_selection.dice))
		{
			return false;
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

	/*public void ReturnDiceToOwner()
	{
		foreach (var die in GetDice())
		{
			die.ReturnToOwner();
		}
	}*/

	public void PutDiceOnCard()
	{
		foreach (var die in GetDice())
		{
			die.PutOnCard();
		}
	}

	public List<ResourceDie> GetDice()
	{
		List<ResourceDie> dice = new List<ResourceDie>();

		foreach (var group in resourceGroups)
		{
			dice.AddRange(group.GetDice());
		}

		return dice;
	}
}