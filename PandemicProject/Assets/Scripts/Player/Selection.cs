using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
	public List<ResourceDie> dice;
	public Player player;

	void Start()
    {
        
    }

    void Update()
    {

	}

	public void Flush()
	{
		foreach (var die in dice)
		{
			die.isSelected = false;
		}

		dice.Clear();
	}

	public void Select(ResourceDie _die)
	{
		if (!dice.Contains(_die))
		{
			_die.isSelected = true;
			dice.Add(_die);

			Debug.Log("Dice selected : " + _die.faceType);
		}
	}

	public void Unselect(ResourceDie _die)
	{
		if (dice.Contains(_die))
		{
			_die.isSelected = false;
			dice.Remove(_die);

			Debug.Log("Dice unselected");
		}
	}

	public void TakeDiceOutOfHand()
	{
		foreach (var die in dice)
		{
			die.TakeOutOfHand();
		}
	}

	public void SelectAllDice()
	{
		foreach (var die in player.hand.GetDice())
		{
			Select(die);
		}
	}
}
