using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float velocity;
    void Start()
    {
        LeanTween.rotateAroundLocal(gameObject, Vector3.forward, 360, 360 / velocity).setLoopCount(0);
    }
}
