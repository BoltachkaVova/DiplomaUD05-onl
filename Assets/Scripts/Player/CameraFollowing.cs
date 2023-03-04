using System;
using Cinemachine;
using UnityEngine;

namespace Scripts
{
    public class CameraFollowing : MonoBehaviour
    {
        private void Awake()
        {
            var camera = FindObjectOfType<CinemachineVirtualCamera>();
            camera.m_Follow = transform;
            camera.m_LookAt = transform;
        }
    }
}