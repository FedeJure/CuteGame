using System;
using UnityEngine;

public class UnityActorCameraView : MonoBehaviour
{
    [SerializeField] private GameObject target;

    // private void FixedUpdate()
    // {
    //     // LookAtActor();
    // }

    public void LookAtActor()
    {
        transform.LookAt(target.transform);
    }
}
