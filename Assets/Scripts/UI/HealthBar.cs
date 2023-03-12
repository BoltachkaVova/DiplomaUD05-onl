using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private float durationShowHealth = 2f;
        [SerializeField] private float durationFillingAmount = 0.5f;


        private float _duration;
        private Transform _cameraTransform;
        private CanvasGroup _canvasGroup;
        private Image _bar;
        
        private void Awake()
        {
            _bar = GetComponentInChildren<BarImage>().GetComponent<Image>();
            _cameraTransform = FindObjectOfType<Camera>().transform;
            _canvasGroup = GetComponent<CanvasGroup>();
            
            _bar.fillAmount = 1;
            _duration = durationShowHealth;
        }
        
        public async UniTaskVoid Show(float countHealth)
        {
            StartCoroutine(SetHealthBar());
            await _canvasGroup.DOFade(1, durationFillingAmount);
            await _bar.DOFillAmount(countHealth, durationFillingAmount);
            await _canvasGroup.DOFade(0, durationFillingAmount);
        }

        private IEnumerator SetHealthBar()
        {
            while (durationShowHealth >= 0)
            {
                durationShowHealth -= Time.deltaTime;
                transform.rotation = Quaternion.LookRotation(_cameraTransform.forward);
                yield return null; 
            }

            durationShowHealth = _duration;
            StopCoroutine(SetHealthBar());
        }
    }
}