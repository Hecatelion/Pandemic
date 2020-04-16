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
	DiceThrower diceThrower;

	public int nbThrowMax = 2;
	public int nbThrow;

    void Start()
    {
		nbThrow = nbThrowMax;

		diceThrower = FindObjectOfType<DiceThrower>();
		card = GetComponentInChildren<RoleCard>();
		pawn = GetComponentInChildren<PlayerPawn>();
		pawn.player = this;
		selection = Instantiate(selectionPrefab, transform.position, Quaternion.identity).GetComponent<Selection>();
		selection.player = this;
		
		pawn.MoveTo(Room.FindRoomOfType(card.spawingRoomType));
    }

    void Update()
    {
        
    }

	public List<ResourceDie> GetUsableDice()
	{
		return (from die in dice where !die.isLocked select die).ToList();
	}

	public void StartTurn()
	{
		Debug.Log(name + " start turn");

		// allow dice
		foreach (var die in dice)
		{
			if (die.isOnCard)
			{
				die.ReturnToOwner();
			}
		}

		// first throw
		selection.SelectAllDice();
		diceThrower.ThrowSelection();

		//
		nbThrow = nbThrowMax;
	}

	public void EndTurn()
	{
		Debug.Log(name + " start turn");

		// disallow dice 
		foreach (var die in hand.GetDice())
		{
			hand.Release(die);
			die.PutOnCard();
		}

		// pawn 
		pawn.movementAllowed = 0;
	}
}
