﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcePlace : MonoBehaviour
{
	public bool isFull;
	public ResourceDie die;

	void Start()
    { }
	
    void Update()
    { }

	public void Fill(ResourceDie _die)
	{
		isFull = true;

		die = _die;
		die.Lock();
	}

	public void Empty()
	{
		isFull = false;

		die.Unlock();
		die = null;
	}
}