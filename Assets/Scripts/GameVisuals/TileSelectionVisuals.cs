using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace PlayerInterface
{
    public class TileSelectionVisuals : MonoBehaviour
    {
        public List<Highlights> highlights;
        public HexTileObject[,] hexTiles;

        public void HighLightTile(HexVector tilePosition, TileHighlightType highlightType)
        {
            if (hexTiles[tilePosition.x, tilePosition.y] == null)
            {
                return;
            }
            if (highlightType == TileHighlightType.NoHighlight)
            {
                hexTiles[tilePosition.x, tilePosition.y].selectionSprite.color = new Color(0, 0, 0, 0);
                hexTiles[tilePosition.x, tilePosition.y].glowSelectionSprite.color = new Color(0, 0, 0, 0);
                return;
            }
            foreach (var tile in highlights)
            {
                if (tile.tileHighlightType == highlightType)
                {
                    hexTiles[tilePosition.x, tilePosition.y].selectionSprite.color = tile.color;
                    hexTiles[tilePosition.x, tilePosition.y].glowSelectionSprite.color = new Color(1, 1, 1, tile.color.a);
                    break;
                }
            }
        }

        public void ChangeTilesColor(List<HexVector> tilesPositions, TileHighlightType highlightType)
        {
            foreach (HexVector tilePosition in tilesPositions)
            {
                ChangeTilesColor(tilesPositions, highlightType);
            }
        }
    }

    public enum TileHighlightType
    {
        NoHighlight,
        Hover,
        Selection,
        PossibleMove,
        Spell
    }

    [System.Serializable]
    public struct Highlights
    {
        public TileHighlightType tileHighlightType;
        public Color color;
    }
}