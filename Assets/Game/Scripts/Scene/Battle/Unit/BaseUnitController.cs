using FireEmblemDuplicate.Message;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using FireEmblemDuplicate.Scene.Battle.Stage;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    public class BaseUnitController : MonoBehaviour, IBaseUnitAction, IBaseUnitInteraction
    {
        [SerializeField, Range(0f, 1f)] private float _mouseDragSpeed = 0.01f;

        private Camera _mainCamera;
        private Coroutine _dragUnitCoroutine;
        private StageController _stageController;
        private Vector2 _velocity = Vector2.zero;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _stageController = StageController.Instance;
        }

        public void Attack()
        {
            throw new System.NotImplementedException();
        }

        public void Move()
        {
            throw new System.NotImplementedException();
        }

        public void OnUnitClick(OnClickUnit message)
        {
            
        }

        public void OnStartDragUnit(OnStartDragUnit message)
        {
            if (message.SelectedObject != gameObject) return;
            _dragUnitCoroutine = StartCoroutine(DragUpdate(message.SelectedObject, message.PositionValue));
        }

        public void OnEndDragUnit(OnEndDragUnit message)
        {
            if (_dragUnitCoroutine == null) return;
            StopCoroutine(_dragUnitCoroutine);
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