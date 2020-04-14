using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDie : PhysicsInteractible, IPickable
{
	public Player owner;
	public ResourceType faceType = ResourceType.None;
	public bool isSelected = false;
	public bool isStable = false;

	Rigidbody rb;

	//public Callback onStabilise = () => { };

	void Start()
	{
		owner = transform.parent.GetComponent<Player>();
		rb = GetComponent<Rigidbody>();

		ReturnToOwner();

		SetTypeFromFace();
	}

	void Update()
	{
		if (!isStable && rb.IsSleeping())
		{
			SetTypeFromFace();
			isStable = true;

			//onStabilise();
		}
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

	public void ReturnToOwner()
	{
		AssignTo(owner.transform);
		AllowPhysics(false);
		Unlock();
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
	}

	public static ResourceType GetTypeFromTransform(Transform _dieTransform) // must be tested
	{
		Vector3 dieForward = _dieTransform.forward;
		Vector3 dieUp = _dieTransform.up;
		Vector3 dieRight = _dieTransform.right;

		if (dieForward == Vector3.up) // front face
		{
			return ResourceType.Vaccine;
		} 
		else if (dieForward == -Vector3.up) // back face
		{
			return ResourceType.Food;
		}
		else if (dieUp == Vector3.up) // top face
		{
			return ResourceType.Power;
		}
		else if (dieUp == -Vector3.up) // bot face
		{
			return ResourceType.FirstAid;
		}
		else if (dieRight == Vector3.up) // right face
		{
			return ResourceType.Water;
		}
		else if (dieRight == -Vector3.up) // left face
		{
			return ResourceType.Plane;
		}
		else
		{
			return ResourceType.None;
		}
	}
}
