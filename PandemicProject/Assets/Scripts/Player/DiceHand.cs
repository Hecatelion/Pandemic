using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DiceHand : MonoBehaviour
{
	DiceSlot[] slots;

    void Start()
    {
		slots = GetComponentsInChildren<DiceSlot>();
	}

    void Update()
    {
        
    }

	public void Catch(ResourceDie _die)
	{
		int index = GetFreeSlotIndex();
		slots[index].Catch(_die);
	}

	public void Release(ResourceDie _die)
	{
		(from slot in slots where slot.die == _die select slot).ToArray()[0].Empty();
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

		Debug.Log("dice hand : no free slot");
		return -1;
	}

	public ResourceDie[] GetDice()
	{
		return (from slot in slots where !slot.isFree select slot.die).ToArray();
	}
}
