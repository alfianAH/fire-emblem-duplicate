using FireEmblemDuplicate.Message;
using SuperMaxim.Messaging;
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
            if(hit2D.collider != null)
            {
                if (hit2D.collider.gameObject.CompareTag("Unit"))
                {
                    Messenger.Default.Publish(new OnClickUnit());
                }
            }
        }

        public void OnDrag(InputAction.CallbackContext context)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);

            RaycastHit2D hit2D = Physics2D.GetRayIntersection(ray);

            switch (context.phase)
            {
                case InputActionPhase.Performed:
                    if (hit2D.collider == null) return;
                    GameObject selectedObject = hit2D.collider.gameObject;

                    if (selectedObject.CompareTag("Unit"))
                    {
                        float positionValue = context.ReadValue<float>();
                        Messenger.Default.Publish(new OnStartDragUnit(selectedObject, positionValue));
                    }
                    break;

                case InputActionPhase.Canceled:
                    if (context.duration < 0.4f) return;

                    // BUG: ADD IF TO LIMIT THE PUBLISH
                    Messenger.Default.Publish(new OnEndDragUnit());
                    break;
            }
        }
    }
}