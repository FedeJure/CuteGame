using UnityEngine;
using UnityEngine.UI;

namespace Modules.MiniGame.Scripts.UnityDelivery
{
    public class UnityMiniGameStabilityComponentView: MonoBehaviour
    {
        [SerializeField] private Slider slider;

        public void UpdateStability(float stability, float maxStability)
        {
            slider.value = stability / maxStability;
        }
    }
}