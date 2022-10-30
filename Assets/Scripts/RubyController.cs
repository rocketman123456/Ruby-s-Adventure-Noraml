using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 1.5f;
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 4.0f;

    public int maxHealth = 5;
    int currentHealth;

    Rigidbody2D rigidbody2d;
    Vector2 velocity;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        rigidbody2d = GetComponent<Rigidbody2D>();

        // set data
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        velocity = new Vector2(horizontal, vertical);
        velocity = velocity.normalized;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = runningSpeed;
        }
        else
        {
            speed = walkingSpeed;
        }

        Vector2 position = transform.position;
        position = position + speed * Time.deltaTime * velocity;
        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
