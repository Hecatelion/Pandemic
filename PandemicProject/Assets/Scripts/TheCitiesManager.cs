using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// pattern Singleton
public class TheCitiesManager : MonoBehaviour
{
	public static TheCitiesManager instance;
	City[] cities;

	void Start()
	{
		if (instance == null)
		{
			instance = this;

			cities = FindObjectsOfType<City>();
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	void Update()
    {
        
    }

	public City GetRandomCityInState(CityState _state)
	{
		City[] validCities = (from city in cities where city.state == _state select city).ToArray();

		if (validCities.Count() > 0)
		{
			int randomIndex = Random.Range(0, validCities.Count());
			return validCities[randomIndex];
		}
		else
		{
			Debug.Log("no city in state : " + _state);
			return null;
		}
	}

	public void ActivateRandomCity()
	{
		City waitingCity = GetRandomCityInState(CityState.Waiting);
		if (waitingCity != null)	waitingCity.Activate();
	}
}
