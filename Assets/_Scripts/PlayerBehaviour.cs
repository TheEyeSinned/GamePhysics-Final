using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public Transform bulletSpawn;
    public GameObject bullet;
    public int fireRate;


    public BulletManager bulletManager;

    [Header("Movement")]
    public float speed;
    public bool isGrounded;
    public bool isColliding;
    public Contact touchContact;

    public RigidBody3D body;
    public CubeBehaviour cube;
    public Camera playerCam;

    public LevelLoader level;

    void start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            level.IntroScene();
            Cursor.lockState = CursorLockMode.None;
        }
        _Fire();
        _Move();
    }

    private void _Move()
    {


        if (isGrounded)
        {

            if (Input.GetAxisRaw("Horizontal") > 0.0f)
            {
                // move right
                body.velocity += playerCam.transform.right * speed * Time.deltaTime;
            }

            else if (Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                // move left
                body.velocity += -playerCam.transform.right * speed * Time.deltaTime;
            }

            if (Input.GetAxisRaw("Vertical") > 0.0f)
            {
                // move forward
                body.velocity += playerCam.transform.forward * speed * Time.deltaTime;
            }

            else if (Input.GetAxisRaw("Vertical") < 0.0f)
            {
                // move Back
                body.velocity += -playerCam.transform.forward * speed * Time.deltaTime;
            }



            body.velocity = new Vector3(body.velocity.x, 0.0f, body.velocity.z); // remove y

            if (Input.GetAxisRaw("Jump") > 0.0f)
            {
                body.velocity += body.transform.up * speed * Time.deltaTime;
            }

            body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, 0.95f);
           
        }

        if (isColliding)
        {
            if (touchContact.face.x < 0.0f)
            {
                if (body.velocity.x < 0.0f)
                {
                    body.velocity.x = 0.0f;
                    
                }
            }

            if (touchContact.face.x > 0.0f)
            {
                if (body.velocity.x > 0.0f)
                {
                    body.velocity.x = 0.0f;
                    
                }
            }

            if (touchContact.face.z > 0.0f)
            {
                if (body.velocity.z > 0.0f)
                {
                    body.velocity.z = 0.0f;
                    
                }
            }

            if (touchContact.face.z < 0.0f)
            {
                if (body.velocity.z < 0.0f)
                {
                    body.velocity.z = 0.0f;
                    
                }
            }

        }

        transform.position += body.velocity;
    }

   

    private void _Fire()
    {
        if (Input.GetAxisRaw("Fire1") > 0.0f)
        {
            // delays firing
            if (Time.frameCount % fireRate == 0)
            {

                var tempBullet = bulletManager.GetBullet(bulletSpawn.position, bulletSpawn.forward);
                tempBullet.transform.SetParent(bulletManager.gameObject.transform);
            }
        }
    }

    void FixedUpdate()
    {
        GroundCheck();
        CollideCheck();  //to check for colliding
    }

    private void GroundCheck()
    {
        isGrounded = cube.isGrounded;
    }

    //added to check for colliding
    private void CollideCheck()
    {
        isColliding = cube.isColliding;
        if (isColliding)
        {
            touchContact = cube.touchContact;
        }
        else
        {
            touchContact = null;
        }
    }
}
