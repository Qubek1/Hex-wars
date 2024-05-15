using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseHoverCollider : MonoBehaviour
{
    public static OnMouseHoverCollider enterHoveredCollider;
    public static OnMouseHoverCollider exitHoveredCollider;

    private void OnMouseEnter()
    {
        enterHoveredCollider = this;
    }

    private void OnMouseExit()
    {
        exitHoveredCollider = this;
        if (enterHoveredCollider == this)
        {
            enterHoveredCollider = null;
        }
    }
}