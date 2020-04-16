using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGameManager : MonoBehaviour
{
	public static TheGameManager instance;

	public int nbPlayer = 2;
	[SerializeField] public List<Player> players;
	public Player curPlayer;
	int curPlayerIndex = 0;
	bool hasGameStarted = false;

	void Start()
	{
		if (instance == null)
		{
			instance = this;

			Init();
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && !hasGameStarted)
		{
			StartGame();
		}
        if (Input.GetKeyDown(KeyCode.Space) && hasGameStarted)
		{
			NextTurn();
		}
        if (Input.GetKeyDown(KeyCode.KeypadPlus) && hasGameStarted)
		{
			TheCitiesManager.instance.ActivateRandomCity();
		}
    }

	void Init()
	{
		List<Player> participatingPlayers = new List<Player>();

		for (int i = 0; i < players.Count; i++)
		{
			if (i < nbPlayer)
			{
				participatingPlayers.Add(players[i]);
			}
			else
			{
				Destroy(players[i].gameObject);
			}
		}

		players.Clear();
		players = participatingPlayers;
	}

	public void StartGame()
	{
		Debug.Log("Game Strating !");
		hasGameStarted = true;
		SetTurn(curPlayerIndex);
	}

	public void NextTurn()
	{
		Debug.Log("Next Turn !");
		curPlayer.EndTurn();
		curPlayerIndex++;
		if (curPlayerIndex == nbPlayer) curPlayerIndex = 0;

		SetTurn(curPlayerIndex);
	}

	public void SetTurn(int _index)
	{
		curPlayer = players[_index];
		curPlayer.StartTurn();
	}

	public void ActivateCity()
	{

	}
}
