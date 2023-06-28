using Cinemachine;
using UnityEngine;

namespace Camera
{
    public abstract class ActiveCamera : MonoBehaviour
    {
        [SerializeField] protected CinemachineVirtualCamera camera;
        [SerializeField] protected int defaultPriority = 10;

        public CinemachineVirtualCamera Camera => camera;
        
        public abstract void RestoreDefault();
    }
}