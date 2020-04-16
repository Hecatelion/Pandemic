using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum CityState
{
	None = -1,
	Waiting,
	NeedsHelp,
	Rescued,
	Any,
	Count
}

public enum CityCardPos
{
	None = -1,
	Bottom, 
	Top,
	Left, 
	Right
}

public class City : MonoBehaviour
{
	//[SerializeField] public string name = "default_name";
	[SerializeField] public City previous;
	[SerializeField] public City next;

	[SerializeField] public ResourceType[] resourcesNeeded = new ResourceType[0];

	public int[] resourcesNeededAmount = new int[5] { 0,0,0,0,0 };

	public CityState state = CityState.Waiting;

	[SerializeField] public CityCardPos cardPos = CityCardPos.None;
	[SerializeField] public Material cardMat;
	[SerializeField] public GameObject cityCardPrefab;
	[SerializeField] public CityCard card;

	Airplane airplane;

    void Start()
    {
		airplane = FindObjectOfType<Airplane>();
		resourcesNeededAmount = GetResourceAmount(resourcesNeeded);
    }

    void Update()
    {
        
    }

	private void OnMouseDown()
	{
		int dist = DistanceWith(airplane.curCity);
		if (dist > 0 && dist <= airplane.movementAllowed)
		{
			airplane.MoveTo(this);
			airplane.movementAllowed = 0;

			Debug.Log("Airplane movement allowed : " + airplane.movementAllowed);
		}
	}

	int[] GetResourceAmount(ResourceType[] _resources)
	{
		int[] resourcesAmount = new int[5] { 0, 0, 0, 0, 0 } ;

		foreach (var res in _resources)
		{
			resourcesAmount[(int)res]++;
		}

		return resourcesAmount;
	}

	public void Activate()
	{
		card = Instantiate(cityCardPrefab, transform).GetComponent<CityCard>();
		card.meshRenderer.material = cardMat;
		switch (cardPos)
		{
			case CityCardPos.None:
				Debug.Log("City.cardPos not set.");
				break;
			case CityCardPos.Bottom:
				card.transform.localPosition = Vector3.right * 2f + Vector3.back * 4.5f ;
				break;
			case CityCardPos.Top:
				card.transform.localPosition = Vector3.right * 2f + Vector3.forward * 4.5f;
				break;
			case CityCardPos.Left:
				card.transform.localPosition = Vector3.left * 4.5f + Vector3.back * 2f;
				card.transform.localRotation = Quaternion.Euler(0, 90f, 0);
				break;
			case CityCardPos.Right:
				card.transform.localPosition = Vector3.right * 4.5f + Vector3.forward * 2f;
				card.transform.localRotation = Quaternion.Euler(0, -90f, 0);
				break;
			default:
				break;
		}

		state = CityState.NeedsHelp;
	}

	public bool CanBeRescued(List<Supply> _supplies)
	{
		ResourceType[] resources = (from supply in _supplies select supply.type).ToArray();
		int[] resourcesAmount = GetResourceAmount(resources);

		for (int i = 0; i < 5; i++)
		{
			if (resourcesAmount[i] < resourcesNeededAmount[i])
			{
				return false;
			}
		}

		return true;
	}

	public void Rescue()
	{
		Destroy(card.gameObject);
		state = CityState.Rescued;
		TheGameManager.instance.nbCityToRescue--;
		TheGameManager.instance.token++;

		if (TheGameManager.instance.nbCityToRescue < 1)
		{
			TheGameManager.instance.Win();
		}
	}

	int DistanceWith(City _city)
	{
		// w/ next
		City testedCity = this;
		int distWithNext = 0;
		while (testedCity != _city && distWithNext < 100)
		{
			testedCity = testedCity.next;
			distWithNext++;
		}

		// w/ previous
		testedCity = this;
		int distWithPrev = 0;
		while (testedCity != _city && distWithPrev < 100)
		{
			testedCity = testedCity.previous;
			distWithPrev++;
		}

		if (distWithNext > 99 || distWithPrev > 99) Debug.LogError("DistanceBetween(" + airplane.curCity + ", " + _city + ") > 99.");

		return (distWithPrev < distWithNext) ? distWithPrev : distWithNext;
	}
}
