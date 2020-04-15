using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleCard : Card
{
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
