using System;
using UnityEngine;

public class Score : MonoBehaviour
{
    [field: SerializeField] public int MaxValue { get; private set; }
    public int CurrentValue { get; private set; }

    public event Action<int> Changed;
    public event Action Finished; 

    public void Reset()
    {
        CurrentValue = 0;
    }

    public void Add(int increment)
    {
        if (increment <= 0)
            return;

        CurrentValue += increment;
        Changed?.Invoke(CurrentValue);

        if (CurrentValue >= MaxValue)
        {
            CurrentValue = MaxValue;
            Finished?.Invoke();
        }
    }
}
