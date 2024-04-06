using System;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public event Action<Crystal> PickedUp;
    public event Action<Crystal> Absorbed;

    public bool IsPickUped {  get; private set; } 
    
    public void PickUp(Transform parentTransform, float holdDistance)
    {
        IsPickUped = true;
        transform.SetParent(parentTransform);
        transform.localPosition = new Vector3(0f, 0f, holdDistance);

        PickedUp?.Invoke(this);
    }

    public void Absorb()
    {
        IsPickUped = false;
        Absorbed?.Invoke(this);
    }
}
