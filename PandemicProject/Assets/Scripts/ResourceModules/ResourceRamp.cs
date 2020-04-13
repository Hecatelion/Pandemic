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

	public bool IsActivable()
	{
		return GetHigherValue() > 0;
	}

	public int GetHigherValue()
	{
		for (int i = resourceGroups.Count - 1; i != 0; --i)
		{
			if (resourceGroups[i].isFull)
			{
				return resourceGroups[i].valueProduced;
			}
		}

		return 0;
	}

	public bool IsFull()
	{
		return resourceGroups.Last().isFull;
	}

	public void CatchDice(List<ResourceDie> _dice)
	{
		_ = 0; // c.f. CarnetOrange/ • ramp
	}
}
