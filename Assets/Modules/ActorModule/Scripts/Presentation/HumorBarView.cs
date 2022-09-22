using System;

namespace Modules.ActorModule.Scripts.Presentation
{
    public interface HumorBarView
    {
        event Action OnViewEnable;
        event Action OnViewDisable;
        void HumorChange(float humorLevel, float lastHumorChange, float maxHumor);
        void InitView(float humor, float maxHumor);
    }
}