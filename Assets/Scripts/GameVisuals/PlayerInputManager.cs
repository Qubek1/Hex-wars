using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace PlayerInterface
{
    public class PlayerInputManager : MonoBehaviour
    {
        private OnMouseHoverCollider currentOnMouseHoverCollider;

        public HexVector mouseHexTile { get; private set; }
        public HexVector lastMouseHexTile { get; private set; }

        public bool mouseTileChanged { get; private set; }

        private void Update()
        {
            lastMouseHexTile = mouseHexTile;
            mouseTileChanged = false;
            if (OnMouseHoverCollider.exitHoveredCollider != null)
            {
                if (currentOnMouseHoverCollider == OnMouseHoverCollider.exitHoveredCollider)
                {
                    currentOnMouseHoverCollider = null;
                }
                //HexTileObject unSelectedTileObject = OnMouseHoverCollider.exitHoveredCollider.transform.parent.GetComponent<HexTileObject>();
                OnMouseHoverCollider.exitHoveredCollider = null;
            }
            if (OnMouseHoverCollider.enterHoveredCollider != null)
            {
                HexTileObject selectedTileObject = OnMouseHoverCollider.enterHoveredCollider.transform.parent.GetComponent<HexTileObject>();
                mouseHexTile = selectedTileObject.hexPosition;
                currentOnMouseHoverCollider = OnMouseHoverCollider.enterHoveredCollider;
                OnMouseHoverCollider.enterHoveredCollider = null;
            }
            if (Input.GetMouseButtonDown(0) && currentOnMouseHoverCollider != null)
            {
                HexVector position = currentOnMouseHoverCollider.transform.parent.GetComponent<HexTileObject>().hexPosition;
                //Debug.Log(position);
            }

            if (lastMouseHexTile != mouseHexTile)
            {
                mouseTileChanged = true;
            }
        }
    }
}