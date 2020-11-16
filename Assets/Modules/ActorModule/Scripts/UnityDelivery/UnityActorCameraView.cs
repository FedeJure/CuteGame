using UnityEngine;

namespace Modules.ActorModule.Scripts.UnityDelivery
{
    public class UnityActorCameraView : MonoBehaviour
    {
        [SerializeField] private GameObject target;

        // private void FixedUpdate()
        // {
        //     // LookAtActor();
        // }

        public void LookAtActor()
        {
            transform.LookAt(target.transform);
        }
    }
}
