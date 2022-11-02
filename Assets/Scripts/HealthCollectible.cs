using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    int amount = 1;

    public GameObject shinePrefab;

    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        if (controller != null)
        {
            if (controller.health < controller.maxHealth)
            {
                GameObject projectileObject = Instantiate(shinePrefab, this.GetComponent<Transform>().position, Quaternion.identity);

                controller.ChangeHealth(amount);
                controller.PlaySound(collectedClip);
                Destroy(gameObject);
            }
        }
    }
}
