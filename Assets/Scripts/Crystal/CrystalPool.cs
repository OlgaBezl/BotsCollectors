using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CrystalPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Crystal _prefab;

    private Queue<Crystal> _pool;
    private List<Crystal> _activeObjects;

    public event Action CrystalAbsorbed;

    private void Awake()
    {
        _pool = new Queue<Crystal>();
        _activeObjects = new List<Crystal>();
    }

    private void OnDisable()
    {
        foreach (Crystal crystal in _pool)
        {
            crystal.Absorbed -= AbsorbCrystal;
        }
    }

    public void Reset()
    {
        OnDisable();
        _pool.Clear();
    }

    public Crystal GetObject()
    {
        if (_pool.Count == 0)
        {
            Crystal newCrystal = Instantiate(_prefab);
            newCrystal.transform.parent = _container;
            newCrystal.Absorbed += AbsorbCrystal;
            _activeObjects.Add(newCrystal);

            return newCrystal;
        }

        Crystal crystal = _pool.Dequeue();
        crystal.gameObject.SetActive(true);
        _activeObjects.Add(crystal);

        return crystal;
    }

    public List<Crystal> GetAllActive()
    {
        return _activeObjects.Where(crystal => crystal.IsPickUped == false).ToList();
    }

    private void AddObject(Crystal crystal)
    {
        _pool.Enqueue(crystal);
        _activeObjects.Remove(crystal);
        crystal.gameObject.SetActive(false);
    }

    private void AbsorbCrystal(Crystal crystal)
    {
        crystal.transform.SetParent(_container);
        AddObject(crystal);
        CrystalAbsorbed?.Invoke();
    }
}
