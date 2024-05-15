using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameLogic
{
    public class HeightMapGenerator
    {
        private struct Island
        {
            public Vector2 position;
            public float height;

            public Island(Vector2 position, float height)
            {
                this.position = position;
                this.height = height;
            }
        }

        private List<Island> islands;
        private HeightMapParameters parameters;
        private float noiseRandom;

        public HeightMapGenerator(HeightMapParameters heightMapParameters)
        {
            parameters = heightMapParameters;
            noiseRandom = Random.value * 100f + 10000f;
        }

        public void Generate()
        {
            islands = new List<Island>();
            for (int i = 0; i < parameters.mainIslandsAmount; i++)
            {
                CreateNextIsland(GetRandomVector(parameters.minRadius, parameters.maxRadius) + parameters.center, parameters.maxHeight);
            }
        }

        private void CreateNextIsland(Vector2 position, float height)
        {
            islands.Add(new Island(position, height));
            float relativeHeight = (height - parameters.minHeight) / (parameters.maxHeight - parameters.minHeight);
            for (float i = 0; i < parameters.nextIslandsAmountPerHeight * relativeHeight; i += 1)
            {
                float newIslandHeight = Random.Range((parameters.minHeight + parameters.minIslandHeight)/2f, height * parameters.nextIslandsHeightMultiplier);
                if ((newIslandHeight + parameters.minHeight) / (parameters.maxHeight - parameters.minHeight) > parameters.minIslandHeight)
                {
                    CreateNextIsland(GetRandomVector(0, (relativeHeight + 2f) / parameters.islandsDensity * 100f) + position, newIslandHeight);
                }
            }
        }

        public float GetHeightInPosition(Vector2 position) 
        {
            float height = parameters.minHeight;
            foreach (var island in islands)
            {
                height = Mathf.Max(height, GetIslandHeightInPosition(island, position));
            }
            height += GetPerlinNoiseInPosition(position);
            return height;
        }

        private float GetIslandHeightInPosition(Island island, Vector2 position) 
        {
            float distance = (position - island.position).magnitude;
            return parameters.maxHeight / 
                (distance / parameters.mountainsDensity + (parameters.maxHeight / (parameters.maxHeight - parameters.minHeight)))
                + parameters.minHeight;
        }

        private float GetPerlinNoiseInPosition(Vector2 position)
        {
            return (Mathf.PerlinNoise(noiseRandom + position.x * parameters.perlinNoiseDensity, noiseRandom + position.y * parameters.perlinNoiseDensity) - 0.5f) * parameters.perlinNoiseImpact * (parameters.maxHeight - parameters.minHeight);
        }

        private Vector2 GetRandomVector(float minLenght, float maxLenght)
        {
            float lenght = (minLenght + Mathf.Sqrt(Random.Range(0f, 1f)) * (maxLenght - minLenght));
            float angle = Random.Range(0, Mathf.PI * 2);
            return new Vector2(Mathf.Cos(angle) * lenght, Mathf.Sin(angle) * lenght);
        }
    }
}