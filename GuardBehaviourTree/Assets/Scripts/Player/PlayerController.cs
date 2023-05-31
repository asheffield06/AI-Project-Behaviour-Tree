using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    float horizontal;
    float vertical;

    public float speed = 20.0f;

    public GameObject playerModel;

    Vector3 previousPosition;
    Vector3 lastMoveDirection;

    public bool LootCollected;

    public bool movementLocked = false;

    // Set up singleton
    public static PlayerController instance;
    public void Awake()
    {
        // Initialise Singleton
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        instance = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        previousPosition = transform.position;
        lastMoveDirection = Vector3.zero;
    }

    void Update()
    {
        if (!movementLocked)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");
        }
        
    }

    private void FixedUpdate()
    {
        if (!movementLocked)
        {
            if (transform.position != previousPosition)
            {
                lastMoveDirection = (transform.position - previousPosition).normalized;
                previousPosition = transform.position;
            }

            rb.velocity = new Vector3(horizontal * speed, 0, vertical * speed);
            playerModel.transform.rotation = Quaternion.LookRotation(rb.velocity, transform.up);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        
    }
}
