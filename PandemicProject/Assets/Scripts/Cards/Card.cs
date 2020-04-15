using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : Interactible
{
	//public bool faceUp = true;
	[SerializeField] public CardType type = CardType.None;
	[SerializeField] public MeshRenderer meshRenderer;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
