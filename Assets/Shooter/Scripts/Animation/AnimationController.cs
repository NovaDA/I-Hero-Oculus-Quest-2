using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    private Animator _animator;

    public enum AnimationState
    {
        Idle,
        Attack,
        Move,
        Die,
        Jump,
        Victory
    }

    private Dictionary<AnimationState, bool> _stateFlags = new Dictionary<AnimationState, bool>();

    private void Start()
    {
        _animator = GetComponent<Animator>();

        // Initialize state flags
        foreach (AnimationState state in System.Enum.GetValues(typeof(AnimationState)))
        {
            _stateFlags[state] = false;
        }
    }

    public void SetAnimationState(AnimationState state, bool value)
    {
        _stateFlags[state] = value;
    }

    public void TriggerAnimation(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }

    private void Update()
    {
        foreach (var state in _stateFlags)
        {
            _animator.SetBool(state.Key.ToString(), state.Value);
        }
    }
}
