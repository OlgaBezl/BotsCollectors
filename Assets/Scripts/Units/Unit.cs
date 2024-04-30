using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed = 5f; 
    [SerializeField] private float _holdDistance = 1f;
    [SerializeField] private MaterialChanger _materialChanger;

    private Vector3 _targetPosition = Vector3.zero;

    public UnitState CurrentState { get; private set; } = UnitState.Wait;
    public Vector3 ParentBasePosition { get; private set; }
    public Crystal CurrentCrystal {get; private set;}

    private void Update()
    {
        if (CurrentState != UnitState.Wait)
        {
            Vector3 direction = _targetPosition - transform.position;
            transform.Translate(_speed * Time.deltaTime * direction.normalized, Space.World);
            transform.LookAt(_targetPosition);
        }
    }

    public void SetParentBasePosition(Vector3 position)
    {
        ParentBasePosition = position;
    }

    public void MoveToCrystal(Crystal crystal)
    {
        CurrentState = UnitState.MoveToCristal;
        CurrentCrystal = crystal;
        MoveTo(crystal.transform.position);
    }

    public void MoveToFlag(Flag flag)
    {
        CurrentState = UnitState.MoveToFlag;
        MoveTo(flag.transform.position);
    }

    public void PickUpCrystal()
    {
        CurrentCrystal.PickUp(transform, _holdDistance);
        MoveTo(ParentBasePosition);
        CurrentState = UnitState.MoveToParentBase;
    }

    public void ReturnToBase(Base parentBase)
    {
        CurrentCrystal.Absorb();

        _materialChanger.SetDefaultMaterial();
        CurrentState = UnitState.Wait;

        parentBase.ReturnUnitWithCristal();
    }

    public void BuildBase(Flag flag)
    {
        SetParentBasePosition(flag.transform.position);
        flag.BuildBase(this);
        _materialChanger.SetDefaultMaterial();
        CurrentState = UnitState.Wait;
    }

    private void MoveTo(Vector3 targetPosition)
    {
        _targetPosition = targetPosition;
        _materialChanger.SetAlternativeMaterial();
    }
}
