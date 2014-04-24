using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    private float xspeed = 8.0F;                // X speed when walking
    private float yspeed = 7.0F;               // Y speed when jumping  10.0
    private float xyspeed = 6.0F;               // X speed when jumping
    private const float gravity = 30.0F;        // -Y acceleration when jumping/falling
    private Vector3 direction = Vector3.zero;   // Actual direction
    private int life;                           // When life reaches 0, the player "faints"

    private float prevTime;                     // Control variable that will only allow the
                                                // player to shoot once in x seconds.
    private float shootTime = 0.5F;             // Time between shots

    private Vector3 checkpoint;                 // The last place the player has been to

    public GameObject bullet;                   // The ultimate bullet

    private Vector3 rightDirection;             // The direction the player is looking

	//
	private int stones;							// The number of bullets the player can shoot

    void Start()
    {
        setLife(1);
        prevTime = Time.time;
        rightDirection = transform.localScale;
		stones = 2;
    }

    // Controls the movement of the play using CharacterController.
    void FixedUpdate()
    {
        move();

        if (Input.GetButtonDown("Fire1")) { shoot(); }

        // If he falls, he dies.
        if (transform.position.y < -3) death();
    }

    public int getLife() { return life; }
    public void setLife(int l) { life = l; }

    void move()
    {
        CharacterController controller = GetComponent<CharacterController>();
        direction.x = Input.GetAxis("Horizontal");

        if (direction.x > 0)
            transform.localScale = rightDirection;
        if (direction.x < 0)
            transform.localScale = new Vector3(rightDirection.x, rightDirection.y, -rightDirection.z);

        if (controller.isGrounded)
        {
            direction.y = 0;
            direction.x *= xspeed;
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
            {
                direction.y = yspeed;
            }
        }
        else
        {
            direction.x *= xyspeed;
            direction.y -= gravity * Time.deltaTime;
        }

        transform.TransformDirection(direction);
        controller.Move(direction * Time.deltaTime);
    }

    void shoot()
    {
		if (Time.time >= prevTime + shootTime && stones > 0)
        {
            prevTime = Time.time;
            Vector3 projPos = GameObject.Find("ProjectilPosition").transform.position;
            GameObject projectile = (GameObject)Instantiate(bullet, projPos, transform.rotation);
			projectile.GetComponent<Projectile>().setRight (transform.localScale.z == 7 ? 1 : -1);
			stones -= 1;
			//Debug.Log ("Shot shot shot");
        }
    }

    public int damage(int d)
    {
        setLife(getLife() - d);
        if (life - d <= 0)
        {
            death();
            setLife(1);
        }

        return getLife();
    }

    public void death()
    {
        transform.position = checkpoint;
        print(transform.position);
    }

    public void setCheckpoint(Vector3 pos) { checkpoint = pos; }
    public void setCheckpoint(float x, float y, float z)
    {
        checkpoint.x = x;
        checkpoint.y = y;
        checkpoint.z = z;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            death();
        }
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Projectile") {
			stones += 1;
			Destroy(other.gameObject);
			Debug.Log("Stone picked up");
		}
		if (other.gameObject.tag == "BPortalA")
		{
			transform.position = new Vector3 (GameObject.Find ("EPortalA").transform.position.x, transform.position.y, transform.position.z);
		}
		if (other.gameObject.tag == "BPortalB")
		{
			transform.position = new Vector3 (GameObject.Find ("EPortalB").transform.position.x, transform.position.y, transform.position.z);
		}
	}
}