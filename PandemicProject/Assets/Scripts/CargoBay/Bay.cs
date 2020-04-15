using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bay : MonoBehaviour
{
	SupplySlot[] slots;

	void Start()
	{
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
		Debug.Log("Bay sending supplies. (needs to be implemented)");

		// should return sent supplies to owner room & reset sandtimer

		foreach (var slot in slots)
		{
			if (!slot.isFree)
			{
				slot.supply.ReturnToHomeRoom();
				slot.Empty();
			}
		}
	}

	public bool HasValidSupplies()
	{
		Debug.Log("Bay supply check. (needs to be implemented)");

		// should compare with current town supply needs
		return true;
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
}