using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    public AudioClip damageClip;

    void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null)
        {
            bool result = controller.ChangeHealth(-1);
            if (result)
            {
                controller.PlaySound(damageClip);
            }
        }
    }
}
