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
        damageCollider.isTrigger = true;
        damageCollider.enabled = false;
    }

    public void ToggleDamageCollider(bool newState)
    {
        damageCollider.enabled = newState;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hittable"))
        {
            Character opponet = other.GetComponent<Character>();

            if (opponet.IsBlocking()) opponet.CounterAttack();
            else opponet.Hit();
        }   
    }
}
