using UnityEngine;

namespace Camera
{
    public class FollowCamera : ActiveCamera
    {
        public override void RestoreDefault()
        {
            camera.Priority = defaultPriority;
            camera.LookAt = null;
            camera.Follow = null;
        }

        public void LookAt(Transform transformToLookAt) => camera.LookAt = transformToLookAt;
    }
}