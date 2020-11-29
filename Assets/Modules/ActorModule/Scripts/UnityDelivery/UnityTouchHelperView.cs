using System;
using System.Collections.Generic;
using Modules.ActorModule.Scripts.Presentation;
using Modules.ActorModule.Scripts.Presentation.Events;
using Modules.Common;
using UnityEngine;
#if UNITY_EDITOR
using Input = InputWrapper.Input;
#endif


namespace Modules.ActorModule.Scripts.UnityDelivery
{
    public class UnityTouchHelperView: MonoBehaviour, TouchHelperView
    {
        public event Action OnViewEnabled = () => { };
        public event Action<ActorInteraction> OnActorInteraction = interaction => { };

        private Vector2 fingerDown;
        private Vector2 fingerUp;
        private bool canProcess;
        [SerializeField] Camera selectedCamera;
        [SerializeField] List<TouchAction> touchAction;
        
        HitTargetRepository hitTargetRepository;
        float SWIPE_THRESHOLD = 5f;

        private void Awake()
        {
            ActorModuleProvider.ProvidePresenterFor(this);
        }

        private void OnEnable()
        {
            OnViewEnabled();
            hitTargetRepository = ActorModuleProvider.ProvideHitTargetRepository();
            canProcess = true;
        }

        void Update()
        {
            foreach (Touch touch in Input.touches)
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        fingerUp = touch.position;
                        fingerDown = touch.position;
                        canProcess = true;
                        break;
                    case TouchPhase.Moved:
                    {
                        CheckRaycasting(touch);
                        checkSwipe();
                        fingerDown = touch.position;
                        break;
                    }
                    case TouchPhase.Ended:
                        fingerDown = touch.position;
                        checkSwipe();
                        hitTargetRepository.ClearHit(GetInstanceID());
                        break;
                }
            }
        }

        void CheckRaycasting(Touch touch)
        {
            if (hitTargetRepository.TargetHitted()) return;
            Ray raycast = selectedCamera.ScreenPointToRay(touch.position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit) && raycastHit.collider.gameObject.GetInstanceID().Equals(gameObject.GetInstanceID()))
            {
                hitTargetRepository.HitTarget(GetInstanceID());
            }
        }

        void checkSwipe()
        {
            if (!hitTargetRepository.ImOwner(GetInstanceID()) || !canProcess) return;
            var isVertical = VerticalMove() > SWIPE_THRESHOLD && HorizontalValMove() <= SWIPE_THRESHOLD;
            var isHorizontal = HorizontalValMove() > SWIPE_THRESHOLD &&  VerticalMove() <= SWIPE_THRESHOLD;
            var isDiagonal = HorizontalValMove() > SWIPE_THRESHOLD && VerticalMove() > SWIPE_THRESHOLD;
            var up = fingerDown.y - fingerUp.y > 0;
            var down = fingerDown.y - fingerUp.y < 0;
            var left = fingerDown.x - fingerUp.x < 0;
            var right = fingerDown.x - fingerUp.x > 0;
            
            // Debug.LogWarning($"isVertical: {isVertical}, isHorizontal:{isHorizontal}, isDiagonal:{isDiagonal}, up:{up}, down:{down}, left:{left}, rigth:{right}");
            if (IsDirection(TouchDirection.UpLeft) && isDiagonal && up && left)
            {
                SendInteraction(GetAction(TouchDirection.UpLeft));
                return;
            }

            if (IsDirection(TouchDirection.UpRight) && isDiagonal && up && right)
            {
                SendInteraction(GetAction(TouchDirection.UpRight));
                return;
            }

            if (IsDirection(TouchDirection.DownLeft) && isDiagonal && down && left)
            {
                SendInteraction(GetAction(TouchDirection.DownLeft));
                return;
            }

            if (IsDirection(TouchDirection.DownRight) && isDiagonal && down && right)
            {
                SendInteraction(GetAction(TouchDirection.DownRight));
                return;
            }

            if (IsDirection(TouchDirection.Up) && !isDiagonal && isVertical && up)
            {
                SendInteraction(GetAction(TouchDirection.Up));
                return;
            }

            if (IsDirection(TouchDirection.Down) && !isDiagonal && isVertical && down)
            {
                SendInteraction(GetAction(TouchDirection.Down));
                return;
            }

            if (IsDirection(TouchDirection.Right) && !isDiagonal && isHorizontal && right)
            {
                SendInteraction(GetAction(TouchDirection.Right));
                return;
            }

            if (IsDirection(TouchDirection.Left) && !isDiagonal && isHorizontal && left)
            {
                SendInteraction(GetAction(TouchDirection.Left));
                return;
            }
            
            fingerUp = fingerDown;
        }

        float VerticalMove()
        {
            return Mathf.Abs(fingerDown.y - fingerUp.y);
        }

        float HorizontalValMove()
        {
            return Mathf.Abs(fingerDown.x - fingerUp.x);
        }

        bool IsDirection(TouchDirection direction)
        {
            return touchAction.Exists(action => action.workOnDirections.Contains(direction));
        }
        
        ActorInteraction GetAction(TouchDirection direction)
        {
            return touchAction.Find(action => action.workOnDirections.Contains(direction)).interactionResult;
        }

        void SendInteraction(ActorInteraction interaction)
        {
            OnActorInteraction(interaction);
            canProcess = false;
        }
    }
}