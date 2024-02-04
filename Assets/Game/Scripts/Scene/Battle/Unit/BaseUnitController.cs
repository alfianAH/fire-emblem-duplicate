using FireEmblemDuplicate.Message;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using FireEmblemDuplicate.Scene.Battle.Stage;
using FireEmblemDuplicate.Scene.Battle.Terrain;
using FireEmblemDuplicate.Scene.Battle.Unit.Enum;
using FireEmblemDuplicate.Utility;
using System.Collections.Generic;
using SuperMaxim.Messaging;
using FireEmblemDuplicate.Scene.Battle.Terrain.Enum;
using FireEmblemDuplicate.Scene.Battle.Stage.Enum;
using FireEmblemDuplicate.Scene.Battle.Weapon;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public abstract class BaseUnitController : MonoBehaviour, IBaseUnitAction, IBaseUnitInteraction
    {
        [SerializeField] private SpriteRenderer _unitTypeSpriteRenderer;
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
            /*if (unit.TerrainController.Terrain.CanBeUsed)
            {
                transform.position = unit.TerrainController.transform.position;
            }
            else
            {
                if(unit.OriginTerrainController == null) return;
                transform.position = unit.OriginTerrainController.transform.position;
            }*/
        }

        public void OnUnitClick(OnClickUnitMessage message)
        {
            if (unit.MovementSpace == 0) return;

            switch (unit.UnitPhase)
            {
                case UnitPhase.Idle:
                    unit.SetOriginTerrain(unit.TerrainController);
                    unit.SetUnitPhase(UnitPhase.OnClick);

                    // Change stage's unit and phase to send it to gameplay input
                    Messenger.Default.Publish(new ChangeCurrentUnitOnClickMessage(this));
                    Messenger.Default.Publish(new ChangeStageInPhaseMessage(InPhaseEnum.OnClickUnit));
                    break;

                case UnitPhase.OnClick:
                    unit.SetUnitPhase(UnitPhase.ConfirmOnClick);
                    break;

                case UnitPhase.ConfirmOnClick:
                    unit.SetUnitPhase(UnitPhase.Immovable);
                    Messenger.Default.Publish(new DeactivateTerrainIndicatorMessage());
                    break;

                default: break;
            }
            InspectUnitMovementArea();
        }

        public void OnStartDragUnit(OnStartDragUnitMessage message)
        {
            if (message.SelectedObject != gameObject) return;
            _dragUnitCoroutine = StartCoroutine(DragUpdate(message.SelectedObject, message.PositionValue));
            unit.SetUnitPhase(UnitPhase.OnDrag);
        }

        public void OnEndDragUnit(OnEndDragUnitMessage message)
        {
            if (_dragUnitCoroutine == null) return;
            StopCoroutine(_dragUnitCoroutine);
            _dragUnitCoroutine = null;

            Move();

            // If on dragging and the terrain is the same on end drag, unit can still move
            if (unit.TerrainController != unit.OriginTerrainController)
                unit.SetUnitPhase(UnitPhase.Immovable);
            else
                unit.SetUnitPhase(UnitPhase.Idle);

            Messenger.Default.Publish(new DeactivateTerrainIndicatorMessage());
        }

        public void MoveUnitOnClickedTerrain(OnClickTerrainMessage message)
        {
            unit.SetTerrain(message.TerrainController);
            unit.SetUnitPhase(UnitPhase.ConfirmOnClick);
            Move();
            Messenger.Default.Publish(new ChangeStageInPhaseMessage(InPhaseEnum.Idle));
        }

        protected virtual void SetupUnit()
        {
            // Set terrain on start because terrain is made on awake
            CheckTerrain();
            Move();
            SetUnitTypeSprite();
        }

        /// <summary>
        /// Check terrain where unit is on
        /// Used on Start and Update on drag
        /// </summary>
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
        /// Publish message to terrain which terrain that available to be used to move around
        /// </summary>
        /// <param name="terrainPoints"></param>
        protected virtual void SetTerrainAsMovementArea(List<Vector2> terrainPoints)
        {
            if (unit.UnitPhase == UnitPhase.OnClick)
            {
                foreach (Vector2 terrainPoint in terrainPoints)
                {
                    Messenger.Default.Publish(new ChangeTerrainIndicatorMessage(
                        (int)terrainPoint.x, (int)terrainPoint.y, TerrainIndicator.MovementArea));
                }
            }
        }

        private void SetUnitTypeSprite()
        {
            _unitTypeSpriteRenderer.sprite = unit.UnitTypeSprite;
        }

        private void InspectUnitMovementArea()
        {
            if (unit.UnitPhase == UnitPhase.Immovable) return;

            int currentXPos = unit.TerrainController.Terrain.XPos;
            int currentYPos = unit.TerrainController.Terrain.YPos;

            List<Vector2> terrainPoints = RhombusPoints.GeneratePointsInsideRhombus(
                new Vector2(currentXPos, currentYPos), 
                unit.MovementSpace
            );

            SetTerrainAsMovementArea(terrainPoints);
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