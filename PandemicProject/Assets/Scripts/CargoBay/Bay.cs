using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bay : MonoBehaviour
{
	SupplySlot[] slots;
	Airplane airplane;

	void Start()
	{
		airplane = FindObjectOfType<Airplane>();
		slots = GetComponentsInChildren<SupplySlot>();
	}

	void Update()
	{

	}

	public void Catch(Supply _supply)
	{
		int index = GetFreeSlotIndex();
		slots[index].Catch(_supply);
	}

	public void Release(Supply _supply)
	{
		(from slot in slots where slot.supply == _supply select slot).ToArray()[0].Empty();
	}

	public int GetFreeSlotIndex()
	{
		for (int i = 0; i < slots.Count(); i++)
		{
			if (slots[i].isFree)
			{
				return i;
			}
		}

		Debug.Log("bay : no free slot");
		return -1;
	}

	public void SendSupplies()
	{
		// should return sent supplies to owner room & reset sandtimer
		List<ResourceType> resourcesNeeded = new List<ResourceType>();
		for (int i = 0; i < airplane.curCity.resourcesNeeded.Length; i++)
		{
			resourcesNeeded.Add(airplane.curCity.resourcesNeeded[i]);
		}

		foreach (var slot in slots)
		{
			if (!slot.isFree)
			{
				foreach (var resourceNeeded in resourcesNeeded)
				{
					if (slot.supply.type == resourceNeeded)
					{
						slot.supply.ReturnToHomeRoom();
						slot.Empty();

						resourcesNeeded.Remove(resourceNeeded);
						break;
					}
				}
			}
		}

		airplane.curCity.Rescue();
	}

	public bool HasValidSupplies()
	{
		List<Supply> supplies = GetSupplies();
		return airplane.curCity.CanBeRescued(supplies);
	}

	public int GetFreeSlotAmount()
	{
		int amount = 0;

		for (int i = 0; i < slots.Count(); i++)
		{
			if (slots[i].isFree)
			{
				amount++;
			}
		}

		return amount;
	}

	public List<Supply> GetSupplies()
	{
		return (from slot in slots where !slot.isFree select slot.supply).ToList();
	}
}