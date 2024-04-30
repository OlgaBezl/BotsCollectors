using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private MaterialChanger _materialChanger;
    [SerializeField] private Collider _collider;

    private CrystalPool _crystalPool;

    public CrystalState CurrentState { get; private set; } = CrystalState.NotActive;

    public void SetPosition(Vector3 position, CrystalPool crystalPool)
    {
        _crystalPool = crystalPool;
        transform.position = position;
    }

    public void Activate()
    {
        CurrentState = CrystalState.Free;
        gameObject.SetActive(true);
    }

    public void Reset()
    {
        gameObject.SetActive(false);
        CurrentState = CrystalState.NotActive;
        _materialChanger.SetDefaultMaterial();
    }

    public void Found()
    {
        _collider.enabled = true;
        CurrentState = CrystalState.Found;
        _materialChanger.SetAlternativeMaterial();
    }

    public void WentAfter()
    {
        CurrentState = CrystalState.WentAfter;
    }

    public void PickUp(Transform parentTransform, float holdDistance)
    {
        CurrentState = CrystalState.PickUped;
        _collider.enabled = false;
        transform.SetParent(parentTransform);
        transform.localPosition = new Vector3(0f, 0f, holdDistance);
    }

    public void Absorb()
    {
        Reset();
        _crystalPool.AbsorbCrystal(this);
    }
}
