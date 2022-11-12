using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeClothes : MonoBehaviour
{
    //public SpriteRenderer bodyParty;

    public SO_BodyPart[] clothAnimationClip;

    public string position = "Down";

    private string _state = "Walking";

    //public List<Sprite> options = new List<Sprite>();

    protected Animator animator;

    protected AnimatorOverrideController animatorOverrideController;

    private int _currentOption = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _currentOption = 0;

        animatorOverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        animator.runtimeAnimatorController = animatorOverrideController;
    }

    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            NextOption();
        }
        if (Input.GetKeyDown("k"))
        {
            PreviousOption();
        }
    }

    public void NextOption()
    {
        _currentOption++;

        if (_currentOption >= clothAnimationClip.Length)
        {
            _currentOption = 0;
        }

        Change();
    }

    public void PreviousOption()
    {
        _currentOption--;

        if (_currentOption < 0)
        {
            _currentOption = clothAnimationClip.Length - 1;
        }

        Change();
    }

    public void ChangeState(string newState)
    {
        _state = newState;
        Change();
    }

    public void Change()
    {
        foreach (SO_BodyPart bodyPart in clothAnimationClip)
        {
            if (bodyPart.bodyPartAnimationID == _currentOption)
            {
                foreach (AnimationClip anim in bodyPart.allBodyPartAnimations)
                {
                    if (anim.name.Contains(_state + position))
                    {
                        animatorOverrideController["FloralGreen"+_state+"Down"] = anim;
                    }
                }
            }
        }
    }
}
