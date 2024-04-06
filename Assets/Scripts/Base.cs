using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private UnitGenerator _unitGenerator;
    [SerializeField] private CrystalScaner _scaner;
    [SerializeField] private Score _score;

    private void OnEnable()
    {
        _scaner.Finded += GoForCrystals;
    }

    private void OnDisable()
    {
        _scaner.Finded -= GoForCrystals;
    }

    public void StartLevel()
    {
        _unitGenerator.Generate();
        _scaner.StartScan();
    }

    public void Reset()
    {
        _unitGenerator.Reset();
        _scaner.Reset();
    }

    private void GoForCrystals(List<Crystal> crystals)
    {
        if (crystals.Count() == 0)
            return;

        List<Unit> freeUnits = _unitGenerator.Units.Where(unit => unit.IsBusy == false).ToList();

        if (freeUnits.Count() == 0)
            return;

        int pairCount = Mathf.Min(freeUnits.Count(), crystals.Count);

        for (int i = 0; i < pairCount; i++)
        {
            freeUnits[i].MoveTo(crystals[i].transform.position);
        }
    }

    public void AbsorbCristal()
    {
        _score.Add(1);
    }
}
