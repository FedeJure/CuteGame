using System;
using Modules.Common;
using UnityEngine;

#if UNITY_EDITOR
using Input = Modules.Common.Input;
#endif

public class UnitySwipeInput : MonoBehaviour
{
        public event Action<TouchDirection> OnSwipeDetected = swipe => { };

        private Vector2 fingerDown;
        private Vector2 fingerUp;
        private bool canProcess;
        private Camera selectedCamera;
        
        HitTargetRepository hitTargetRepository;
        float SWIPE_THRESHOLD = 5f;

        private void OnEnable()
        {
            selectedCamera = CameraRepository.GetGameCurrentCamera();
            hitTargetRepository = new HitTargetRepository();
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
            if (isDiagonal && up && left)
            {
                OnSwipeDetected(TouchDirection.UpLeft);
                return;
            }

            if (isDiagonal && up && right)
            {
                OnSwipeDetected(TouchDirection.UpRight);
                return;
            }

            if (isDiagonal && down && left)
            {
                OnSwipeDetected(TouchDirection.DownLeft);
                return;
            }

            if (isDiagonal && down && right)
            {
                OnSwipeDetected(TouchDirection.DownRight);
                return;
            }

            if (!isDiagonal && isVertical && up)
            {
                OnSwipeDetected(TouchDirection.Up);
                return;
            }

            if (!isDiagonal && isVertical && down)
            {
                OnSwipeDetected(TouchDirection.Down);
                return;
            }

            if (!isDiagonal && isHorizontal && right)
            {
                OnSwipeDetected(TouchDirection.Right);
                return;
            }

            if (!isDiagonal && isHorizontal && left)
            {
                OnSwipeDetected(TouchDirection.Left);
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
}

internal class HitTargetRepository
{
    private bool targetHitted;
    private int ownerId = 0;
    public bool TargetHitted()
    {
        return targetHitted;
    }

    public bool ImOwner(int ownerId)
    {
        return ownerId == this.ownerId;
    }
    public void HitTarget(int ownerId)
    {
        if (this.ownerId != 0) return;
        this.ownerId = ownerId;
        targetHitted = true;
    }

    public void ClearHit(int ownerId)
    {
        if (ownerId != this.ownerId) return;
        targetHitted = false;
        this.ownerId = 0;
    }
}