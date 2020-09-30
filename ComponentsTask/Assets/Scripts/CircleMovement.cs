using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
	float timeCounter = 0;
	float speed = 2;
	float diameter = 5;

	void Update () {
		//Todo Implement circular movement here
		timeCounter += Time.deltaTime * speed;

		float x = Mathf.Cos(timeCounter) * 2 - 1;
		float y = 1f;
		float z = Mathf.Sin(timeCounter)* 2 - 1;

	    transform.localPosition = new Vector3(x, y, z);
	}

}
