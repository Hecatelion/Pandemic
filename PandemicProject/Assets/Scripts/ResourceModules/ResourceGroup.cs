using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGroup : MonoBehaviour
{
	[SerializeField] public int resourceNeeded = 0;
	[SerializeField] public int valueProduced = 0; // if (valueProduced == 0) then ResourceRamp can't be activated

	public bool isFull = false;
	[SerializeField] public List<ResourcePlace> resourcePlaces = new List<ResourcePlace>();

	void Start()
    { }

    void Update()
    { }

	public void Fill(List<ResourceDie> _dice)
	{
		if (resourcePlaces.Count != _dice.Count) Debug.LogError("ResourceGroup \"" + this.name + "\" failed to be filled : given dice amount != resource places amount");

		for (int i = 0; i < resourcePlaces.Count; ++i)
		{
			resourcePlaces[i].Fill(_dice[i]);
		}

		isFull = true;
	}

	public void Empty()
	{
		for (int i = 0; i < resourcePlaces.Count; ++i)
		{
			resourcePlaces[i].Empty();
		}

		isFull = false;
	}
}
