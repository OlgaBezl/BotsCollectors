using System.Collections.Generic;
using UnityEngine;

public class UnitGenerator : MonoBehaviour
{
    [SerializeField] private int _unitsCount;
    [SerializeField] private int _distanceFromBase;
    [SerializeField] private Transform _baseTransform;
    [SerializeField] private Unit _prefab;
    [SerializeField] private Transform _container;

    public List<Unit> Units;

    public void Generate()
    {
        Units = new List<Unit>();
        float angleStep = 360 / _unitsCount;

        for (int i = 0; i < _unitsCount; i++)
        {
            float degreeAngle = angleStep * i;
            float radianAngle = degreeAngle * Mathf.Deg2Rad;

            Vector2 localPosition = new Vector2(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle)) * _distanceFromBase;

            Unit newUnit = Instantiate(_prefab, _container);

            newUnit.transform.localPosition = new Vector3(
                transform.position.x + localPosition.x,
                transform.position.y,
                transform.position.z + localPosition.y
                );

            newUnit.SetParentBasePosition(_baseTransform.position);
            Units.Add(newUnit);
        }
    }

    public void Reset()
    {
        foreach(Unit unit in Units)
        {
            Destroy(unit.gameObject);
        }

        Units = null;
    }
}
