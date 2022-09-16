using Modules.ActorModule.Scripts;
using Modules.PlayerModule;
using Modules.Services;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UnityEngine.SceneManagement;

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
                        if (!ok) return;
                        GooglePlayServicesManager.LogOut();
                        PlayerModuleProvider.ProvidePlayerRepository().Clear();
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }).Subscribe();
            });
            deleteActor.onClick.AddListener(() =>
            {
                ConfirmationModal.Open("Delete character", "Do you want to delete this CupCake? This action can't be reverted.")
                    .Do((ok) =>
                    {
                        if (!ok) return;
                        PlayerModuleProvider.ProvidePlayerRepository().Get()
                            .Do(player =>
                            {
                                ActorModuleProvider.ProvideActorRepository().Delete(player.id)
                                    .Do((_) =>
                                    {
                                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                                    }).Subscribe();
                            });
                    }).Subscribe();
            });
        }
    }
}
