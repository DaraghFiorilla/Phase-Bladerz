using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public UnityEvent OnAttackPerformed;
    public UnityEvent SetAttackingSpeedPos;
    public UnityEvent SetAttackingSpeedNeg;
    public UnityEvent OnBoomerang;

    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }

    public void InvokeBoomerangAttack()
    {
        OnBoomerang?.Invoke();
    }

    public void SwitchAttackingSpeedPos()
    {
        SetAttackingSpeedPos?.Invoke();
    }

    public void SwitchAttackingSpeedNeg()
    {
        SetAttackingSpeedNeg?.Invoke();
    }
}
