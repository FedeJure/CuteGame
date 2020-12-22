using TMPro;
using UnityEngine;

namespace Modules.MiniGame.Scripts.UnityDelivery
{
    public class UnityMiniGameScoreComponentView: MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;


        public void UpdateScore(int score)
        {
            scoreText.text = score.ToString();
        }
    }
}