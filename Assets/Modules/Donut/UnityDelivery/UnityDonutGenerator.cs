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
        [SerializeField] private int maxDonutGeneration = 50;
        private int generatedDonuts;
        Queue<UnityDonutView> availableDonut = new Queue<UnityDonutView>();

        List<IDisposable> disposables = new List<IDisposable>();
        private Vector3 currentPosition;
        private Vector3 currentScale;
        private float height;
        private float width;

        private void Start()
        {
            currentPosition = planeTransformFromGenerate.position;
            currentScale = planeTransformFromGenerate.localScale;
            height = currentScale.z * normalPlaneSize;
            width = currentScale.x * normalPlaneSize;
            
            for (int i = 0; i < maxDonutGeneration / 2 ; i++)
            {
                CreateDonut(GetStartLocationRandom());
            }
        }
        private void OnEnable()
        {
            Observable.Interval(TimeSpan.FromMilliseconds(500))
                .Do(_ => SpawnDonut())
                .Subscribe()
                .AddTo(disposables);
        }

        private void OnDisable()
        {
            disposables.ForEach(d => d.Dispose());
        }

        private void SpawnDonut()
        {
            if (generatedDonuts < maxDonutGeneration)
            {
                CreateDonut(GetStartLocationAtTop());
                return;
            }

            if (availableDonut.Count <= 0) return;
            var donut = availableDonut.Dequeue();
            var location = GetStartLocationAtTop();
            donut.transform.SetPositionAndRotation(location, Quaternion.identity);
            donut.gameObject.SetActive(true);
        }

        private void CreateDonut(Vector3 location)
        {
            UnityDonutView donut = Instantiate(donutModel, location, Quaternion.identity, transform);
            generatedDonuts++;
            donut.OnDonutRecicle
                .Do(_ =>
                {
                    availableDonut.Enqueue(donut);
                    donut.gameObject.SetActive(false);
                })
                .Subscribe()
                .AddTo(disposables);
        }

        private Vector3 GetStartLocationRandom()
        {
            float startTop = transform.position.y;
            return new Vector3(Random.Range(currentPosition.x - width / 2, currentPosition.x + width / 2),
                Random.Range(0, startTop), Random.Range(currentPosition.z - height / 2, currentPosition.z + height / 2));
        }
        
        private Vector3 GetStartLocationAtTop()
        {
            return new Vector3(Random.Range(currentPosition.x - width / 2, currentPosition.x + width / 2),
                transform.position.y, Random.Range(currentPosition.z - height / 2, currentPosition.z + height / 2));
        }
    }
}
