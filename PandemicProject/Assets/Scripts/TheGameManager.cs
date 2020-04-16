using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

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
	[SerializeField] public Button selectionButton;
	[SerializeField] public Button turnButton;
	[SerializeField] public GameObject pauseScreen;
	[SerializeField] public GameObject winScreen;
	[SerializeField] public GameObject loseScreen;

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

			StartGame();
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && gameState == GameState.Running)
		{
			Win();
		}
        if (Input.GetKeyDown(KeyCode.Space) && gameState == GameState.Running)
		{
			Lose();
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

		// players management init
		nbPlayer = TheSettings.instance.nbPlayer;

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

		// UI
		UISetActive(false);
	}

	public void StartGame()
	{
		Debug.Log("Game Strating !");
		gameState = GameState.Running;

		StartCoroutine(StartNewCycleIn(3, nbCityAtStart, true));
		UISetActive(true);
	}

	public void NextTurn()
	{
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

	public IEnumerator StartNewCycleIn(float _pauseDuration, int _nbCity, bool _isFirstTurn = false)
	{
		// pause
		SetPause(true);
		yield return new WaitForSeconds(_pauseDuration);
		SetPause(false);

		// starting cycle
		StartNewCycle(_nbCity);

		if (_isFirstTurn)
		{
			yield return new WaitForSeconds(1);

			SetTurn(curPlayerIndex);
		}

		yield return null;
	}

	public void SetPause(bool _on)
	{
		isGamePaused = _on;
		pauseScreen.SetActive(_on);
	}

	public void StartNewCycle(int _nbCity)
	{ 
		for (int i = 0; i < _nbCity; i++)
		{
			ActivateRandomCity();
		}

		TurnNewSandtimer();
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
		loseScreen.SetActive(true);

		StartCoroutine(GoToMenuIn(2));
	}

	public void Win()
	{
		gameState = GameState.Win;
		winScreen.SetActive(true);

		StartCoroutine(GoToMenuIn(2));
	}

	public void UISetActive(bool _b)
	{
		hqDisplayer.gameObject.SetActive(_b);
		playerNameUI.gameObject.SetActive(_b);
		selectionButton.gameObject.SetActive(_b);
		turnButton.gameObject.SetActive(_b);
	}

	public void UI_EndTurnButton()
	{
		NextTurn();
	}

	public void UI_SelectionButton()
	{
		if (UIButtonShouldSelect())
		{
			curPlayer.selection.SelectAllDice();
		}
		else
		{
			curPlayer.selection.Flush();
		}
	}

	public bool UIButtonShouldSelect()
	{
		ResourceDie[] handDice = curPlayer.hand.GetDice();

		ResourceDie[] handDiceNotSelected = (from die in handDice where !die.isSelected select die).ToArray();

		return handDiceNotSelected.Count() > 0;
	}

	public void UI_GoToMenu()
	{
		SceneManager.LoadScene(0);
	}

	IEnumerator GoToMenuIn(float _duration)
	{
		yield return new WaitForSeconds(_duration);

		SceneManager.LoadScene(0);
		yield return null;
	}
}
