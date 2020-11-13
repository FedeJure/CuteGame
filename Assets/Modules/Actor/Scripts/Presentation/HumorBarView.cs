using System;

namespace Modules.Actor.Scripts.Presentation
{
    public interface HumorBarView
    {
        event Action OnViewEnable;
        event Action OnViewDisable;
        void HumorChange(int humorLevel, int lastHumorChange, int maxHumor);
    }
}