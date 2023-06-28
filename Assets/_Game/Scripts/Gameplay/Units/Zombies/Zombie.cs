using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Zombie : GameUnit
{
    [SerializeField] Transform raycastPoint;
    enum State { Move, Attack }

    State curState;
    ABSMatchUnit target;

    private void Update()
    {
        
    }
    public void OnInit()
    {

    }

    public RaycastHit CastRay()
    {
        Physics.Raycast(raycastPoint.position, raycastPoint.forward, out RaycastHit hit, float.MaxValue, Util.LayerOf(Constant.Layer.MATCH_UNIT));
        return hit;
    }

    public void MoveTo()
    {

    }

    public void OnAttack()
    {

    }
}
