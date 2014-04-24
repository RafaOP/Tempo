using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
	
	public float distanceToMove = 3.0f;
	private float center;
	private float degree;
	public float angularSpeed;
	public float sign;

	void Start () {
		center = transform.position.x;
		degree = 0.0f;
		angularSpeed = 1.0f;
	}

	void Update () {
		transform.position = new Vector3 (center + (distanceToMove * (sign * Mathf.Sin ((Mathf.PI/180) * degree))), transform.position.y, transform.position.z);
		degree += angularSpeed;
		if (degree >= 360.0f)
			degree = 0.0f;
	}
}
