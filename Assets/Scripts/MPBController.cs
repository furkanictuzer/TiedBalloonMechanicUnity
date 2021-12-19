using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MPBController : MonoBehaviour
{
    private List<Color> mainColors = new List<Color>()
        {Color.red, Color.blue, Color.yellow, Color.cyan, Color.green, Color.magenta};
    
    private Color _mainColor;
    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock = null;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
        _mainColor = mainColors[Random.Range(0, mainColors.Count)];
    }

    private void Update()
    {
        _materialPropertyBlock.SetColor("_Color",_mainColor);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
