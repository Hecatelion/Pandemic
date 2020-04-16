﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

	private void OnMouseDown()
	{
		PlayerPawn curPawn = TheGameManager.instance.curPlayer.pawn;

		int dist = DistanceWith(curPawn.curRoom);
		if (dist > 0 && dist <= curPawn.movementAllowed)
		{
			curPawn.MoveTo(this);
			curPawn.movementAllowed = 0;

			Debug.Log("Player pawn movement allowed : " + curPawn.movementAllowed);
		}
	}

	void ConnectionTestLog()
	{
		string log = roomType + " connected to : ";
		foreach (var room in connectedRooms)
		{
			log += room.roomType + ", ";
		}

		Debug.Log(log);
	}

	// A*
	public int DistanceWith(Room _room)
	{
		List<Room> testedRooms = new List<Room>();
		List<Room> roomsToTest = new List<Room>();
		int dist = 0;

		roomsToTest.Add(this);

		while (!roomsToTest.Contains(_room))
		{
			dist++;

			testedRooms.AddRange(roomsToTest);
			roomsToTest.Clear();

			foreach (var testedRoom in testedRooms)
			{
				foreach (var connectedRoom in testedRoom.connectedRooms)
				{
					if (!testedRooms.Contains(connectedRoom) && !roomsToTest.Contains(connectedRoom))
					{
						roomsToTest.Add(connectedRoom);
					}
				}
			}
		}

		return dist;
	}

	public static Room FindRoomOfType(RoomType _type)
	{
		Room[] rooms = FindObjectsOfType<Room>();
		return (from room in rooms where room.roomType == _type select room).ToArray()[0];
	}
}
