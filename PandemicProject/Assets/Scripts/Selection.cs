using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : MonoBehaviour
{
	public List<ResourceDie> dice;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
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

			Debug.Log("Dice selected");
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
}
