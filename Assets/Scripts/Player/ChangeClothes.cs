using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothes : MonoBehaviour
{

    public SO_CharacterBody clothAnimationClip;

    private string[] _animStates = { "IdleDown", "IdleUp", "IdleRight", "IdleLeft", "WalkingDown", "WalkingUp", "WalkingRight", "WalkingLeft" };

    protected Animator animator;

    protected AnimatorOverrideController animatorOverrideController;

    private void Start()
    {
        animator = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
    }

    public void Change(SO_Cloth newPart)
    {
        foreach(BodyPart charBodyPart in clothAnimationClip.characterBodyParts)
        {
            if(charBodyPart.bodyPartType == newPart.bodyPartType)
            {
                foreach (string state in _animStates)
                {
                    string inicialAnim = charBodyPart.bodyPart.allBodyPartAnimations.Find(anim => anim.name.Contains(state)).name;

                    AnimationClip currentAnim = newPart.allBodyPartAnimations.Find(anim => anim.name.Contains(state));

                    animatorOverrideController[inicialAnim] = currentAnim;
                }
            }
        }
    }
}
