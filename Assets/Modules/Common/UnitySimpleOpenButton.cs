using UnityEngine;
using UnityEngine.UI;

namespace Modules.Common
{
    public class UnitySimpleOpenButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private GameObject view;

        private void Awake()
        {
            button.onClick.AddListener(() => view.SetActive(true));
        }
    }
}
