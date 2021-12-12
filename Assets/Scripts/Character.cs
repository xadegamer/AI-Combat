using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum State {Attack, SpecialAttack, Dodge, Strafe}

    [SerializeField] State state;
    [SerializeField] Transform target;
    [SerializeField] float stateDelay;

    [Header("State Duration")]

    [SerializeField] float attackDuration;
    [SerializeField] float specialDuration;
    [SerializeField] float dodgeDuration;
    [SerializeField] float strafeDuration;
    
    bool isBlocking;
    int currentChoice;
    int hits = 0;

    WaitForSeconds w_battleDelay;
    WaitForSeconds w_attackDelay;
    WaitForSeconds w_specialAttackDelay;
    WaitForSeconds w_dodgeDelay;


    AnimationHandler animationHandler;

    private void Awake()
    {
        w_battleDelay = new WaitForSeconds(GameManager.Instance.BattleDelay());

        w_attackDelay = new WaitForSeconds(attackDuration + stateDelay);
        w_specialAttackDelay = new WaitForSeconds(specialDuration + stateDelay);
        w_dodgeDelay = new WaitForSeconds(dodgeDuration + stateDelay);
    }

    IEnumerator Start()
    {
        animationHandler = GetComponent<AnimationHandler>();

        yield return w_battleDelay;

        SwitchState();
    }

    public void SwitchState()
    {
        int randomState = Random.Range(0,4);

        while (randomState == currentChoice || randomState == (int)State.Dodge) { randomState = Random.Range(0, 4); }

        currentChoice = randomState;

        state = (State)currentChoice;

        if(!CanDodge()) StartCoroutine(state.ToString());
    }

    IEnumerator Attack()
    {
        animationHandler.AttackAnimation();
        yield return w_attackDelay;
        SwitchState();
    }

    IEnumerator SpecialAttack()
    {
        animationHandler.SpeciaialAttackAnimation();
        yield return w_specialAttackDelay;
        SwitchState();
    }

    IEnumerator Dodge()
    {
        animationHandler.BlockAnimation();
        yield return w_dodgeDelay;
        SwitchState();
    }

    IEnumerator Strafe()
    {
        float currentDuration = strafeDuration;

        while (currentDuration > 0)
        {
            DoStraf(Vector3.up);

            currentDuration -= Time.deltaTime;
            yield return null;
        }

        animationHandler.StrafAnimation(0.5f);

        SwitchState();
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
        hits++;
    }

    public bool CanDodge()
    {
        if (target.GetComponent<WeaponHandler>().AboutToAttack())
        {
            int randomChanceToDodge = Random.Range(0, 10);
            if (randomChanceToDodge > 1)
            {
                state = State.Dodge;
                currentChoice = (int) state;
                StartCoroutine(state.ToString());
                return true;
            }
        }
        return false;
    }

    public void CounterAttack()
    {
        StopAllCoroutines();
        state = State.Attack;
        currentChoice = (int)state;
        StartCoroutine(state.ToString());
    }
}
