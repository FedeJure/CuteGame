namespace Modules.ActorModule.Scripts.Core.Domain
{
    public class ActorSkin
    {
        public string bodySkinId;
        public string headSkinId;

        public ActorSkin(string bodySkinId, string headSkinId)
        {
            this.bodySkinId = bodySkinId;
            this.headSkinId = headSkinId;
        }
    }
}