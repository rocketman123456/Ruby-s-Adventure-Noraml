using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 1.5f;
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 4.0f;

    public int maxHealth = 5;

    public int health { get { return currentHealth; } }
    int currentHealth;

    public float timeInvincible = 2.0f;
    float invincibleTimer = 0.0f;
    bool isInvincible = false;

    Rigidbody2D rigidbody2d;

    Vector2 velocity = new Vector2(0, 0);
    Vector2 lookDirection = new Vector2(0, -1);
    float horizontal;
    float vertical;

    Animator animator;

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // set data
        currentHealth = maxHealth;
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        velocity.x = horizontal;
        velocity.y = vertical;

        if (!Mathf.Approximately(velocity.x, 0.0f) || !Mathf.Approximately(velocity.y, 0.0f))
        {
            lookDirection.Set(velocity.x, velocity.y);
            lookDirection.Normalize();
            velocity.Normalize();
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
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

        animator.SetFloat("Speed", speed * velocity.magnitude);
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            animator.SetTrigger("Hit");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
