using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Rewards
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private float jumpPowerCoin;
        private void Start()
        {
            var directionX = Random.Range(0.5f, 1f);
            var directionZ = Random.Range(0.5f, 1f);
            
            transform.DOJump(transform.position + new Vector3(directionX, 0, directionZ), jumpPowerCoin, 1, 1)
                .SetEase(Ease.InOutSine);
            
            transform.DORotate(new Vector3(90,360,0), 1.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        }
    }
}