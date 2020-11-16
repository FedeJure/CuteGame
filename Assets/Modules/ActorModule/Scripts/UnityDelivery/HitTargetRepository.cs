namespace Modules.ActorModule.Scripts.UnityDelivery
{
    public class HitTargetRepository
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
}