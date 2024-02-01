using FireEmblemDuplicate.Scene.Battle.InputSystem;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Player.Input
{
    public class PlayerInputController : MonoBehaviour
    {
        private InputActionManager _inputActionManager;
        private GameplayInput _gameplayInput;

        private void Start()
        {
            _inputActionManager = new InputActionManager();
            _gameplayInput = new GameplayInput();
            
            _inputActionManager.Gameplay.Enable();
            _inputActionManager.Gameplay.Click.performed += _gameplayInput.OnClick;
            _inputActionManager.Gameplay.Drag.performed += _gameplayInput.OnDrag;
        }
    }
}