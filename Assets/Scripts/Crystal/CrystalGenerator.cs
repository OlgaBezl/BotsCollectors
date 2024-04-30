using System.Collections;
using UnityEngine;

public class CrystalGenerator : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private PlayingField _playingField;
    [SerializeField] private float _indent = 2;
    [SerializeField] private float _maxTryGenerateCount = 5;
    [SerializeField] private LayerMask _excludedLayerMask;
    [SerializeField] private CrystalPool _crystalPool;

    private bool _isActive;
    private Coroutine _coroutine;

    public void StartWork()
    {
        _isActive = true;
        _coroutine = StartCoroutine(GenerateCrystals());
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
            TrySpawn();
            yield return wait;
        }
    }

    private void TrySpawn()
    {
        Vector3? spawnPosition = TryGetSpawnPoint();

        if(spawnPosition.HasValue)
        {
            Crystal crystal = _crystalPool.GetObject();
            crystal.SetPosition(spawnPosition.Value, _crystalPool);
        }
    }

    private Vector3? TryGetSpawnPoint()
    {
        int tryCount = 0;

        while (tryCount < _maxTryGenerateCount)
        {
            tryCount++;
            Vector3 spawnPosition = _playingField.GetRandomPosition();
            Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, _indent, _excludedLayerMask);

            if (hitColliders.Length == 0)
                return spawnPosition;
        }

        return null;
    }
}
