using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPawn : Interactible
{
	public int movementAllowed = 0;
	public Room curRoom;
	public Player player;

	Text ui;

	void Start()
	{
		ui = GetComponentInChildren<Text>();
		ui.gameObject.SetActive(false);
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

		ui.gameObject.SetActive(true);
		ui.text = movementAllowed.ToString();

		_selection.Flush();
	}

	public void MoveTo(Room _room)
	{
		curRoom = _room;
		transform.position = curRoom.transform.position;

		ui.gameObject.SetActive(false);
	}
}
