using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void StrafAnimation(float direction)
    {
        animator.SetFloat(AnimationTags.Straf, direction);
    }

    public void AttackAnimation()
    {
        if (AnimationIsPlaying(AnimationTags.ATTACK)) return;
        animator.SetTrigger(AnimationTags.ATTACK);
    }

    public void SpeciaialAttackAnimation()
    {
        if (AnimationIsPlaying(AnimationTags.SPECIALATTACK)) return;
        animator.SetTrigger(AnimationTags.SPECIALATTACK);
    }

    public void HitAnimation()
    {
        if (AnimationIsPlaying(AnimationTags.HIT)) return;
        animator.SetTrigger(AnimationTags.HIT);
    }


    public void BlockAnimation()
    {
        if (AnimationIsPlaying(AnimationTags.BLOCK)) return;
        animator.SetTrigger(AnimationTags.BLOCK);
    }

    public void BlockHitAnimation()
    {
        if (AnimationIsPlaying(AnimationTags.BLOCKHIT)) return;
        animator.SetTrigger(AnimationTags.BLOCKHIT);
    }

    public bool AnimationIsPlaying(string stateName)
    {
        return (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName) && CurrentAnimationStillPlaying());
    }

    public bool CurrentAnimationStillPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).normalizedTime != 1;
    }
}
