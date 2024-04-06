using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _speed = 5f; 
    [SerializeField] private float _holdDistance = 1f;
    public bool IsBusy { get; private set; }

    private Vector3 _targetPosition = Vector3.zero;
    private Vector3 _parentBasePosition;
    private Crystal _currentCrystal;

    private void Update()
    {
        if (IsBusy)
        {
            Vector3 direction = _targetPosition - transform.position;
            transform.Translate(_speed * Time.deltaTime * direction.normalized, Space.World);
            transform.LookAt(_targetPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_currentCrystal == null)
        {
            return;
        }

        if (other.TryGetComponent(out Base parentBase))
        {
            parentBase.AbsorbCristal();
            _currentCrystal.Absorb();
            IsBusy = false;
        }
    }

    public void SetParentBasePosition(Vector3 position)
    {
        _parentBasePosition = position;
    }

    public void MoveTo(Vector3 targetPosition)
    {
        IsBusy = true;
        _targetPosition = targetPosition;        
    }

    public void PickUpCrystal(Crystal crystal)
    {
        _currentCrystal = crystal;
        _currentCrystal.PickUp(transform, _holdDistance);
        MoveTo(_parentBasePosition);
    }
}
