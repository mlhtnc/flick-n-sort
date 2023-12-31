using NotDecided.InputManagament;
using NotDecided.ObjectPooling;
using UnityEngine;

namespace NotDecided
{
    public class PieceController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerDragHandler
    {
        [SerializeField]
        private ColorType colorType;

        [SerializeField]
        private float maxPullRange;

        [SerializeField]
        private LayerMask layerMask;

        private Rigidbody rgBody;

        private Vector3 pointerDownPoint;

        private bool isMoving;

        public bool IsPieceInCorrectPlace { get; set; }

        public float MaxPullRange => maxPullRange;

        public ColorType ColorType => colorType;

        private void Start()
        {
            var gameManager = GameManager.Instance;
            var renderer    = GetComponent<Renderer>();
            rgBody          = GetComponent<Rigidbody>();

            renderer.material.color = gameManager.GetColorByType(colorType);
        }

        private void Update()
        {
            isMoving = rgBody.velocity.magnitude > 0f;
            IsPieceInCorrectPlace = CheckIfPieceInCorrectPlace();
        }

        private bool CheckIfPieceInCorrectPlace()
        {
            var ray = new Ray(transform.position, -transform.up);
            Physics.Raycast(
                ray,
                out RaycastHit hit,
                100,
                layerMask
            );

            if(hit.collider != null)
            {
                if(hit.transform.GetComponent<PartController>().ColorType == colorType)
                    return true;
            }
            else
            {
                Debug.LogError("Something wrong here, piece should be above some collider always");
                return false;
            }

            return false;
        }

        public void OnPointerDown(Vector3 pos)
        {
            pointerDownPoint = transform.position;//pos;
        }

        public void OnPointerDrag(Vector3 pos)
        {
            if(isMoving)
                return;
            
            // Make sure they are in the same plane
            pos = new Vector3(pos.x, pointerDownPoint.y, pos.z);

            PieceArrowController.Instance.Show(this, pointerDownPoint, pos);
        }

        public void OnPointerUp(Vector3 pos)
        {
            PieceArrowController.Instance.Hide();
            
            if(isMoving)
                return;
            
            // Make sure they are in the same plane
            var pointerUpPoint = new Vector3(pos.x, pointerDownPoint.y, pos.z);

            var direction = pointerDownPoint - pointerUpPoint;
            var magnitude = Mathf.Clamp(direction.magnitude, 0, maxPullRange);

            if(magnitude < 0.9f)
                return;

            rgBody.AddForce(direction.normalized * magnitude * 250, ForceMode.Force);

            TutorialManager.Instance.OnPieceMoved();
            VibrationManager.Instance.VibratePop();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var collidedPiece = collision.gameObject.GetComponent<PieceController>();
            if(collidedPiece != null && gameObject.GetInstanceID() < collision.gameObject.GetInstanceID())
            {
                var particleGo = PoolManager.Spawn(GameManager.Instance.CollisionParticlePrefab, collision.GetContact(0).point, Quaternion.identity);
                var colParticleController = particleGo.GetComponent<CollisionParticleController>();

                AudioManager.Instance.Play(GameManager.Instance.CollisionAudioClip);

                colParticleController.Play(() =>
                {
                    PoolManager.Despawn(particleGo);
                });
            }
        }
    }
}
