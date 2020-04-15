using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// pattern Singleton
public class DiceThrower : MonoBehaviour
{
	public static DiceThrower instance;

	[SerializeField] public GameObject box;
	[SerializeField] public float throwForce = 5.0f;

	List<ResourceDie> dice = new List<ResourceDie>();

	public DiceCallback onThrowEnd = (dice) => { };

    void Start()
    {
        if (instance == null)
		{
			instance = this;
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
		ThrowSelection();
	}

	public void Throw(List<ResourceDie> _dice, bool _consumeDice = true)
	{
		GetDice(_dice);

		foreach (var die in dice)
		{
			die.AssignTo(box.transform);
			die.AllowPhysics();
			die.Lock();

			die.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * throwForce);
			die.isStable = false;
		}

		StartCoroutine(WaitForResult(_consumeDice));
	}

	public void ThrowSelection()
	{
		Selection selection = TheGameManager.instance.curPlayer.selection;
		selection.TakeDiceOutOfHand();
		Throw(selection.dice, false);
		selection.Flush();
	}

	public void Empty()
	{
		dice.Clear();
	}

	public IEnumerator WaitForResult(bool _consumeDice)
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

		onThrowEnd(dice);

		if (_consumeDice)
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
}
