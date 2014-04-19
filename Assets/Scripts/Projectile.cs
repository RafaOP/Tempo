using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
	private const float gravity = 0.1F;
	private const float speed = 10.0F;
	private Vector3 direction = Vector3.zero;
	//private int right = 0;

	public void setRight(int r) { direction.x = speed * r; }

	private float timeDestroy = 2.0f;
	private float currentTime = 0;
	
	void FixedUpdate ()
	{
		if (direction.x != 0)
		{
			currentTime += Time.deltaTime;
			
			if (currentTime >= timeDestroy)
				Destroy(gameObject);
			
			direction.y -= gravity;
			transform.position += direction * Time.deltaTime;
		}
	}
	
	// Why isn't it a collision when I jump in the upper part of the bullet? Figure it out!
	void OnCollisionEnter()
	{
		//Debug.Log("BOOM!");
		Transform.Destroy(gameObject);
	}
}