using UnityEngine;

namespace NotDecided.InputManagament
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance;

        private Camera currCamera;

        [SerializeField]
        private LayerMask layerMask;

        private Transform transformAlreadyPointerDowned;

        private bool pointerWasOnUI;

        private bool isInputDisabled;

        private void Awake()
        {
            if(Instance != null)
            {
                return;
            }

            Instance = this;
            currCamera = Camera.main;
            transformAlreadyPointerDowned = null;
        }

        private void Update()
        {
            if(isInputDisabled)
                return;

            var down = Input.GetMouseButtonDown(0);
            var up = Input.GetMouseButtonUp(0);
            var drag = Input.GetMouseButton(0);

            if(down)
            {
                OnMouseDown();
            }

            if(drag)
            {
                OnMouseDrag();
            }

            if(up)
            {
                OnMouseUp();
            }
        }

        private void OnMouseDown()
        {
            Vector2 pos = Input.mousePosition;
            if(InputHelper.IsPointerOverUIObject(pos))
            {
                pointerWasOnUI = true;
                return;
            }
            
            pointerWasOnUI = false;
            RaycastHit hit;
            RaycastFromCamera(pos, out hit);

            if (hit.collider != null)
            {
                transformAlreadyPointerDowned = hit.transform;
                hit.transform.GetComponent<IPointerDownHandler>()?.OnPointerDown(hit.point);
            }
        }

        private void OnMouseDrag()
        {
            Vector2 pos = Input.mousePosition;
            RaycastHit hit;
            RaycastFromCamera(pos, out hit);

            if (transformAlreadyPointerDowned != null)
            {
                transformAlreadyPointerDowned.GetComponent<IPointerDragHandler>()?.OnPointerDrag(hit.point);
            }
        }

        private void OnMouseUp()
        {
            // If one of the pointers was on UI, do not run the rest of it
            if(pointerWasOnUI)
                return;

            Vector2 pos = Input.mousePosition;
            RaycastHit hit;

            RaycastFromCamera(pos, out hit);

            if (transformAlreadyPointerDowned != null)
            {
                transformAlreadyPointerDowned.GetComponent<IPointerUpHandler>()?.OnPointerUp(hit.point);
                // hit.transform.GetComponent<IPointerUpHandler>()?.OnPointerUp(hit.point);
            }
        }


        private void RaycastFromCamera(Vector3 pos,out RaycastHit hit)
        {
            Ray ray = currCamera.ScreenPointToRay(pos);

            // Debug.DrawRay(ray.origin, ray.direction*100, Color.red, 2f);
            Physics.Raycast(
                ray,
                out hit,
                currCamera.farClipPlane - currCamera.nearClipPlane,
                layerMask
            );
        }

        public void EnableInput()
        {
            isInputDisabled = true;
        }

        public void DisableInput()
        {
            isInputDisabled = false;
        }
    }
}
