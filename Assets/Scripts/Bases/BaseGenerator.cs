using System.Collections.Generic;
using UnityEngine;

public class BaseGenerator : MonoBehaviour
{
    [SerializeField] private PlayingField _playingField;
    [SerializeField] private Base _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private UICounterGenerator _counterGenerator;
    [SerializeField] private FlagGenerator _flagGenerator;
    [SerializeField] private CrystalPool _crystalPool;
    [SerializeField] private BaseCounter _baseCounter;
    [SerializeField] private UnitGenerator _unitGenerator;

    private List<Base> _bases;

    public void StartWork()
    {
        _bases = new List<Base>();
        AddNew();
    }

    public void Reset()
    {
        _unitGenerator.Reset();
        _counterGenerator.Reset();

        foreach (Base parentBase in _bases)
        {
            parentBase.Selected -= ChangeSelectedBase;
            parentBase.Flag.ReadyToBuilded -= AddNew;
            parentBase.Reset();
            Destroy(parentBase.gameObject);
        }

        _bases.Clear();
    }

    private void AddNew()
    {
        AddNew(_playingField.GetRandomPosition());
    }

    private void AddNew(Vector3 position, Unit firstUnit = null)
    {
        Base newBase = Instantiate(_prefab, _container);
        newBase.transform.localPosition = position;
        newBase.Selected += ChangeSelectedBase;

        Flag flag = _flagGenerator.Generate(newBase.transform);
        flag.ReadyToBuilded += AddNew;

        newBase.StartWork(_crystalPool, flag, _unitGenerator, firstUnit);

        _counterGenerator.Generate(newBase.CounterPlaceTransform.position, newBase.CrystalCounter);

        _bases.Add(newBase);
        _baseCounter.Add(1);
    }

    private void ChangeSelectedBase(Base selectedBase)
    {
        foreach(Base parentBase in _bases)
        {
            if (parentBase != selectedBase)
            {
                parentBase.SetSelected(false);
            }
        }
    }
}
