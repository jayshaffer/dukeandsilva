using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public int speed;
    public int maxSpeed;
    public float fireDelay;
    public float maxShotDistance;
    public LayerMask jumpCheckMask;
    public Animator anim;
    public int jumpPower;
    public int jumpHash = Animator.StringToHash("Jump");
    public float jumpMargin;
    public AudioSource walkingAudio;
    public GameObject gun;
    private bool canJump;
    public bool hasGun;
    private float nextGrowth;
    private float distToGround;
    private Camera mainCamera;
    private float nextFire;
    private float lastGroundTouch;


    public void PickUpGun()
    {
        hasGun = true;
    }

	// Use this for initialization
	void Start () {
        walkingAudio = Instantiate(walkingAudio);
        anim = GetComponent<Animator>();
        distToGround = GetComponent<BoxCollider2D>().bounds.extents.y;
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        nextFire = Time.time;
   	}
	
	// Update is called once per frame
	void Update () {
        if (hasGun)
        {
            gun.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            gun.GetComponent<SpriteRenderer>().enabled = false;
        }

        //move player
        bool touchingGround = IsTouchingGround();
        if (touchingGround)
        {
            lastGroundTouch = Time.time;
            canJump = true;
        }

        var x = Mathf.Clamp((Input.GetAxis("Horizontal") * speed), -speed, speed);
        anim.SetFloat("Speed", Mathf.Abs(x));
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>(); 
        rigidbody.AddForce(new Vector3(x, 0, 0));
        AudioSource source = walkingAudio.GetComponent<AudioSource>();
        if(touchingGround && Mathf.Abs(x) > 0.1f && !source.isPlaying)
        {
            source.Play();
        }

        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            var playerVelocity = rigidbody.velocity;
            var yVelocity = playerVelocity.y;
            playerVelocity.y = 0f;
            playerVelocity = Vector2.ClampMagnitude(playerVelocity, maxSpeed);
            playerVelocity.y = yVelocity;
            rigidbody.velocity = playerVelocity;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && 
            (touchingGround || lastGroundTouch > Time.time - jumpMargin) && canJump
            )
        {
            canJump = false;
            anim.SetTrigger(jumpHash);
            rigidbody.AddForce(new Vector3(0, jumpPower));
        }

        float direction = GetMovementDirection();
        if(direction == -1)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        
        if (hasGun)
        {
            DoGunActions();
        }
    }
    
    public void Shrink()
    {
        transform.localScale = new Vector3(.25f, .25f);
    }

    public void Grow()
    {
        transform.localScale = new Vector3(.55f, .55f);
    }

    void DoGunActions()
    {
        //move gun
        Vector3 position = Input.mousePosition;
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(
            new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    Input.mousePosition.z - mainCamera.transform.position.z
                )
           );
        Rigidbody2D gunRigid = gun.GetComponent<Rigidbody2D>();
        if (gunRigid)
        {
            gunRigid.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(
                (mousePosition.y - transform.position.y), (mousePosition.x - transform.position.x)) * Mathf.Rad2Deg);
        }
        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            gun.GetComponent<GunController>().FireBig(mousePosition);
            nextFire = Time.time + fireDelay;
        }
        else if (Input.GetMouseButton(1) && Time.time > nextFire) 
        {
            gun.GetComponent<GunController>().FireSmall(mousePosition);
            nextFire = Time.time + fireDelay;
        }

    }

    void OnCollisionEnter2D(Collision2D hit)
    {
        if(hit.gameObject.tag == "Changeable")
        {
            Rigidbody2D body = hit.collider.attachedRigidbody;
            Changeable changeable = body.gameObject.GetComponent<Changeable>();
            body.AddForceAtPosition(new Vector3((100 / changeable.currentSize) * GetMovementDirection(), 0), hit.contacts[0].point);
        }
    }

    private int GetMovementDirection()
    {
        float x = Input.GetAxis("Horizontal");
        if(x == 0)
        {
            return 1;
        }
        return (int) Mathf.Sign(x);
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }
    
    private Vector3 GetLinePoint(Vector3 a, Vector3 b)
    {
        return maxShotDistance * Vector3.Normalize(b - a) + a;
    }

    bool IsTouchingGround()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        RaycastHit2D touching = Physics2D.Raycast(transform.position, Vector2.down, distToGround + 0.1f, jumpCheckMask);
        return touching && touching.transform.name != "Player";
    }
}
