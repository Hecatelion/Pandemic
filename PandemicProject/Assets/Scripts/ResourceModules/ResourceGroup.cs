using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGroup : MonoBehaviour
{
	[HideInInspector] public int resourceNeeded = 0;
	[SerializeField] public int valueProduced = 0; // if (valueProduced == 0) then ResourceRamp can't be activated

	public bool isFull = false;
	[SerializeField] public List<ResourcePlace> resourcePlaces = new List<ResourcePlace>();

	void Start()
	{
		resourceNeeded = resourcePlaces.Count;
	}

    void Update()
    { }

	public void Fill(List<ResourceDie> _dice)
	{
		for (int i = 0; i < resourcePlaces.Count; ++i)
		{
			resourcePlaces[i].Fill(_dice[i]);
		}

		isFull = true;
	}

	public void Empty()
	{
		if (isFull)
		{
			for (int i = 0; i < resourcePlaces.Count; ++i)
			{
				resourcePlaces[i].Empty();
			}
		}

		isFull = false;
	}
}
