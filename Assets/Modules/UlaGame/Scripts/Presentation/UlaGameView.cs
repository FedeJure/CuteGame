using System;
using Modules.Common;
using Modules.MiniGame.Scripts.Presentation;

namespace Modules.UlaGame.Scripts.Presentation
{
    public interface UlaGameView : MiniGameView
    {
        event Action<TouchDirection> OnSwipeReceived;
        void SetStability(float currentStability);
        void SetStage(int stage);
        void EndGame();
    }
}