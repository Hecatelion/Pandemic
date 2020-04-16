using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Airplane : Interactible
{
	public City curCity;
	public int movementAllowed = 0;

	Text ui;

    void Start()
    {
		ui = GetComponentInChildren<Text>();
		ui.gameObject.SetActive(false);

		MoveTo(curCity);
    }

    void Update()
    {
        
    }

	private void OnMouseDown()
	{
		List<ResourceDie> planeDice = (from die in TheGameManager.instance.curPlayer.selection.dice where die.faceType == ResourceType.Plane select die).ToList();

		movementAllowed += planeDice.Count;

		ui.gameObject.SetActive(true);
		ui.text = movementAllowed.ToString();

		foreach (var die in planeDice)
		{
			die.PutOnCard();
		}

		TheGameManager.instance.curPlayer.selection.Flush();
	}

	public void MoveTo(City _city)
	{
		transform.position = _city.transform.position;
		curCity = _city;

		ui.gameObject.SetActive(false);
	}
}
