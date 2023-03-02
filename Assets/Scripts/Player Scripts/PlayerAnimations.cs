using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private RuntimeAnimatorController[] animControllers;

    private string currentAnimation;
    private Vector3 currentScale;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation)
            return;

        animator.Play(newAnimation);
        currentAnimation = newAnimation;
    }

    public void ChangeFacingDirection(bool faceingRight)
    {
        currentScale = Vector3.one;
        if (faceingRight)
            currentScale.x = 1f;
        else
            currentScale.x = -1f;

        transform.localScale = currentScale;
    }

    public void ChangeAnimatorController(int controllerIndex)
    {
        animator.runtimeAnimatorController = animControllers[controllerIndex];
        currentAnimation = "";
    }

    public int GetNumberOfWeapons()
    {
        return animControllers.Length;
    }
}
