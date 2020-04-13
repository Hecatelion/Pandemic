using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
	[SerializeField] RoomType type = RoomType.None;
	[SerializeField] Room[] connectedRooms = new Room[0];

	void Start()
    {
		//ConnectionTestLog();
	}

    void Update()
    { }

	void ConnectionTestLog()
	{
		string log = type + " connected to : ";
		foreach (var room in connectedRooms)
		{
			log += room.type + ", ";
		}

		Debug.Log(log);
	}
}
