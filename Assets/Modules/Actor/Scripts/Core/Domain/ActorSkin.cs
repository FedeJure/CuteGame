namespace Modules.Actor.Scripts.Core.Domain
{
    public class ActorSkin
    {
        private string bodySkinId;
        private string headSkinId;

        public ActorSkin(string bodySkinId, string headSkinId)
        {
            this.bodySkinId = bodySkinId;
            this.headSkinId = headSkinId;
        }
    }
}