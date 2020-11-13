using System;

namespace Modules.Humor.Presentation
{
    public interface HumorBarView
    {
        event Action OnViewEnable;
        event Action OnViewDisable;
        void HumorChange(int HumorLevel, int LastHumorChange);
    }
}