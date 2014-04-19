using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour {
	
	private float distanceToWalk = 3;
	private float speedWalk = 3;

	private Vector3 rightDirection;
	private Vector3 leftDirection;
	private Vector3 startPosition;
	private Vector3 finalPosition;
	private bool inLeftDirection = true;
	private bool isBack = true;

	private bool isFollowing = false;
	private float initialPosition;
	private float followRadius = 11.5f;
	private float triggerRadius = 7.2f;

	private int life = 3;

	private Player player;

	void Start () {
		
		startPosition = transform.position;
		finalPosition.x = startPosition.x;
		finalPosition.x -= distanceToWalk;

		leftDirection = transform.localScale;
		rightDirection = leftDirection;
		rightDirection.x *= -1;

		player = GameObject.Find ("Player").GetComponent<Player> ();

		initialPosition = transform.position.x;

	}
	

	void Update () {

		if(GameObject.Find ("Player").transform.position.x > initialPosition - triggerRadius - (distanceToWalk/2) &&
		   GameObject.Find ("Player").transform.position.x < initialPosition + triggerRadius - (distanceToWalk/2) ) {
			isFollowing = true;
		}
		if(GameObject.Find ("Player").transform.position.x < initialPosition - followRadius - (distanceToWalk/2) ||
		   GameObject.Find ("Player").transform.position.x > initialPosition + followRadius - (distanceToWalk/2)) {
			isFollowing = false;
		}

		if(isFollowing){
			if (transform.position.x < player.transform.position.x) {
				transform.localScale = rightDirection;
				transform.Translate (Vector3.right * Time.deltaTime * speedWalk);
			}
			else {
				transform.localScale = leftDirection;
				transform.Translate (Vector3.left * Time.deltaTime * speedWalk);
			}
		}
		if(isFollowing == false) {
			if (inLeftDirection && isBack) {
				transform.Translate (Vector3.left * Time.deltaTime * speedWalk);
				transform.localScale = leftDirection;
			}
			else {
				transform.Translate (Vector3.right * Time.deltaTime * speedWalk);
				transform.localScale = rightDirection;
			}
			if (transform.position.x > finalPosition.x)
				inLeftDirection = true;
			else {
				inLeftDirection = false;
				isBack = false;
			}
			if (transform.position.x > startPosition.x)
				isBack = true;
		}
	}
	
	void OnTriggerEnter (Collider hit)
	{
		if (hit.gameObject.tag == "Player") {
			player.death ();
		}
	}

	void OnCollisionEnter (Collision hit)
	{
		if (hit.transform.tag == "Projectil") {
			life -= 1;
			if (life <= 0)
				Destroy (gameObject);

			if (GameObject.Find ("Player").transform.position.x < transform.position.x)
				transform.position = new Vector3 (transform.position.x + 1.0f, transform.position.y + 0.25f, 0.0f);
			else
				transform.position = new Vector3 (transform.position.x - 1.0f, transform.position.y + 0.25f, 0.0f);
		}

	}
}