using System;
using Modules.ActorModule.Scripts.Core.Domain;

namespace Modules.ActorModule.Scripts.Presentation
{
    public interface ActorAvatarView
    {
        event Action OnViewEnabled;
        event Action OnViewDisabled;
        void SetName(string actorName);
        void SetHumor(Humor humor);
    }
}