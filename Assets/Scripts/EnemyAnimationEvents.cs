using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationEvents : MonoBehaviour
{
    public UnityEvent OnEnemyPunchHit;
    public UnityEvent OnEnemyHitEnd;

    public void MalePunchHit()
    {
        OnEnemyPunchHit?.Invoke();
    }

    public void MaleHurtEnd()
    {
        OnEnemyHitEnd?.Invoke();
    }
}
