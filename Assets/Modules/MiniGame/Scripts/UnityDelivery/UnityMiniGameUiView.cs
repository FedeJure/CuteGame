using System;
using Modules.MiniGame.Scripts.Presentation;
using UnityEngine;

namespace Modules.MiniGame.Scripts.UnityDelivery
{
    public class UnityMiniGameUiView : MonoBehaviour, MiniGameUiView
    {
        public event Action OnViewEnabled = () => { };
        [SerializeField] private UnityMiniGameStabilityComponentView stabilityComponent;
        [SerializeField] private UnityMiniGameScoreComponentView scoreComponent;

        private void Awake()
        {
            MiniGameModuleProvider.ProvidePresenterFor(this);
        }

        private void OnEnable()
        {
            OnViewEnabled();
        }

        public void UpdateScore(int score)
        {
            scoreComponent.UpdateScore(score);
        }

        public void UpdateStability(float stability, float maxStability)
        {
            stabilityComponent.UpdateStability(stability, maxStability);
        }

        public void InitScoreFeature()
        {
            scoreComponent.gameObject.SetActive(true);
        }

        public void InitStability()
        {
            stabilityComponent.gameObject.SetActive(true);
        }

        public void InitView()
        {
            stabilityComponent.gameObject.SetActive(false);
            scoreComponent.gameObject.SetActive(false);
        }
    }
}
