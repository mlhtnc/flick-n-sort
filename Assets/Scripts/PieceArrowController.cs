using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotDecided
{
    public class PieceArrowController : MonoBehaviour
    {
        public static PieceArrowController Instance;

        private GameObject spriteGo;

        private bool isDisabled;

        private void Awake()
        {
            if(Instance != null)
            {
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            spriteGo = transform.GetChild(0).gameObject;

            spriteGo.SetActive(false);

            LevelManager.Instance.OnLevelStartedEvent += OnLevelStarted;
            LevelManager.Instance.OnLevelCompletedEvent += OnLevelCompleted;
        }

        public void Show(PieceController targetPiece, Vector3 startPoint, Vector3 endPoint)
        {
            if(isDisabled)
                return;
            
            var direction = startPoint - endPoint;
            var magnitude = Mathf.Clamp(direction.magnitude, 0, targetPiece.MaxPullRange);

            transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            transform.position = targetPiece.transform.position + direction.normalized * 0.7f;

            var localScale = transform.localScale;
            localScale.z = NormalizationHelper.MinMax(0f, targetPiece.MaxPullRange, 0.7f, 1.35f, magnitude);
            transform.localScale = localScale;

            spriteGo.SetActive(true);
        }

        public void Hide()
        {
            spriteGo.SetActive(false);
        }

        private void OnLevelStarted()
        {
            isDisabled = false;
        }

        private void OnLevelCompleted()
        {
            Hide();
            isDisabled = true;
        }
    }
}