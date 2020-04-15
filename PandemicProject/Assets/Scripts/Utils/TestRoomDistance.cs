using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomDistance : MonoBehaviour
{
	[SerializeField] public Room roomA;
	[SerializeField] public Room roomB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Dist : " + roomA.DistanceWith(roomB));
		}
    }
}
