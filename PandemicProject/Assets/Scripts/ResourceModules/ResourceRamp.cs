using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Collider))]
public abstract class ResourceRamp : MonoBehaviour
{
	[SerializeField] ResourceType type = ResourceType.None;

	public Room room;
	public bool isFull = false;
	[SerializeField] public List<ResourceGroup> resourceGroups = new List<ResourceGroup>();

	protected void Start()
	{
		room = GetComponentInParent<Room>();
	}

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
		// check if dice type and amount correspond to excpected ones
		if (TheGameManager.instance.curPlayer.pawn.curRoom == room &&			// is player in this ramp's room
			GetHigherFreeGroup().resourceNeeded == _selection.dice.Count &&     // is dice amount valid 
			AreTypeValid(_selection.dice))	                                    // is dice type valid
		{
			CatchDice(_selection);
			return true;
		}

		return false;
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