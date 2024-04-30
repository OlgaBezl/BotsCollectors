using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrystalScaner : MonoBehaviour
{
    [SerializeField] private float _delay = 5f;
    [SerializeField] private GameObject _visualEffect;

    private bool _isActive;
    private Coroutine _coroutine;
    private CrystalPool _crystalPool;
    private List<Crystal> _foundedCrystal;

    public event Action<List<Crystal>> Finded;

    public void StartScan(CrystalPool crystalPool)
    {
        _crystalPool = crystalPool;
        _foundedCrystal = new List<Crystal>();
        _isActive = true;
        _visualEffect.SetActive(true);
        _coroutine = StartCoroutine(Scan());
    }

    public void Reset()
    {
        _isActive = false;
        StopCoroutine(_coroutine);
        _foundedCrystal.Clear();
        _visualEffect.SetActive(false);
    }

    public List<Crystal> GetFreeCrystals()
    {
        return _foundedCrystal.Where(crystal => crystal.CurrentState == CrystalState.Found).ToList();
    }

    private IEnumerator Scan()
    {
        var wait = new WaitForSeconds(_delay);

        while (_isActive)
        {
            _foundedCrystal = _crystalPool.GetAllActive();
            _foundedCrystal.Where(cristal => cristal.CurrentState == CrystalState.Free).ToList().
                            ForEach(cristal => cristal.Found());

            if (_foundedCrystal.Count > 0 )
            {
                Finded?.Invoke(_foundedCrystal);
            }

            yield return wait;
        }
    }
}
