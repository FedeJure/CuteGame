using System;
using Modules.Actor.Scripts.Presentation;
using Modules.Actor.Scripts.Presentation.Events;
using UnityEngine;
#if UNITY_EDITOR
using Input = InputWrapper.Input;
#endif


namespace Modules.Actor.Scripts.UnityDelivery
{
    public class UnityTouchHelperView: MonoBehaviour, TouchHelperView
    {
        public event Action OnViewEnabled = () => { };
        public event Action<TouchDirection> OnSwipeAction = direction => { };

        private Vector2 fingerDown;
        private Vector2 fingerUp;
        [SerializeField] GameObject target;
        [SerializeField] Camera camera;

        bool hitTarget;
        float SWIPE_THRESHOLD = 20f;

        private void Awake()
        {
            ModuleProvider.ProvidePresenterFor(this);
        }
        

        private void OnEnable()
        {
            OnViewEnabled();
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
                        hitTarget = false;
                        break;
                    case TouchPhase.Moved:
                    {
                        CheckRaycasting(touch);
                        break;
                    }
                    case TouchPhase.Ended:
                        fingerDown = touch.position;
                        checkSwipe();
                        hitTarget = false;
                        break;
                }
            }
        }

        private void CheckRaycasting(Touch touch)
        {
            if (hitTarget) return;
            Ray raycast = camera.ScreenPointToRay(touch.position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit) && raycastHit.transform.gameObject.Equals(target))
            {
                hitTarget = true;
            }
        }

        void checkSwipe()
        {
            if (!hitTarget) return;
            var isVertical = verticalMove() > SWIPE_THRESHOLD;
            var isHorizontal = horizontalValMove() > SWIPE_THRESHOLD;
            var isDiagonal = isVertical && isHorizontal;
            var up = fingerDown.y - fingerUp.y > 0;
            var down = fingerDown.y - fingerUp.y < 0;
            var left = fingerDown.x - fingerUp.x < 0;
            var right = fingerDown.x - fingerUp.x > 0;

            if (isDiagonal && up && left)
            {
                OnSwipeAction(TouchDirection.UpLeft);
                return;
            }

            if (isDiagonal && up && right)
            {
                OnSwipeAction(TouchDirection.UpRight);
                return;
            }

            if (isDiagonal && down && left)
            {
                OnSwipeAction(TouchDirection.DownLeft);
                return;
            }

            if (isDiagonal && down && right)
            {
                OnSwipeAction(TouchDirection.DownRight);
                return;
            }

            if (isVertical && up)
            {
                OnSwipeAction(TouchDirection.Up);
                return;
            }

            if (isVertical && down)
            {
                OnSwipeAction(TouchDirection.Down);
                return;
            }

            if (isHorizontal && right)
            {
                OnSwipeAction(TouchDirection.Right);
                return;
            }

            if (isHorizontal && left)
            {
                OnSwipeAction(TouchDirection.Left);
                return;
            }
            
            fingerUp = fingerDown;
        }

        float verticalMove()
        {
            return Mathf.Abs(fingerDown.y - fingerUp.y);
        }

        float horizontalValMove()
        {
            return Mathf.Abs(fingerDown.x - fingerUp.x);
        }
    }
}