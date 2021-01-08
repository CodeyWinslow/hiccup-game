using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvents : MonoBehaviour
{
    public UnityEvent OnFemalePunchHit;
    public UnityEvent OnFemalePunchEnd;
    public UnityEvent OnFemaleHitEnd;
    public UnityEvent OnFemaleDeathEnd;

    // Start is called before the first frame update
    public void FemalePunchHit()
    {
        OnFemalePunchHit?.Invoke();
    }

    public void FemalePunchEnd()
    {
        OnFemalePunchEnd?.Invoke();
    }

    public void FemaleHurtEnd()
    {
        OnFemaleHitEnd?.Invoke();
    }

    public void FemaleDeathEnd()
    {
        OnFemaleDeathEnd?.Invoke();
    }
}
