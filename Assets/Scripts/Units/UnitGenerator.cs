using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitGenerator : MonoBehaviour
{
    [SerializeField] private int _defaultUnitsCount;
    [SerializeField] private int _distanceFromBase;
    [SerializeField] private Unit _prefab;
    [SerializeField] private Transform _container;

    public List<Unit> Units;

    private void Awake()
    {
        Units = new List<Unit>();
    }

    public List<Unit> Generate(Transform generationPoint, Unit firstUnit = null)
    {
        if (firstUnit == null)
        {
            float angleStep = 360 / _defaultUnitsCount;

            for (int i = 0; i < _defaultUnitsCount; i++)
            {
                float degreeAngle = angleStep * i;
                AddNew(generationPoint, degreeAngle);
            }
        }
        else
        {
            Units.Add(firstUnit);
        }

        return Units;
    }

    public void Reset()
    {
        foreach(Unit unit in Units)
        {
            Destroy(unit.gameObject);
        }

        Units.Clear();
    }

    public Unit AddNewToRandomPosition(Transform baseTransform)
    {
        float maxDegreeAngle = 360f;
        float randomDegreeAngle = UnityEngine.Random.Range(0, maxDegreeAngle);
        return AddNew(baseTransform, randomDegreeAngle);
    }

    private Unit AddNew(Transform baseTransform, float degreeAngle)
    {
        float radianAngle = degreeAngle * Mathf.Deg2Rad;

        Vector2 localPosition = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle)) * _distanceFromBase;

        Unit newUnit = Instantiate(_prefab, _container);

        newUnit.transform.localPosition = new Vector3(
            baseTransform.position.x + localPosition.x,
            baseTransform.position.y,
            baseTransform.position.z + localPosition.y
            );

        newUnit.SetParentBasePosition(baseTransform.position);
        Units.Add(newUnit);

        return newUnit;
    }
}
