using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrystalPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Crystal _prefab;

    private Queue<Crystal> _pool;
    private List<Crystal> _allCrystals;

    private void Awake()
    {
        _pool = new Queue<Crystal>();
        _allCrystals = new List<Crystal>();
    }

    private void OnDisable()
    {
        foreach (Crystal crystal in _allCrystals)
        {
            Destroy(crystal.gameObject);
        }

        _allCrystals.Clear();
        _pool.Clear();
    }

    public void Reset()
    {
        OnDisable();
    }

    public Crystal GetObject()
    {
        Crystal newCrystal;

        if (_pool.Count == 0)
        {
            newCrystal = Instantiate(_prefab);
            newCrystal.transform.parent = _container;
            _allCrystals.Add(newCrystal);
        }
        else
        {
            newCrystal = _pool.Dequeue();
        }

        newCrystal.Activate();
        return newCrystal;
    }

    public List<Crystal> GetAllActive()
    {
        return _allCrystals.Where(crystal => crystal.CurrentState == CrystalState.Free || crystal.CurrentState == CrystalState.Found).ToList();
    }

    public void AbsorbCrystal(Crystal crystal)
    {
        crystal.transform.SetParent(_container);
        _pool.Enqueue(crystal);
    }
}
