using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoleCard : Card
{
	[SerializeField] public RoomType spawingRoomType;
	PlayerPawn playerPawn;

    void Start()
    {
		type = CardType.Role;
		playerPawn = FindObjectOfType<PlayerPawn>();
    }

    void Update()
    {

	}
}
