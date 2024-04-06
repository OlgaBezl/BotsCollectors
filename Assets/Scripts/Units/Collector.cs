using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Unit _unit;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Crystal crystal))
        {
            _unit.PickUpCrystal(crystal);
        }
    }
}
