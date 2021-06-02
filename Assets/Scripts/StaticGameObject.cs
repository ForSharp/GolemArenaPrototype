using System;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class StaticGameObject : MonoBehaviour
{
    private Renderer _renderer;
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _renderer.material.ChangeAlpha(0.5f);
        _renderer.material.color = GetColorChangeAlpha(_renderer.material.color, 0.5f);
    }

    private Color GetColorChangeAlpha(Color color, float alpha)
    {
        if (alpha < 0 || alpha > 1)
            throw new Exception();
        
        Color newColor = new Color(color.r, color.g,
            color.b, alpha);
        return newColor;
    }
}
