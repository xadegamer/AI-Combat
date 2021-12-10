using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State {Idle, Attack, SpecialAttack, Dodge, Strafe}

    [SerializeField] State state;
    [SerializeField] Transform target;
    [SerializeField] float strafeDuration;

    [SerializeField] bool isBlocking;

    AnimationHandler animationHandler;
    DamageCollider damageCollider;
    void Start()
    {
        animationHandler = GetComponent<AnimationHandler>();
        damageCollider = GetComponentInChildren<DamageCollider>();

       // SwitchState(State.Strafe);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            animationHandler.AttackAnimation();
        }
    }

    IEnumerator SpecialAttack()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator Dodge()
    {
        yield return new WaitForSeconds(1);
    }

    IEnumerator Strafe()
    {
        float currentDuration = strafeDuration;

        while (currentDuration > 0)
        {
            DoStraf(Vector3.up);

            currentDuration--;
            yield return null;
        }

        animationHandler.StrafAnimation(0.5f);

        SwitchState(State.Attack);
    }

    public void SwitchState(State newState)
    {
        state = newState;
        StartCoroutine(newState.ToString());
    }

    void Update()
    {
        LookAtTarget();
    }

    public void DoStraf(Vector3 direction)
    {
        animationHandler.StrafAnimation(direction.y);
        transform.RotateAround(target.transform.position, direction, 30 * Time.deltaTime);
    }

    public void LookAtTarget()
    {
        transform.LookAt(target);
    }

    public void ToggleDamageOn()
    {
        damageCollider.ToggleDamageCollider(true);
    }

    public void ToggleDamageOff()
    {
        damageCollider.ToggleDamageCollider(false);
    }

    public void ToggleBlocking()
    {
        isBlocking = !isBlocking;
    }

    public bool IsBlocking() => isBlocking;

    public void BlockHit()
    {
        animationHandler.BlockHitAnimation();
    }

    public void Hit()
    {
        animationHandler.HitAnimation();
    }
}
