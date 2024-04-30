using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> _meshRenderers;
    [SerializeField] private Material _alternativeMaterial;

    private Material _defaultMaterial;

    private void Start()
    {
        if (_meshRenderers == null || _meshRenderers.Count == 0)
            return;

        _defaultMaterial = _meshRenderers[0].material;
    }

    public void SetDefaultMaterial()
    {
        foreach (MeshRenderer meshenderer in _meshRenderers)
        {
            meshenderer.material = _defaultMaterial;
        }
    }

    public void SetAlternativeMaterial()
    {
        foreach (MeshRenderer meshenderer in _meshRenderers)
        {
            meshenderer.material = _alternativeMaterial;
        }
    }
}
