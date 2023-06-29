using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zombie : GameUnit
{
    [SerializeField] Transform raycastPoint;
    [SerializeField] ZombieStats stats;
    enum State { Move, Attack }

    State curState;
    ABSMatchUnit target;

    bool isMoving;
    private void Update()
    {
        //switch (curState)
        //{
        //    case State.Move:
        //        OnMoveState();
        //        break;
        //    case State.Attack:
        //        OnAttackState();
        //        break;
        //}
    }
    public void OnInit()
    {

    }

    public RaycastHit CastRay()
    {
        Physics.Raycast(raycastPoint.position, raycastPoint.forward, out RaycastHit hit, float.MaxValue, Util.LayerOf(Constant.Layer.MATCH_UNIT));
        return hit;
    }

    public void MoveToTarget()
    {

    }

    public void OnAttackState()
    {

    }
    void OnMoveState()
    {
        if (isMoving)
        {

        }
        else
        {
            RaycastHit hit = CastRay();
            float destination = 0;
            if (hit.collider != null)
            {
                destination = (hit.transform.position - TF.forward).z;
            }
            TF.DOMoveZ(destination, stats.MoveSpeed).SetSpeedBased(true).SetEase(Ease.Linear);
        }
    }
}
