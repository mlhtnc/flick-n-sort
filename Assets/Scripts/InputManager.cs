using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NotDecided
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

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

        }

        private void Update()
        {
            var down = Input.GetMouseButtonDown(0);
            var up = Input.GetMouseButtonUp(0);
            var drag = Input.GetMouseButton(0);

            if(down)
            {
                OnMouseDown();
            }

            if(up)
            {
                OnMouseUp();
            }

            if(drag)
            {
                OnMouseDrag();
            }
        }

        private void OnMouseDrag()
        {
            Debug.Log("down");
        }

        private void OnMouseUp()
        {
            Debug.Log("up");
        }

        private void OnMouseDown()
        {
            Debug.Log("drag");
        }
    }
}
