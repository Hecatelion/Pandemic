using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
	None = -1,
	NotStarted,
	Running,
	Win,
	Lose
}

public class TheGameManager : MonoBehaviour
{
	public static TheGameManager instance;

	[Header("GameParameters")]
	[SerializeField] public int token = 3;
	[SerializeField] public int nbPlayer = 2;
	[SerializeField] public int nbCityAtStart = 2;
	[SerializeField] public int nbCityLeft = 3;
	[SerializeField] public float timerDuration = 40f;
	[SerializeField] public float pauseDuration = 5f;

	public float timer = 0;
	public int nbCityToRescue = 999;

	[Header("UI")]
	[SerializeField] public Text playerNameUI;
	[SerializeField] public HQDisplayer hqDisplayer;

	[Header("Prog")]
	[SerializeField] public List<Player> players;

	public Player curPlayer;
	int curPlayerIndex = 0;

	public GameState gameState = GameState.NotStarted;
	bool isGamePaused = false;

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
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && gameState == GameState.NotStarted)
		{
			StartGame();
		}
        if (Input.GetKeyDown(KeyCode.Space) && gameState == GameState.Running)
		{
			NextTurn();
		}
        if (Input.GetKeyDown(KeyCode.KeypadPlus) && gameState == GameState.Running)
		{
			ActivateRandomCity();
		}

		if (gameState == GameState.Running && !isGamePaused)
		{
			timer -= Time.deltaTime;
			hqDisplayer.timerUI.text = ((int)timer).ToString();

			if (timer < 0)
			{
				if (token == 0)
				{
					Lose();
				}
				else
				{
					StartCoroutine(StartNewCycleIn(pauseDuration, 1));
				}
			}
		}
    }

	void Init()
	{
		token++;
		nbCityToRescue = nbCityAtStart + nbCityLeft;

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

		UISetActive(false);
	}

	public void StartGame()
	{
		Debug.Log("Game Strating !");
		gameState = GameState.Running;
		SetTurn(curPlayerIndex);

		StartCoroutine(StartNewCycleIn(3, nbCityAtStart));
		UISetActive(true);
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

		playerNameUI.text = curPlayer.name;
		playerNameUI.color = curPlayer.pawn.GetComponentInChildren<MeshRenderer>().material.color;
	}

	public void ActivateRandomCity()
	{
		TheCitiesManager.instance.ActivateRandomCity();
	}

	public IEnumerator StartNewCycleIn(float _pauseDuration, int _nbCity)
	{
		Debug.Log("new cycle in " + _pauseDuration + " sec");

		// pause
		SetPause(true);
		yield return new WaitForSeconds(_pauseDuration);
		SetPause(false);

		StartNewCycle(_nbCity);

		yield return null;
	}

	public void SetPause(bool _on)
	{
		// blackScreen.SetActive(_on)
		isGamePaused = _on;
	}

	public void StartNewCycle(int _nbCity)
	{ 
		for (int i = 0; i < _nbCity; i++)
		{
			ActivateRandomCity();
		}

		TurnNewSandtimer();

		Debug.Log("starting new cycle, nb token left : " + token);
	}

	public void TurnNewSandtimer()
	{
		token--;
		hqDisplayer.tokenUI.text = token.ToString();
		timer = timerDuration;
	}

	public void Lose()
	{
		gameState = GameState.Lose;
		Debug.Log("Lose ! (Not implemented yet");
	}

	public void Win()
	{
		gameState = GameState.Win;
		Debug.Log("Win ! (Not implemented yet");
	}

	public void UISetActive(bool _b)
	{
		hqDisplayer.gameObject.SetActive(_b);
		playerNameUI.gameObject.SetActive(_b);
	}
}
