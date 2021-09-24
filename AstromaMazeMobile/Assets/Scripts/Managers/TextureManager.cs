using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureManager : MonoBehaviour
{
    [SerializeField] private Material groundMaterial, wallMaterial, asteroidMaterial;
    [SerializeField] private Color groundMaterialColor, wallMaterialColor, asteroidMaterialColor;
    [SerializeField] private Vector2 groundMaterialTile, wallMaterialTile, asteroidMaterialTile;

    private void Start()
    {
        groundMaterial.color = groundMaterialColor;
        groundMaterial.mainTextureScale = groundMaterialTile;

        wallMaterial.color = wallMaterialColor;
        wallMaterial.mainTextureScale = wallMaterialTile;

        asteroidMaterial.color = asteroidMaterialColor;
        asteroidMaterial.mainTextureScale = asteroidMaterialTile;
    }
}
