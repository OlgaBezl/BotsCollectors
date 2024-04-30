using System;
using System.Drawing;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayingField : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private float _borderIndent;

    private float _halfWidth;
    private float _halfDepth;

    public event Action Clicked;

    private void Start()
    {
        Vector3 size = _meshRenderer.bounds.size;
        _halfWidth = size.x / 2f - _borderIndent;
        _halfDepth = size.z / 2f - _borderIndent;
    }

    private void OnMouseDown()
    {
        Clicked?.Invoke();
    }

    public Vector3 GetRandomPosition()
    {
        float positionX = Random.Range(-_halfWidth, _halfWidth);
        float positionZ = Random.Range(-_halfDepth, _halfDepth);

        return new Vector3(positionX, transform.position.y, positionZ);
    }
}
