using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotDecided
{
    public class PieceArrowController : MonoBehaviour
    {
        public static PieceArrowController Instance;

        [SerializeField]
        private SpriteRenderer pieceIndicator;

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
            pieceIndicator.gameObject.SetActive(false);

            LevelManager.Instance.OnLevelStarted += OnLevelStarted;
            LevelManager.Instance.OnLevelCompleted += OnLevelCompleted;
        }

        public void Show(PieceController targetPiece, Vector3 startPoint, Vector3 endPoint)
        {
            if(isDisabled)
                return;
            
            var direction = startPoint - endPoint;
            var magnitude = Mathf.Clamp(direction.magnitude, 0, targetPiece.MaxPullRange);

            if(direction.normalized != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            }

            transform.position = targetPiece.transform.position + direction.normalized * 0.7f - Vector3.up * 0.15f;;
            pieceIndicator.transform.position = targetPiece.transform.position - Vector3.up * 0.15f;

            var localScale = transform.localScale;
            localScale.z = NormalizationHelper.MinMax(0f, targetPiece.MaxPullRange, 0.7f, 1.35f, magnitude);
            transform.localScale = localScale;

            spriteGo.SetActive(true);
            pieceIndicator.gameObject.SetActive(true);
        }

        public void Hide()
        {
            spriteGo.SetActive(false);
            pieceIndicator.gameObject.SetActive(false);
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