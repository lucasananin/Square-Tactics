using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisualSingle : MonoBehaviour
{
    [SerializeField] MeshRenderer _meshRenderer = null;

    public void Show()
    {
        _meshRenderer.enabled = true;
    }

    public void Hide()
    {
        _meshRenderer.enabled = false;
    }

    public void SetMaterial(Material _material)
    {
        _meshRenderer.sharedMaterial = _material;
    }
}
