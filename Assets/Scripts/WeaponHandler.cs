using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    bool aboutToAttack;
    DamageCollider damageCollider;
    void Start()
    {
        damageCollider = GetComponentInChildren<DamageCollider>(); 
    }

    public void ToggleDamageOn()
    {
        damageCollider.ToggleDamageCollider(true);
    }

    public void ToggleDamageOff()
    {
        damageCollider.ToggleDamageCollider(false);
    }

    public bool AboutToAttack()
    {
        return aboutToAttack;
    }


    public void ToggleDamageIndicatorOn()
    {
        aboutToAttack = true;
    }

    public void ToggleDamageIndicatorOff()
    {
        aboutToAttack = false;
    }
}
