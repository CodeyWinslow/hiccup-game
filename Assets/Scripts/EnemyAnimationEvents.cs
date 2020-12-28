using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimationEvents : MonoBehaviour
{
    public UnityEvent OnEnemyPunchHit;
    public UnityEvent OnEnemyHitEnd;
    // Start is called before the first frame update
    public void MalePunchHit()
    {
        OnEnemyPunchHit?.Invoke();
    }

    public void MaleHurtEnd()
    {
        OnEnemyHitEnd?.Invoke();
    }
}
