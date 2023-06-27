using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLineOfSight : Singleton<PlayerLineOfSight>
{

    [SerializeField] Transform[] statePositions;

    [SerializeField] float slipSpeed = 5f;

    Transform TF;
    Vector3 targetOffset;
    Quaternion targetRotate;

    public bool isTesting;
    public Camera Camera { get; private set; }

    private void Awake()
    {
        TF = transform;
        Camera = Camera.main;
    }

    private void LateUpdate()
    {
        if (!isTesting)
        {
            TF.SetPositionAndRotation(
                Vector3.Lerp(TF.position, targetOffset, Time.deltaTime * slipSpeed),
                Quaternion.Lerp(TF.rotation, targetRotate, Time.deltaTime * slipSpeed)
            );
        }
    }
    public void ChangeState(GameState state)
    {
        targetOffset = statePositions[(int)state].localPosition;
        targetRotate = statePositions[(int)state].localRotation;
        return;
    }
}
