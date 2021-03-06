﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// pattern Singleton
public class DiceThrower : MonoBehaviour
{
	public static DiceThrower instance;

	[SerializeField] public GameObject box;
	[SerializeField] public float throwForce = 5.0f;
	[SerializeField] public float torqueForce = 5.0f;

	List<ResourceDie> dice = new List<ResourceDie>();

	Text ui;

	public DiceCallback onThrowEnd = (dice) => { };

    void Start()
    {
        if (instance == null)
		{
			instance = this;

			ui = GetComponentInChildren<Text>();
		}
		else
		{
			Destroy(this.gameObject);
		}
    }

    void Update()
    {
        
    }

	private void OnMouseDown()
	{
		if (TheGameManager.instance.curPlayer.nbThrow > 0 && TheGameManager.instance.curPlayer.selection.dice.Count > 0)
		{
			TheGameManager.instance.curPlayer.nbThrow--;
			ThrowSelection();
		}
	}

	public void Throw(List<ResourceDie> _dice, bool _returnToOwner = false)
	{
		GetDice(_dice);

		foreach (var die in dice)
		{
			die.AssignTo(box.transform);
			die.AllowPhysics();
			die.Lock();

			// rand pos, force, torque
			die.transform.localPosition = new Vector3(Random.Range(-4f, 4f), Random.Range(-1.5f, 1.5f), Random.Range(-4f, 4f));
			die.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * throwForce);
			die.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * torqueForce);

			die.isStable = false;
		}

		StartCoroutine(WaitForResult(_returnToOwner));

		ui.gameObject.SetActive(false);
	}

	public void ThrowSelection()
	{
		Selection selection = TheGameManager.instance.curPlayer.selection;
		selection.TakeDiceOutOfHand();
		Throw(selection.dice, true);
		selection.Flush();

		ui.text = TheGameManager.instance.curPlayer.nbThrow.ToString();
	}

	public void Empty()
	{
		dice.Clear();
	}

	public IEnumerator WaitForResult(bool _returnToOwner)
	{
		bool areDiceStable = false;

		yield return new WaitForSeconds(0.2f);

		while (!areDiceStable)
		{
			yield return new WaitForSeconds(0.1f);

			areDiceStable = true;

			foreach (var die in dice)
			{
				if (!die.isStable)
				{
					areDiceStable = false;
					break;
				}
			}
		}

		ui.gameObject.SetActive(true);

		RedressDice();
		onThrowEnd(dice);

		if (!_returnToOwner)
		{
			foreach (var die in dice)
			{
				die.PutOnCard();
			}
		}
		else
		{
			foreach (var die in dice)
			{
				die.ReturnToOwner();
			}
		}

		Empty();

		yield return null;
	}

	void GetDice(List<ResourceDie> _dice)
	{
		foreach (var die in _dice)
		{
			dice.Add(die);
		}
	}

	void RedressDice()
	{
		foreach (var die in dice)
		{
			die.Redress();
		}
	}
}
