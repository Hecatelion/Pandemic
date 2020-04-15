using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Airplane : MonoBehaviour
{
	public City curCity;
	public int movementAllowed = 0;

    void Start()
    {
		MoveTo(curCity);
    }

    void Update()
    {
        
    }

	private void OnMouseDown()
	{
		List<ResourceDie> planeDice = (from die in TheGameManager.instance.curPlayer.selection.dice where die.faceType == ResourceType.Plane select die).ToList();

		movementAllowed += planeDice.Count;
		Debug.Log("Airplane's pawn movement allowed : " + movementAllowed);

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
	}
}
