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
	public MeshRenderer meshRenderer;
	public Material baseMat;

	//public Callback onStabilise = () => { };

	void Start()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		baseMat = meshRenderer.material;
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
		faceType = GetTypeFromTransform_V2(transform);
	}
	/*
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
	*/
	public static ResourceType GetTypeFromTransform_V2(Transform _dieTransform) // must be tested
	{
		Vector3 dieForward = _dieTransform.forward;
		Vector3 dieUp = _dieTransform.up;
		Vector3 dieRight = _dieTransform.right;

		ResourceType type = ResourceType.None;
		float angle = 1000f;
		float testedAngle = 1000f;

		// front face
		testedAngle = Mathf.Abs(Vector3.Angle(dieForward, Vector3.up)); 
		if (testedAngle < angle)
		{
			angle = testedAngle;
			type = ResourceType.Vaccine;
		}

		// back face
		testedAngle = Mathf.Abs(Vector3.Angle(dieForward, Vector3.down)); 
		if (testedAngle < angle)
		{
			angle = testedAngle;
			type = ResourceType.Food;
		}

		// top face
		testedAngle = Mathf.Abs(Vector3.Angle(dieUp, Vector3.up)); 
		if (testedAngle < angle)
		{
			angle = testedAngle;
			type = ResourceType.Power;
		}

		// bot face
		testedAngle = Mathf.Abs(Vector3.Angle(dieUp, Vector3.down)); 
		if (testedAngle < angle)
		{
			angle = testedAngle;
			type = ResourceType.FirstAid;
		}

		// right face
		testedAngle = Mathf.Abs(Vector3.Angle(dieRight, Vector3.up)); 
		if (testedAngle < angle)
		{
			angle = testedAngle;
			type = ResourceType.Water;
		}

		// left face
		testedAngle = Mathf.Abs(Vector3.Angle(dieRight, Vector3.down)); 
		if (testedAngle < angle)
		{
			angle = testedAngle;
			type = ResourceType.Plane;
		}

		// none
		if (type == ResourceType.None) Debug.LogError("can't tell cur face type");

		return type;
	}

	public void Redress()
	{
		transform.rotation = Quaternion.identity;

		switch (faceType)
		{
			case ResourceType.Vaccine:		transform.forward = Vector3.up;			break;
			case ResourceType.Food:			transform.forward = Vector3.down;		break;
			case ResourceType.Power:		transform.up	  = Vector3.up;			break;
			case ResourceType.FirstAid:		transform.up	  = Vector3.down;		break;
			case ResourceType.Water:		transform.right   = Vector3.up;			break;
			case ResourceType.Plane:		transform.right   = Vector3.down;		break;

			case ResourceType.None:			Debug.LogError("this die doesn't have face type.");		break;
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
