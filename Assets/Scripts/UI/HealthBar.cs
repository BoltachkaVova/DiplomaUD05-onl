using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        private Transform _cameraTransform;
        private CanvasGroup _canvasGroup;
        private Image _bar;
        
        private void Awake()
        {
            _bar = GetComponentInChildren<BarImage>().GetComponent<Image>();
            _cameraTransform = FindObjectOfType<Camera>().transform;
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            _bar.fillAmount = 1;
        }

        public async UniTaskVoid Show(float countHealth)
        {
            transform.rotation = Quaternion.LookRotation(_cameraTransform.forward);
            await _canvasGroup.DOFade(1, 0.5f);
            await _bar.DOFillAmount(countHealth, 0.5f);
            await _canvasGroup.DOFade(0, 0.5f);
        }
    }
}