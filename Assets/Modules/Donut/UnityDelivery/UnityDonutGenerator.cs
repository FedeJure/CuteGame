using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Modules.Donut.UnityDelivery
{
    public class UnityDonutGenerator : MonoBehaviour
    {
        const int normalPlaneSize = 10; //with scale 1, have 10x10 of area
        [SerializeField] UnityDonutView donutModel;
        [SerializeField] Transform planeTransformFromGenerate;
    
        List<IDisposable> disposables = new List<IDisposable>();

        private void OnEnable()
        {
            
            Observable.Interval(TimeSpan.FromMilliseconds(500))
                .Do(_ => CreateDonut())
                .Subscribe()
                .AddTo(disposables);
        }

        private void OnDisable()
        {
            disposables.ForEach(d => d.Dispose());
        }

        private void CreateDonut()
        {
            var currentPosition = planeTransformFromGenerate.position;
            var currentScale = planeTransformFromGenerate.localScale;
            var height = currentScale.z * normalPlaneSize;
            var width = currentScale.x * normalPlaneSize;
            var location = new Vector3(Random.Range(currentPosition.x - width / 2, currentPosition.x + width / 2),
                currentPosition.y, Random.Range(currentPosition.z - height / 2, currentPosition.z + height / 2));
            Instantiate(donutModel, location, Quaternion.identity, transform);
        }
    }
}
