using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerMovement : MonoBehaviour
{
    Rigidbody2D body;

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    public bool hasTigerKey;
    public bool hasGiraffeKey;
    public bool hasElephantKey;
    public bool hasNoKey;

    public float runSpeed = 20.0f;
    public Transform key;


    public GameObject wayPoint;

    private float timer = 0.5f;

    void Start()
    {
        hasTigerKey = false;
        hasGiraffeKey = false;
        hasElephantKey = false;
        body = GetComponent<Rigidbody2D>();
        hasNoKey = true;
    }

    void Update()
    {
        
        // Gives a value between -1 and 1
        horizontal = Input.GetAxisRaw("Horizontal"); // -1 is left
        vertical = Input.GetAxisRaw("Vertical"); // -1 is down
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            //The position of the waypoint will update to the player's position
            UpdatePosition();
            timer = 0.5f;
        }
    }

    void UpdatePosition()
    {
        //The wayPoint's position will now be the player's current position.
        wayPoint.transform.position = transform.position;
    }


    void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("hit");
            
        }
        if (collision.gameObject.tag == "tigerKey" && hasNoKey)
        {
            Debug.Log("gotKey");
            hasTigerKey = true;
            hasNoKey = false;

        }
        if (collision.gameObject.tag == "giraffeKey" && hasNoKey)
        {
            Debug.Log("gotKey");
            hasGiraffeKey = true;
            hasNoKey = false;

        }
        if (collision.gameObject.tag == "elephantKey" && hasNoKey)
        {
            Debug.Log("gotKey");
            hasElephantKey = true;
            hasNoKey = false;
        }

        if (collision.gameObject.tag == "tigerLock" && hasTigerKey)
        {
            Object.Destroy(this.transform.GetChild(0).gameObject);
            hasNoKey = true;
            hasElephantKey = false;
            hasTigerKey = false;
            hasGiraffeKey = false;

        }
        if (collision.gameObject.tag == "elephantLock" && hasElephantKey)
        {
            Object.Destroy(this.transform.GetChild(0).gameObject);
            hasNoKey = true;
            hasElephantKey = false;
            hasTigerKey = false;
            hasGiraffeKey = false;


        }
        if (collision.gameObject.tag == "giraffeLock" && hasGiraffeKey)
        {
            Object.Destroy(this.transform.GetChild(0).gameObject);
            hasNoKey = true;
            hasElephantKey = false;
            hasTigerKey = false;
            hasGiraffeKey = false;


        }
    }
    
}