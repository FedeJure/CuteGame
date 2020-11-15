namespace Modules.Actor.Scripts.Core.Domain
{
    public class Actor
    {
        public string name { get; }
        public long id { get; }
        public ActorSkin skin { get; }

        public Actor(long id, string name, ActorSkin skin)
        {
            this.name = name;
            this.id = id;
            this.skin = skin;
        }
    }
}