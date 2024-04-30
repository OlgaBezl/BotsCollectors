using System;
using UnityEngine;

public class BaseCounter : Counter
{
    [field: SerializeField] public int MaxValue { get; private set; }

    public event Action Finished;

    public override void Add(int increment)
    {
        base.Add(increment);

        if (CurrentValue >= MaxValue)
        {
            CurrentValue = MaxValue;
            Finished?.Invoke();
        }
    }
}
