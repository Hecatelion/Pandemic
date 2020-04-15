using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : Interactible
{
	public int movementAllowed = 0;
	public Room curRoom;
	public Player player;

	void Start()
    {
        
    }

    void Update()
    {

	}

	private void OnMouseDown()
	{
		if (TheGameManager.instance.curPlayer == player)
		{
			UseSelectionAsMovement(TheGameManager.instance.curPlayer.selection);
		}
	}

	public void UseSelectionAsMovement(Selection _selection)
	{
		foreach (var die in _selection.dice)
		{
			die.PutOnCard();
			movementAllowed++;
		}

		Debug.Log("Player's pawn movement allowed : " + movementAllowed);

		_selection.Flush();
	}

	public void MoveTo(Room _room)
	{
		curRoom = _room;
		transform.position = curRoom.transform.position;
	}
}
