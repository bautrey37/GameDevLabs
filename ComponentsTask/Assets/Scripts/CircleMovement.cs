using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
	
	void Update () {
		//Todo Implement circular movement here
	    transform.localPosition = new Vector3(Mathf.PingPong(Time.time,5f) - 2.5f, 1f, 0f);
	}

}
