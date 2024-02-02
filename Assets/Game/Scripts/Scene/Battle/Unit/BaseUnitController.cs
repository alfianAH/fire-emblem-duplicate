using FireEmblemDuplicate.Message;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using FireEmblemDuplicate.Scene.Battle.Stage;
using FireEmblemDuplicate.Scene.Battle.Terrain;
using FireEmblemDuplicate.Scene.Battle.Unit.Enum;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public abstract class BaseUnitController : MonoBehaviour, IBaseUnitAction, IBaseUnitInteraction
    {
        [SerializeField, Range(0f, 1f)] private float _mouseDragSpeed = 0.01f;
        [SerializeField] private LayerMask _terrainLayer = -1;

        protected BaseUnit unit;
        private Camera _mainCamera;
        private Coroutine _dragUnitCoroutine;
        private StageController _stageController;
        private Vector2 _velocity = Vector2.zero;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _stageController = StageController.Instance;
            unit = GetComponent<BaseUnit>();
        }

        private void Start()
        {
            SetupUnit();
        }

        private void Update()
        {
            // When unit is on drag, get the terrain below unit until unit is stopped being dragged
            if (unit.UnitPhase != UnitPhase.OnDrag) return;
            CheckTerrain();
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Move()
        {
            transform.position = unit.TerrainController.transform.position;
        }

        public void OnUnitClick(OnClickUnit message)
        {
            if (unit.MovementSpace == 0) return;
            unit.SetUnitPhase(UnitPhase.OnClick);
        }

        public void OnStartDragUnit(OnStartDragUnit message)
        {
            if (message.SelectedObject != gameObject) return;
            _dragUnitCoroutine = StartCoroutine(DragUpdate(message.SelectedObject, message.PositionValue));
            unit.SetUnitPhase(UnitPhase.OnDrag);
        }

        public void OnEndDragUnit(OnEndDragUnit message)
        {
            if (_dragUnitCoroutine == null) return;
            StopCoroutine(_dragUnitCoroutine);

            Move();
            unit.SetUnitPhase(UnitPhase.Idle);
        }

        protected virtual void SetupUnit()
        {
            // Set terrain on start because terrain is made on awake
            CheckTerrain();
            Move();
        }

        protected virtual void CheckTerrain()
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 1f, _terrainLayer);

            if (!hit)
            {
                Debug.LogError("Can't find terrain");
                return;
            }

            if (!hit.collider.gameObject.TryGetComponent<BaseTerrainController>(out var terrainController))
            {
                Debug.LogError($"{hit.collider.gameObject.name} don't have BaseTerrainController");
                return;
            }

            unit.SetTerrain(terrainController);
        }

        /// <summary>
        /// Drag selected object to position
        /// </summary>
        /// <param name="selectedObject"></param>
        /// <param name="positionValue"></param>
        /// <returns></returns>
        private IEnumerator DragUpdate(GameObject selectedObject, float positionValue)
        {
            Vector3 objPosition = selectedObject.transform.position;
            float initialDistance = Vector2.Distance(objPosition, _mainCamera.transform.position);

            while (positionValue != 0f)
            {
                Vector3 mousePosition = Mouse.current.position.ReadValue();
                Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

                Vector2 newPosition = Vector2.SmoothDamp(selectedObject.transform.position, ray.GetPoint(initialDistance), ref _velocity, _mouseDragSpeed);

                if (!_stageController.AllowedArea.Contains(newPosition))
                {
                    newPosition.x = Mathf.Clamp(newPosition.x, _stageController.AllowedArea.xMin, _stageController.AllowedArea.xMax);
                    newPosition.y = Mathf.Clamp(newPosition.y, _stageController.AllowedArea.yMin, _stageController.AllowedArea.yMax);
                }

                selectedObject.transform.position = newPosition;

                yield return null;
            }
        }
    }
}