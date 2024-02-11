using FireEmblemDuplicate.Message;
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
            _inputActionManager.Gameplay.Drag.canceled += _gameplayInput.OnDrag;
        }

        private void OnDisable()
        {
            _inputActionManager.Gameplay.Disable();
        }

        public void OnGameOver(WinMessage message)
        {
            _inputActionManager.Gameplay.Disable();
        }

        public void PauseGame(PauseGameMessage message)
        {
            _inputActionManager.Gameplay.Disable();
        }

        public void ResumeGame(ResumeGameMessage message)
        {
            _inputActionManager.Gameplay.Enable();
        }
    }
}