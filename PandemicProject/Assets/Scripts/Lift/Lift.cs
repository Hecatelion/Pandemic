using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Lift : MonoBehaviour
{
	[SerializeField] ResourceType type = ResourceType.None;

	[SerializeField] CargoRoom cargo;

	public int sendingAmount = 0;
	public bool isUsable = false;
	public bool isFree = true;
	public Supply liftedSupply;

	public Callback onDeactivation = () => { };

	void Start()
    {
        
    }

    void Update()
    { }

	private void OnMouseDown()
	{
		Supply[] supplies = GameObject.FindObjectsOfType<Supply>();
		TryCatchingSupply((from supply in supplies where supply.type == type select supply).ToArray()[0]);
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
		liftedSupply.SendToRoom(cargo);

		// lift emptying
		liftedSupply = null;
		isFree = true;
		sendingAmount--;

		if (sendingAmount == 0)
		{
			Deactivate();
		}
	}

	public void Activate(int _sendingAmount)
	{
		sendingAmount = _sendingAmount;

		isUsable = true;

		Debug.Log("Lift Activated");
	}

	public void Deactivate()
	{
		isUsable = false;

		Debug.Log("Lift Deactivated");
		onDeactivation();
	}
}
