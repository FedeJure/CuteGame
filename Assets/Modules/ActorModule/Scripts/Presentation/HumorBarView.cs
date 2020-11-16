using System;

namespace Modules.ActorModule.Scripts.Presentation
{
    public interface HumorBarView
    {
        event Action OnViewEnable;
        event Action OnViewDisable;
        void HumorChange(int humorLevel, int lastHumorChange, int maxHumor);
        void InitView(int humor, int maxHumor);
    }
}