using Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerTrackingCamera : MonoBehaviour
    {
        private void Start()
        {
            var camera = FindObjectOfType<CinemachineVirtualCamera>();
            camera.m_Follow = transform;
            camera.m_LookAt = transform;
        }
    }
}