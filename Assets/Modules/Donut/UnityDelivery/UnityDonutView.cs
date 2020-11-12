using System;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnityDonutView : MonoBehaviour
{
    public ISubject<Unit> OnDonutRecicle = new Subject<Unit>(); 
    [SerializeField] private Transform transform;

    private Vector3 rotationAxis;
    [SerializeField]private float minfallVelocity = 10;
    [SerializeField]private float minRotationVelocity = 50;
    [SerializeField]private float maxfallVelocity = 50;
    [SerializeField]private float maxRotationVelocity = 100;

    private float fallVelocity;
    private float rotationVelocity;

    private IDisposable disposer;
    private void OnEnable()
    {
        rotationAxis = new Vector3(Random.Range(1f, 90f), Random.Range(1f, 90f), Random.Range(1f, 90f));
        fallVelocity = Random.Range(minfallVelocity, maxfallVelocity);
        rotationVelocity = Random.Range(minRotationVelocity, maxRotationVelocity);

        var life =Math.Floor(Math.Abs(transform.position.y * 1.2) / fallVelocity);
        disposer = Observable.Timer(TimeSpan.FromSeconds(life))
            .Do(_ => RecicleDonut())
            .Subscribe();
    }

    private void OnDisable()
    {
        disposer.Dispose();
    }

    private void RecicleDonut()
    {
        disposer.Dispose();
        OnDonutRecicle.OnNext(Unit.Default);
    }

    private void Update()
    {
        float deltaTime = Time.deltaTime;
        transform.Rotate(rotationAxis, rotationVelocity * deltaTime, Space.World);
        transform.Translate(Vector3.down * (fallVelocity * deltaTime), Space.World);
    }
}
