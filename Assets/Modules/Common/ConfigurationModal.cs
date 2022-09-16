using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Modules.Common
{
    public class ConfigurationModal : MonoBehaviour
    {
        [SerializeField] private Button logOut;
        [SerializeField] private Button deleteActor;

        private void Start()
        {
            logOut.onClick.AddListener(() =>
            {
                ConfirmationModal.Open("Leave current session", "Do you want to leave the current session?")
                    .Do((ok) =>
                    {
                    }).Subscribe();
            });
            deleteActor.onClick.AddListener(() =>
            {
                ConfirmationModal.Open("Delete character", "Do you want to delete this CupCake? This action can't be reverted.")
                    .Do((ok) =>
                    {
                    }).Subscribe();
            });
        }
    }
}
