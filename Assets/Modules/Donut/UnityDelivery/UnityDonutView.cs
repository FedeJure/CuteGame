using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnityDonutView : MonoBehaviour
{
    [SerializeField] private Transform transform;

    private Vector3 rotationAxis;
    private float minfallVelocity = 10;
    private float minRotationVelocity = 50;
    private float maxfallVelocity = 50;
    private float maxRotationVelocity = 100;

    private float fallVelocity;
    private float rotationVelocity;
    private float lifeSeconds = 20;

    private IDisposable disposer;
    private void Start()
    {
        rotationAxis = new Vector3(Random.Range(1f, 90f), Random.Range(1f, 90f), Random.Range(1f, 90f));
        fallVelocity = Random.Range(minfallVelocity, maxfallVelocity);
        rotationVelocity = Random.Range(minRotationVelocity, maxRotationVelocity);
        disposer = Observable.Timer(TimeSpan.FromSeconds(20))
            .Do(_ => Destroy(gameObject))
            .Subscribe();
    }

    private void OnDisable()
    {
        disposer.Dispose();
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        transform.Rotate(rotationAxis, rotationVelocity * deltaTime, Space.World);
        transform.Translate(Vector3.down * (fallVelocity * deltaTime), Space.World);
    }
}
