using System;
using UnityEngine;

public class Counter : MonoBehaviour
{
    public int CurrentValue { get; protected set; }

    public event Action<int> Changed;

    public void Reset()
    {
        CurrentValue = 0;
    }

    public virtual void Add(int increment)
    {
        if (increment <= 0)
            return;

        CurrentValue += increment;
        Changed?.Invoke(CurrentValue);
    }
}
