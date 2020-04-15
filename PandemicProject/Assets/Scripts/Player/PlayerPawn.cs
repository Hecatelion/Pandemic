using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : MonoBehaviour
{
	public int movementAllowed = 0;
	public Room curRoom;

	void Start()
    {
        
    }

    void Update()
    {

	}

	private void OnMouseDown()
	{
		UseSelectionAsMovement(TheGameManager.instance.curPlayer.selection);
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
}
