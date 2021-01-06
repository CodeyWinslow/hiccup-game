using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargeting : MonoBehaviour
{
    public struct TargetEventArgs
    {
        public TargetEventArgs (Targetable t, bool isT)
        {
            target = t;
            isTargeting = isT;
        }

        public Targetable target;
        public bool isTargeting;
    }

    LinkedList<Targetable> targets;
    int currentTarget = 0;
    GameInput input;

    public bool HasTarget => TargetTransform != null;

    public bool Targeting { get; private set; }

    public Transform TargetTransform { get; private set; }

    public event System.EventHandler<TargetEventArgs> TargetChanged;

    private void Awake()
    {
        input = GameInput.GetInstance();
        Targeting = false;
        TargetTransform = null;
        currentTarget = 0;
        targets = new LinkedList<Targetable>();
    }
    private void Update()
    {
        if (input.GetTargetAxis() > 0 && !Targeting)
        {
            Targeting = true;
            ChangeTarget(currentTarget);
        }

        if (input.GetTargetAxis() <= 0 && Targeting)
        {
            Targeting = false;
            ChangeTarget(currentTarget);
        }

        if (input.GetSwitchTargetDown() && Targeting)
        {
            NextTarget();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Targetable target = other.gameObject.GetComponent<Targetable>();
        if (target)
            AddTarget(target);
    }

    private void OnTriggerExit(Collider other)
    {
        Targetable target = other.gameObject.GetComponent<Targetable>();
        if (target)
            RemoveTarget(target);
    }

    void RemoveTarget(Targetable target)
    {
        OnDestroyMessage ond = target.GetComponent<OnDestroyMessage>();
        if (ond)
            ond.Destroyed -= ObjectDestroyed;

        targets.Remove(target);

        TargetEventArgs e = new TargetEventArgs(target, false);
        TargetChanged?.Invoke(this, e);
        TargetChanged -= target.OnTargetChanged;

        if (targets.Count == 0)
        {
            currentTarget = 0;
            TargetTransform = null;
        }
        else
        {
            while (currentTarget >= targets.Count)
                --currentTarget;

            ChangeTarget(currentTarget);
        }
    }

    void AddTarget(Targetable target)
    {
        targets.AddLast(target);
        TargetChanged += target.OnTargetChanged;
        ChangeTarget(currentTarget);
        OnDestroyMessage ond = target.GetComponent<OnDestroyMessage>();
        if (ond)
            ond.Destroyed += ObjectDestroyed;
    }

    private void ObjectDestroyed(object sender, GameObject e)
    {
        Targetable target = e.GetComponent<Targetable>();
        if (target)
            RemoveTarget(target);
    }

    void NextTarget()
    {
        if (targets.Count == 0)
        {
            TargetTransform = null;
            return;
        }

        if (targets.Count > currentTarget + 1)
            currentTarget++;
        else
        {
            currentTarget = 0;
        }

        ChangeTarget(currentTarget);
    }

    void PreviousTarget()
    {
        if (targets.Count == 0)
        {
            TargetTransform = null;
            return;
        }

        if (currentTarget - 1 >= 0)
            currentTarget--;
        else
        {
            currentTarget = targets.Count - 1;
        }

        ChangeTarget(currentTarget);
    }

    private void ChangeTarget(int targetInd)
    {
        var iter = targets.GetEnumerator();
        if (iter.MoveNext())
        {
            for (int ii = 0; ii < targetInd; ii++)
            {
                if (!iter.MoveNext())
                {
                    TargetTransform = null;
                    return;
                }
            }

            TargetTransform = iter.Current.targetTransform;
            TargetEventArgs eventArgs =
                new TargetEventArgs(iter.Current, Targeting);
            TargetChanged?.Invoke(this, eventArgs);
        }
    }
}
