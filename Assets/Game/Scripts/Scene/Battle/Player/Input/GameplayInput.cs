using FireEmblemDuplicate.Scene.Battle.Unit;
using UnityEngine;
using UnityEngine.InputSystem;
using static FireEmblemDuplicate.Scene.Battle.InputSystem.InputActionManager;

namespace FireEmblemDuplicate.Scene.Battle.Player.Input
{
    public class GameplayInput : IGameplayActions
    {
        public void OnClick(InputAction.CallbackContext context)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
            if(hit2D.collider != null && hit2D.collider.gameObject.CompareTag("Unit"))
            {
                BaseUnitController unitController = hit2D.collider.gameObject.GetComponent<BaseUnitController>();
                unitController.OnUnitClick();
            }
        }

        public void OnDrag(InputAction.CallbackContext context)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);
            if (hit2D.collider != null && hit2D.collider.gameObject.CompareTag("Unit"))
            {
                BaseUnitController unitController = hit2D.collider.gameObject.GetComponent<BaseUnitController>();
                unitController.OnUnitDrag();
            }
        }
    }
}