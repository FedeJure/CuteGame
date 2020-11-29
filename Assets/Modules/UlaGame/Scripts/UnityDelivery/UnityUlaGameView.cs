using System;
using Modules.Common;
using Modules.UlaGame.Scripts.Presentation;
using UnityEngine;

namespace Modules.UlaGame.Scripts.UnityDelivery
{
    public class UnityUlaGameView: MonoBehaviour, UlaGameView
    {
        public event Action OnViewEnabled = () => { };
        public event Action OnViewDisabled = () => { };
        public event Action<TouchDirection> OnSwipeReceived = action => { };


        private void Awake()
        {
            UlaGameModuleProvider.ProvidePresenterFor(this);
        }

        private void OnEnable()
        {
            OnViewEnabled();
        }

        private void OnDisable()
        {
            OnViewDisabled();
        }

        public void SetStability(float currentStability)
        {
            Debug.Log($"stability: {currentStability}");
        }

        public void SetStage(int stage)
        {
            Debug.Log($"Current stage: {stage}");
        }

        public void EndGame()
        {
            Debug.Log("GameEnded");
        }
    }
}