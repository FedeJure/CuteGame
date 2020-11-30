using UnityEngine;

namespace Modules.Common
{
    public static class ActorComponentsRepository
    {
        private static Animator animator;

        public static Animator GetAnimator()
        {
            return animator;
        }

        public static void SetAnimator(Animator animator)
        {
            ActorComponentsRepository.animator = animator;
        } 
    }
}