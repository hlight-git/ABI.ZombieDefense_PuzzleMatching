using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchUnitGrabber : MonoBehaviour
{
    [SerializeField] MatchBoard matchBoard;
    ABSMatchUnit grabbingUnit;
    Vector2 pressDownPos;

    void Update()
    {
        if (GameManager.IsState(GameState.GamePlay) && matchBoard.IsMovable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnPressDown();
            }
            if (grabbingUnit != null && Input.GetMouseButton(0))
            {
                OnDrag();
            }
            if (Input.GetMouseButtonUp(0))
            {
                OnRelease();
            }
        }
    }
    void OnPressDown()
    {
        RaycastHit hit = CastRay();
        if (hit.collider != null && hit.collider.CompareTag(Constant.Tag.MATCH_UNIT))
        {
            pressDownPos = Input.mousePosition;
            grabbingUnit = MatchUnitCache.Get(hit.collider);
        }
    }
    void OnDrag()
    {
        Vector2 dragOffset = (Vector2)Input.mousePosition - pressDownPos;
        if (dragOffset.sqrMagnitude > Constant.Input.MIN_DRAG_OFFSET_VALUE_TO_MOVE)
        {
            matchBoard.TryMove(grabbingUnit, dragOffset);
            OnRelease();
        }
    }
    void OnRelease()
    {
        grabbingUnit = null;
    }

    RaycastHit CastRay()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 screenMousePosFar = new Vector3(mousePos.x, mousePos.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);

        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out RaycastHit hit, float.PositiveInfinity, Util.LayerOf(Constant.Layer.MATCH_UNIT));
        return hit;
    }
}
