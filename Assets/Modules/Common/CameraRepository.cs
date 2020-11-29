using UnityEngine;

namespace Modules.Common
{
    public class CameraRepository
    {
        private static Camera gameCurrentCamera;
        public static Camera GetGameCurrentCamera()
        {
            return gameCurrentCamera;
        }

        public static void SetGameCurrentCamera(Camera camera)
        {
            gameCurrentCamera = camera;
        }
    }
}