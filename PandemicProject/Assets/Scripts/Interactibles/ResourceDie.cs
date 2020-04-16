using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDie : PhysicsInteractible, IPickable
{
	public Player owner;
	public ResourceType faceType = ResourceType.None;
	public bool isSelected = false;
	public bool isStable = false;

	public bool isOnCard = false;

	public bool hasBeenUsedThisTurn = false;

	float sleepingTimeToBeStable = 1f;
	float sleepingTime = 0f;

	Rigidbody rb;

	//public Callback onStabilise = () => { };

	void Start()
	{
		owner = transform.parent.GetComponent<Player>();
		rb = GetComponent<Rigidbody>();

		PutOnCard();

		SetTypeFromFace();
	}

	void Update()
	{
		SleepCheck();
	}

	public void OnMouseDown()
	{
		if (isSelected)
		{
			owner.selection.Unselect(this);
		}
		else
		{
			owner.selection.Select(this);
		}
	}

	void IPickable.Pick()
	{
		// pick
	}

	void SleepCheck()
	{
		if (!isStable)
		{
			if (sleepingTime > sleepingTimeToBeStable)
			{
				SetTypeFromFace();
				isStable = true;
				sleepingTime = 0f;
			}
			else
			{
				if (rb.IsSleeping())
				{
					sleepingTime += Time.deltaTime;
				}
			}
			
		}
	}

	public void TakeOutOfHand()
	{
		owner.hand.Release(this);
	}

	public void ReturnToOwner()
	{
		isOnCard = false;
		owner.hand.Catch(this);
	}

	public void SetRandomFace()
	{
		int i = Random.Range(0, 6);

		switch (i)
		{
			case 0:		transform.forward =  Vector3.up;	break;
			case 1:		transform.forward = -Vector3.up;	break;
			case 2:		transform.up	  =  Vector3.up;	break;
			case 3:		transform.up	  = -Vector3.up;	break;
			case 4:		transform.right   =  Vector3.up;	break;
			case 5:		transform.right   = -Vector3.up;	break;

			default:	break;
		}

		SetTypeFromFace();
	}

	public void SetTypeFromFace()
	{
		faceType = GetTypeFromTransform(transform);
		//Debug.Log("face : " + faceType);
	}

	public static ResourceType GetTypeFromTransform(Transform _dieTransform) // must be tested
	{
		Vector3 dieForward = _dieTransform.forward;
		Vector3 dieUp = _dieTransform.up;
		Vector3 dieRight = _dieTransform.right;

		if (CustomMaths.Approximately(dieForward, Vector3.up)) // front face
		{
			return ResourceType.Vaccine;
		} 
		else if (CustomMaths.Approximately(dieForward, - Vector3.up)) // back face
		{
			return ResourceType.Food;
		}
		else if (CustomMaths.Approximately(dieUp, Vector3.up)) // top face
		{
			return ResourceType.Power;
		}
		else if (CustomMaths.Approximately(dieUp, -Vector3.up)) // bot face
		{
			return ResourceType.FirstAid;
		}
		else if (CustomMaths.Approximately(dieRight, Vector3.up)) // right face
		{
			return ResourceType.Water;
		}
		else if (CustomMaths.Approximately(dieRight, -Vector3.up)) // left face
		{
			return ResourceType.Plane;
		}
		else
		{
			Debug.Log("no face, forward : " + dieForward);
			return ResourceType.None;
		}
	}

	public void SetUsedThisTurn()
	{
		hasBeenUsedThisTurn = true;
	}

	public void PutOnCard()
	{
		isOnCard = true;
		AssignTo(owner.card.transform);
		AllowPhysics(false);
		Lock();
	}
}
