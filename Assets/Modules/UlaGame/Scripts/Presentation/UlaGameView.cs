using Modules.MiniGame.Scripts.Presentation;

namespace Modules.UlaGame.Scripts.Presentation
{
    public interface UlaGameView : MiniGameView
    {
        void SetStability(float currentStability);
        void SetStage(int stage);
    }
}