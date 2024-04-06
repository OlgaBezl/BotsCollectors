using System.Collections;
using UnityEngine;

public class CrystalGenerator : MonoBehaviour
{
    [SerializeField] private float _delay;

    [SerializeField] private float _leftBound;
    [SerializeField] private float _rightBound;
    [SerializeField] private float _topBound;
    [SerializeField] private float _bottomBound;
    [SerializeField] private float _indent = 2;
        
    [SerializeField] private CrystalPool _crystalPool;

    private bool _isActive;
    private Coroutine _coroutine;

    public Coroutine StartGeneration()
    {
        _isActive = true;
        return _coroutine = StartCoroutine(GenerateCrystals());
    }

    public void Reset()
    {
        _isActive = false;
        StopCoroutine(_coroutine);
        _crystalPool.Reset();
    }

    private IEnumerator GenerateCrystals()
    {
        var wait = new WaitForSeconds(_delay);

        while (_isActive)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        Crystal crystal = _crystalPool.GetObject();
        crystal.transform.position = GetSpawnPoint();
    }

    private Vector3 GetSpawnPoint()
    {
        float spawnPositionX = Random.Range(_rightBound, _leftBound);
        float topBound = _topBound;
        float bottomBound = _bottomBound;

        if (spawnPositionX > (transform.position.x - _indent) &&
            spawnPositionX < (transform.position.x + _indent))
        {
            bool isTopInterval = System.Convert.ToBoolean(Random.Range(0, 2));

            if (isTopInterval)
            {
                bottomBound = transform.position.y + _indent;
            }
            else
            {
                topBound = transform.position.y - _indent;
            }
        }

        float spawnPositionZ = Random.Range(topBound, bottomBound);

        return new Vector3(spawnPositionX, transform.position.y, spawnPositionZ);
    }
}
