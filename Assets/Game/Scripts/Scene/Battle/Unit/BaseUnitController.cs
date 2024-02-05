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
using System;
using FireEmblemDuplicate.Scene.Battle.Terrain.Pool;

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
        private SpriteRenderer _spriteRenderer;
        private StageController _stageController;
        private Vector2 _velocity = Vector2.zero;

        public BaseUnit Unit => unit;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _stageController = StageController.Instance;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            unit = GetComponent<BaseUnit>();
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
            if (unit.TerrainController.Terrain.CanBeUsed)
            {
                transform.position = unit.TerrainController.transform.position;

                // Reset unit blocked terrain
                unit.RemoveBlockedTerrain();
            }
            else
            {
                if(unit.OriginTerrainController == null) return;
                
                // Set the terrain back to origin
                transform.position = unit.OriginTerrainController.transform.position;
                unit.SetTerrain(unit.OriginTerrainController);
            }
        }

        public void OnUnitClick(OnClickUnitMessage message)
        {
            if (message.ClickedUnit != this || unit.MovementSpace == 0) return;

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
                    // NOTE: CHANGE IT TO IMMOVABLE. THIS IS JUST FOR DEBUG
                    unit.SetUnitPhase(UnitPhase.Idle);
                    Messenger.Default.Publish(new ChangeStageInPhaseMessage(InPhaseEnum.Idle));
                    Messenger.Default.Publish(new DeactivateTerrainIndicatorMessage());
                    break;

                default: break;
            }
            InspectUnitMovementArea();
        }

        public void OnStartDragUnit(OnStartDragUnitMessage message)
        {
            if (message.SelectedObject != gameObject || 
                unit.UnitPhase == UnitPhase.Immovable) return;

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
                unit.SetUnitPhase(UnitPhase.Idle); // NOTE: CHANGE IT TO IMMOVABLE. THIS IS JUST FOR DEBUG
            else
                unit.SetUnitPhase(UnitPhase.Idle);

            Messenger.Default.Publish(new DeactivateTerrainIndicatorMessage());
        }

        public void MoveUnitOnClickedTerrain(OnClickTerrainMessage message)
        {
            if (StageController.Instance.CurrentUnitOnClick != this ||
                !message.TerrainController.Terrain.CanBeUsed) return;

            unit.SetTerrain(message.TerrainController);
            unit.SetUnitPhase(UnitPhase.ConfirmOnClick);
            Move();
        }

        public virtual void SetupUnit()
        {
            // Set terrain on start because terrain is made on awake
            Move();
            SetUnitTypeSprite();
            SetUnitColor();
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

        protected virtual List<UnitImpassableTerrain> ImpassableTerrains()
        {
            return new List<UnitImpassableTerrain>();
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
                    if (BlockMovement(terrainPoint)) continue;

                    SetTerrainIndicator(terrainPoint, TerrainIndicator.MovementArea);
                }
            }
        }

        private void SetTerrainAsAttackArea(List<Vector2> terrainPoints)
        {
            if (unit.UnitPhase == UnitPhase.OnClick)
            {
                foreach (Vector2 terrainPoint in terrainPoints)
                {
                    // NOTE: CEK APAKAH MASIH ADA ATTACK AREA SETELAH DIBLOCK TERRAIN
                    SetTerrainIndicator(terrainPoint, TerrainIndicator.AttackingArea);
                }
            }
        }

        protected void SetTerrainIndicator(Vector2 terrainPoint, TerrainIndicator indicator)
        {
            Messenger.Default.Publish(new ChangeTerrainIndicatorMessage(
                (int)terrainPoint.x, (int)terrainPoint.y, indicator));
        }

        private bool BlockMovement(Vector2 terrainPoint)
        {
            bool isBlocked = false;

            // If terrain is blocked, mark it as impassable
            if (unit.BlockedTerrain.Contains(terrainPoint))
            {
                isBlocked = true;
                SetTerrainIndicator(terrainPoint, TerrainIndicator.AttackingArea);
                return isBlocked;
            }

            // Check terrain
            BaseTerrainController terrain = TerrainPoolController.Instance.TerrainPool.Find(
                t => t.Terrain.XPos == terrainPoint.x && t.Terrain.YPos == terrainPoint.y);

            if (terrain == null)
            {
                isBlocked = true;
                return isBlocked;
            }

            UnitImpassableTerrain impassableTerrain = ImpassableTerrains().Find(t => t.BlockedTerrainType == terrain.GetType());

            // If terrain is impassable, mark it as impassable
            if (impassableTerrain != null)
            {
                // Block the terrain
                if(impassableTerrain.StartBlockRange == 0)
                {
                    SetTerrainIndicator(terrainPoint, TerrainIndicator.AttackingArea);
                    isBlocked = true;
                }

                // Add blocked terrain
                BaseTerrain unitCurrentTerrain = unit.TerrainController.Terrain;
                Vector2 unitCurrentPos = new Vector2(unitCurrentTerrain.XPos, unitCurrentTerrain.YPos);

                if(unitCurrentPos.x == terrainPoint.x)
                {
                    for(int i = 1 + impassableTerrain.StartBlockRange; i <= unit.MovementSpace; i++)
                    {
                        if(unitCurrentPos.y < terrainPoint.y)
                        {
                            // Add to up
                            unit.BlockedTerrain.Add(new Vector2(
                                unitCurrentPos.x, unitCurrentPos.y + i));
                        }
                        else if(unitCurrentPos.y > terrainPoint.y)
                        {
                            // Add to bottom
                            unit.BlockedTerrain.Add(new Vector2(
                                unitCurrentPos.x, unitCurrentPos.y - i));
                        }
                    }
                }
                else if(unitCurrentPos.y == terrainPoint.y)
                {
                    for (int i = 1 + impassableTerrain.StartBlockRange; i <= unit.MovementSpace; i++)
                    {
                        if (unitCurrentPos.x < terrainPoint.x)
                        {
                            // Add to right
                            unit.BlockedTerrain.Add(new Vector2(
                                unitCurrentPos.x + i, unitCurrentPos.y));
                        }
                        else if (unitCurrentPos.x > terrainPoint.x)
                        {
                            // Add to left
                            unit.BlockedTerrain.Add(new Vector2(
                                unitCurrentPos.x - i, unitCurrentPos.y));
                        }
                    }
                }
            }

            return isBlocked;
        }

        private void SetUnitColor()
        {
            _spriteRenderer.color = unit.UnitColor;
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

            List<Vector2> attackingTerrainPoints = RhombusPoints.GeneratePointsInsideRhombus(
                new Vector2(currentXPos, currentYPos),
                unit.MovementSpace + unit.WeaponController.WeaponSO.Range
            );
            List<Vector2> movementTerrainPoints = RhombusPoints.GeneratePointsInsideRhombus(
                new Vector2(currentXPos, currentYPos), 
                unit.MovementSpace
            );

            //SetTerrainAsAttackArea(attackingTerrainPoints);
            SetTerrainAsMovementArea(movementTerrainPoints);
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