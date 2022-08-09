using System;

namespace Modules.MiniGame.Scripts.Presentation
{
    public interface MiniGameUiView
    {
        event Action OnViewEnabled;
        void UpdateScore(float score);
        void UpdateStability(float stability, float maxStability);
        void InitScoreFeature();
        void InitStability();
        void DisposeScoreFeature();
        void DisposeStability();
        void InitView();
    }
}