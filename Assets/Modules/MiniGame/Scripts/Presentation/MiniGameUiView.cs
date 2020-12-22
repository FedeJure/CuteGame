using System;

namespace Modules.MiniGame.Scripts.Presentation
{
    public interface MiniGameUiView
    {
        event Action OnViewEnabled;
        void UpdateScore(int score);
        void UpdateStability(float stability, float maxStability);
        void InitScoreFeature();
        void InitStability();
        void InitView();
    }
}