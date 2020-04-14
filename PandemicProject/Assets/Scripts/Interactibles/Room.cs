using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	[SerializeField] public RoomType roomType = RoomType.None;
	[SerializeField] Room[] connectedRooms = new Room[0];

	void Start()
    {
		//ConnectionTestLog();
	}

    void Update()
    { }

	void ConnectionTestLog()
	{
		string log = roomType + " connected to : ";
		foreach (var room in connectedRooms)
		{
			log += room.roomType + ", ";
		}

		Debug.Log(log);
	}
}
