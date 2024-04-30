using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private CrystalScaner _scaner; 
    [SerializeField] private int _unitPrice;
    [SerializeField] private int _basePrice;
    [SerializeField] private MaterialChanger _materialChanger;
    [SerializeField] private UnitGenerationPoint _unitGenerationPoint;

    [field: SerializeField] public CrystalCounter CrystalCounter {  get; private set; }
    [field: SerializeField] public Transform CounterPlaceTransform { get; private set; }


    private bool _isSelected;

    public event Action<Base> Selected;

    public FlagState FlagState => Flag == null ? FlagState.Hide : Flag.CurrentState;
    public Flag Flag { get; private set; }

    private void OnEnable()
    {
        _scaner.Finded += GoForCrystals;
        CrystalCounter.Changed += SpendCrystals;
    }

    private void OnDisable()
    {
        _scaner.Finded -= GoForCrystals;
        CrystalCounter.Changed -= SpendCrystals;
        Flag.Setted -= SetFlag;
    }

    private void OnMouseDown()
    {
        SetSelected(!_isSelected); 
        SwitchFlag();
    }

    public void StartWork(CrystalPool crystalPool, Flag flag, UnitGenerator unitGenerator, Unit firstUnit = null)
    {
        _unitGenerationPoint.StartWork(unitGenerator, firstUnit);
        _scaner.StartScan(crystalPool);

        Flag = flag;
        Flag.Setted += SetFlag;
    }

    public void Reset()
    {
        _scaner.Reset();
        _unitGenerationPoint.Reset();
        CrystalCounter.Reset();
    }

    public void ReturnUnitWithCristal()
    {
        CrystalCounter.Add(1);        
        GoForCrystals(_scaner.GetFreeCrystals());
    }

    public void SetSelected(bool isSelected)
    {
        if (_isSelected == isSelected)
        {
            return;
        }

        _isSelected = isSelected;

        if (_isSelected)
        {
            Selected?.Invoke(this);
            _materialChanger.SetAlternativeMaterial();
        }
        else
        {
            _materialChanger.SetDefaultMaterial();
        }
    }

    private void GoForCrystals(List<Crystal> crystals)
    {
        if (crystals.Count() == 0)
            return;

        List<Unit> freeUnits = _unitGenerationPoint.GetFreeUnits();

        if (freeUnits.Count() == 0)
            return;

        int pairCount = Mathf.Min(freeUnits.Count(), crystals.Count);

        for (int i = 0; i < pairCount; i++)
        {
            freeUnits[i].MoveToCrystal(crystals[i]);
            crystals[i].WentAfter();
        }
    }
    
    private void SpendCrystals(int count)
    {
        if (FlagState == FlagState.Setted)
        {
            TryBuildBase();
            return;
        }

        if (CrystalCounter.TrySubtract(_unitPrice))
        {
            _unitGenerationPoint.AddNew();
        }
    }

    private void SetFlag(Vector3 flagPosition)
    {
        TryBuildBase();
    }

    private void TryBuildBase()
    {
        List<Unit> freeUnits = _unitGenerationPoint.GetFreeUnits();

        if (freeUnits.Count() == 0)
            return;

        if (CrystalCounter.TrySubtract(_basePrice))
        {
            freeUnits[0].MoveToFlag(Flag);
            Flag.WaitDeliveryResources();
            SetSelected(false);
        }
    }

    private void SwitchFlag()
    {
        if(FlagState == FlagState.DeliveryResourcesToFlag)
        {
            return;
        }

        if (FlagState == FlagState.Hide)
        {
            Flag.Activate();
        }
        else
        {
            Flag.Deactivate();
        }
    }
}
