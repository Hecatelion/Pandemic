using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
	public List<ResourceDie> dice;
	public Player player;
	[SerializeField] Material mat;

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
			die.meshRenderer.material = die.baseMat;
		}

		dice.Clear();
	}

	public void Select(ResourceDie _die)
	{
		if (!dice.Contains(_die))
		{
			_die.isSelected = true;
			dice.Add(_die);
			_die.meshRenderer.material = mat;
		}
	}

	public void Unselect(ResourceDie _die)
	{
		if (dice.Contains(_die))
		{
			_die.isSelected = false;
			dice.Remove(_die);
			_die.meshRenderer.material = _die.baseMat;
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
