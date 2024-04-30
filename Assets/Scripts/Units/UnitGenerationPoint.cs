using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitGenerationPoint : MonoBehaviour
{
    private UnitGenerator _unitGenerator;
    private List<Unit> _units;

    public void StartWork(UnitGenerator unitGenerator, Unit firstUnit = null)
    {
        _unitGenerator = unitGenerator;
        _units = _unitGenerator.Generate(transform, firstUnit);
    }

    public void Reset()
    {
        _units.Clear();        
    }

    public List<Unit> GetFreeUnits()
    {
        return _units.Where(unit => unit.CurrentState == UnitState.Wait).ToList();
    }

    public void AddNew()
    {
        _units.Add(_unitGenerator.AddNewToRandomPosition(transform));
    }
}
