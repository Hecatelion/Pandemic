using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
	[SerializeField] public DiceHand hand;
	[SerializeField] public List<ResourceDie> dice;
	[SerializeField] public GameObject selectionPrefab;
	[SerializeField] public PlayerPawn pawn;
	public Selection selection;
	public RoleCard card;

    void Start()
    {
		card = GetComponentInChildren<RoleCard>();
		pawn = GetComponentInChildren<PlayerPawn>();
		pawn.player = this;
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
