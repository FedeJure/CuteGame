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

        [SerializeField] private Animator ulaAnimator;
        [SerializeField] private RuntimeAnimatorController actorAnimatorController;
        [SerializeField] private UnitySwipeInput swipeInput;

        private Animator actorAnimator;
        private int rotationVelocityKey = Animator.StringToHash("rotationVelocity");
        private int stabilityFactorKey = Animator.StringToHash("stabilityFactor");
        private int actorVelocityKey = Animator.StringToHash("velocity");
        private int actorNormalKey = Animator.StringToHash("normal");
        private int actorHappyKey = Animator.StringToHash("happy");
        private int actorScaredKey = Animator.StringToHash("scared");

        private int[] facesKey = new int[3];
        
        private float stabilityLimit;

        private void Awake()
        {
            UlaGameModuleProvider.ProvidePresenterFor(this);
            swipeInput.OnSwipeDetected += OnSwipeReceived.Invoke;
            actorAnimator = ActorComponentsRepository.GetAnimator();
            actorAnimator.runtimeAnimatorController = actorAnimatorController;

            facesKey[0] = actorHappyKey;
            facesKey[1] = actorNormalKey;
            facesKey[2] = actorScaredKey;
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
            ulaAnimator.SetLayerWeight(1, Math.Abs(currentStability / stabilityLimit));
            UpdateVelocity(currentStability);
            UpdateFace(currentStability);
        }

        private void UpdateVelocity(float currentStability)
        {
            var velocity = (-1 / stabilityLimit) * Math.Abs(currentStability) + 3;
            ulaAnimator.SetFloat(rotationVelocityKey, velocity);
            actorAnimator.SetFloat(actorVelocityKey, velocity);
        }

        private void UpdateFace(float currentStability)
        {
            var normalizedStability = Math.Abs((int)((currentStability / stabilityLimit) * facesKey.Length));
            if (normalizedStability >= facesKey.Length) return;
            actorAnimator.SetTrigger(facesKey[normalizedStability]);
        }

        public void SetStage(int stage)
        {
            // Debug.Log($"Current stage: {stage}");
        }

        public void EndGame()
        {
            Debug.Log("GameEnded");
            Destroy(gameObject);
        }

        public void Init(float stabilityLimit)
        {
            this.stabilityLimit = stabilityLimit;
        }
    }
}