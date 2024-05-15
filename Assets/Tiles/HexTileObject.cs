using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInterface
{
    public class HexTileObject : MonoBehaviour
    {
        public SpriteRenderer selectionSprite;
        public SpriteRenderer glowSelectionSprite;

        public HexVector hexPosition;
        public float height;
        public int heightLevel;

        public Vector3 startPosition;

        public List<Transform> UnitPositions;

        public void Awake()
        {
            startPosition = transform.position;
            selectionSprite = GetComponentInChildren<SpriteRenderer>();
        }
    }
}