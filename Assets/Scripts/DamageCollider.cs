using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    public int damage;

    Collider damageCollider;

    private void Awake()
    {
        damageCollider = GetComponent<Collider>();
        damageCollider.gameObject.SetActive(true);
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void ToggleDamageCollider(bool newSate)
    {
        damageCollider.enabled = newSate;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hittable"))
        {
            Character opponet = other.GetComponent<Character>();
            
            if(opponet.IsBlocking())
            {
                opponet.BlockHit();
            }
            else
            {
                opponet.Hit();
            }
        }
        
    }
}
