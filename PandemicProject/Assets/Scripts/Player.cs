using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
	[SerializeField] public List<ResourceDie> dice;
	[SerializeField] public GameObject selectionPrefab;
	public Selection selection;

    void Start()
    {
		selection = Instantiate(selectionPrefab, transform.position, Quaternion.identity).GetComponent<Selection>();
    }

    void Update()
    {
        
    }

	public List<ResourceDie> GetUsableDice()
	{
		return (from die in dice where !die.isLocked select die).ToList();
	}
}
