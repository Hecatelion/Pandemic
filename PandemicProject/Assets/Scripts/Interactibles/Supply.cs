using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Supply : PhysicsInteractible, IPickable
{
	public SupplyRoom homeRoom;
	public Room curRoom;
	[SerializeField] public ResourceType type = ResourceType.None;

    void Start()
    {
		SupplyRoom[] rooms = GameObject.FindObjectsOfType<SupplyRoom>();
		homeRoom = (from room in rooms where room.resourceType == type select room).ToArray()[0];
		curRoom = homeRoom;
 }

    void Update()
    { }

	void IPickable.Pick()
	{
		// pick
	}

	public void SendToRoom(Room _room)
	{
		curRoom = _room;

		AssignTo(curRoom.transform);
		AllowPhysics(true);
		Unlock();
	}

	public void ReturnToHomeRoom()
	{
		SendToRoom(homeRoom);
	}
}
