using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInterface;

namespace GameVisuals
{
    public class Waves : MonoBehaviour
    {
        public BoardCreator boardCreator;
        public float noiseSize = 8;
        public float noiseStrenght = 0.5f;
        public float speed = 0.4f;
        public float waterHeightImpact = 2f;

        public List<HexTileObject> waterTiles;

        private float avarageHeight = 0;
        private float perlinNoiseAverage = 0.465f;

        public Vector2 windDirection = new Vector2(1, 0);
        public float windDirectionAngle = 0;
        public float windDirectionMagnitude = 1;
        public float windDirectionChangeSpeed = 1f;

        public Vector2 currentWindVector = new Vector2(0, 0);

        public void Initialize()
        {
            waterTiles = new List<HexTileObject>();
            foreach (HexTileObject tile in boardCreator.hexTiles)
            {
                if (tile != null && tile.heightLevel == 0)
                {
                    waterTiles.Add(tile);
                }
            }

            //float xd = 0;
            //int c = 0;
            //for (float i = 0; i < 10000f; i += 0.01f)
            //{
            //    xd += Mathf.PerlinNoise(i, 0);
            //    c++;
            //}
            //Debug.Log(xd / ((float)c));
        }

        // Update is called once per frame
        void Update()
        {
            foreach (HexTileObject waterTile in waterTiles)
            {
                waterTile.transform.position = waterTile.startPosition + Vector3.up * CalculatePerlinNoise(waterTile);
                if (waterTile.hexPosition.x == 20 && waterTile.hexPosition.y == 20)
                {
                    avarageHeight += CalculatePerlinNoise(waterTile);
                    //Debug.Log(avarageHeight);
                }
            }

            windDirection = new Vector2(
                Mathf.Cos(windDirectionAngle),
                Mathf.Sin(windDirectionAngle)) *
                windDirectionMagnitude;

            windDirectionAngle += windDirectionChangeSpeed * Time.deltaTime / 10f * speed;
            if (windDirectionAngle > Mathf.PI * 2f)
            {
                windDirectionAngle -= Mathf.PI * 2f;
            }

            currentWindVector = currentWindVector + windDirection * Time.deltaTime * speed / 10f;
        }

        private float CalculatePerlinNoise(HexTileObject waterTile)
        {
            //return (Mathf.PerlinNoise(waterTile.startPosition.x / noiseSize + Time.time * speed, waterTile.startPosition.z / noiseSize + Time.time * speed) - perlinNoiseAverage)
                //* noiseStrenght * Mathf.Pow(boardCreator.board.boardParameters.heightLevels[1] - waterTile.height, waterHeightImpact);

            return PerlinNoise3D(
                waterTile.startPosition.x / noiseSize + currentWindVector.x,
                waterTile.startPosition.z / noiseSize + currentWindVector.y,
                Time.time * speed)
                * noiseStrenght * Mathf.Pow(1 + boardCreator.board.boardParameters.heightLevels[1] - waterTile.height, waterHeightImpact);
        }

        private float PerlinNoise3D(float x, float y, float z)
        {
            float xy = Mathf.PerlinNoise(x, y) - perlinNoiseAverage;
            float xz = Mathf.PerlinNoise(x, z) - perlinNoiseAverage;
            float yz = Mathf.PerlinNoise(y, z) - perlinNoiseAverage;
            float yx = Mathf.PerlinNoise(y, x) - perlinNoiseAverage;
            float zx = Mathf.PerlinNoise(z, x) - perlinNoiseAverage;
            float zy = Mathf.PerlinNoise(z, y) - perlinNoiseAverage;

            return (xy + xz + yz + yx + zx + zy) / 6f;
        }
    }
}