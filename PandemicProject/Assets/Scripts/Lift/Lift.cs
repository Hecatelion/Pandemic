﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Lift : MonoBehaviour
{
	[SerializeField] ResourceType type = ResourceType.None;

	[SerializeField] CargoRoom cargo;

	Text ui;

	public int sendingAmount = 0;
	public bool isUsable = false;
	public bool isFree = true;
	public Supply liftedSupply;

	public Callback onDeactivation = () => { };

	void Start()
    {
		ui = GetComponentInChildren<Text>();
		ui.gameObject.SetActive(false);
		cargo = FindObjectOfType<CargoRoom>();
    }

    void Update()
    { }

	private void OnMouseDown()
	{
		TryCatchingSupply(GetAllFreeSuitableSupplies()[0]);
	}

	public void TryCatchingSupply(Supply _supply)
	{
		if (_supply.type == type && isUsable && isFree)
		{
			CatchSupply(_supply);
		}
	}

	public void CatchSupply(Supply _supply)
	{
		liftedSupply = _supply;
		liftedSupply.transform.position = this.transform.position;
		liftedSupply.transform.rotation = this.transform.rotation;
		liftedSupply.AllowPhysics(false);

		liftedSupply.Lock();

		isFree = false;
	}

	public void TrySendingSupply()
	{
		if (isUsable && !isFree)
		{
			SendSupply();
		}
	}

	public void SendSupply()
	{
		// supply sending
		cargo.ReceiveSupply(liftedSupply);

		// lift emptying
		liftedSupply = null;
		isFree = true;
		sendingAmount--;
		ui.text = sendingAmount.ToString();

		if (sendingAmount == 0)
		{
			Deactivate();
		}
	}

	public void Activate(int _sendingAmount)
	{
		sendingAmount = Mathf.Min(Mathf.Min(_sendingAmount, GetAllFreeSuitableSupplies().Count), cargo.bay.GetFreeSlotAmount());

		isUsable = true;
		ui.gameObject.SetActive(true);
		ui.text = sendingAmount.ToString();

		Debug.Log("Lift Activated");
	}

	public void Deactivate()
	{
		isUsable = false;
		ui.gameObject.SetActive(false);

		Debug.Log("Lift Deactivated");
		onDeactivation();
	}

	List<Supply> GetAllFreeSuitableSupplies()
	{
		// all supplies of this lift type
		Supply[] supplies = (from supply in FindObjectsOfType<Supply>() where supply.type == type select supply).ToArray();

		List<Supply> validSupplies = new List<Supply>();

		foreach (var supply in supplies)
		{
			if (supply.curRoom == supply.homeRoom)
			{
				validSupplies.Add(supply);
			}
		}

		return validSupplies;
	}
}
