using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalScaner : MonoBehaviour
{
    [SerializeField] private float _delay = 5f;
    [SerializeField] private GameObject _visualEffect;
    [SerializeField] private CrystalPool _crystalPool;

    private bool _isActive;
    private Coroutine _coroutine;

    public event Action<List<Crystal>> Finded;

    public Coroutine StartScan()
    {
        _isActive = true;
        _visualEffect.SetActive(true);
        return _coroutine = StartCoroutine(Scan());
    }

    public void Reset()
    {
        _isActive = false;
        _visualEffect.SetActive(false);
        StopCoroutine(_coroutine);
    }

    private IEnumerator Scan()
    {
        var wait = new WaitForSeconds(_delay);

        while (_isActive)
        {
            var list = _crystalPool.GetAllActive();
            Finded?.Invoke(list);
            yield return wait;
        }
    }
}
