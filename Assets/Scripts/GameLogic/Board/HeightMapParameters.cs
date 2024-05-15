using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HeightMapParameters", menuName = "ScriptableObjects/Board/HeightMapParameters", order = 100)]
public class HeightMapParameters : ScriptableObject
{
    [Header("Map coordinates position")]
    public Vector2 center;
    public float minHeight;
    public float maxHeight;

    [Header("Perlin noise")]
    public float perlinNoiseImpact;
    public float perlinNoiseDensity;

    [Header("Max and min islands distance from center")]
    public float minRadius;
    public float maxRadius;

    [Header("Islands paramters")]
    public int mainIslandsAmount;
    public float minIslandHeight;
    public float islandsDensity;

    [Header("Weird islands paramters")]
    public float nextIslandsHeightMultiplier;
    public float nextIslandsAmountPerHeight;
    public float mountainsDensity;
}