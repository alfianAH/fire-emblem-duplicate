using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Terrain.Enum;
using FireEmblemDuplicate.Scene.Battle.Unit;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Terrain
{
    public abstract class BaseTerrainController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _indicatorSpriteRenderer;
        [SerializeField] private TerrainIndicatorScriptableObject _indicatorData;
        
        private SpriteRenderer _spriteRenderer;
        private BaseTerrain _baseTerrain;
        
        public BaseTerrain Terrain => _baseTerrain;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetUnitOnTerrain(SetUnitOnTerrainMessage message)
        {
            if (message.XPos != _baseTerrain.XPos ||
                message.YPos != _baseTerrain.YPos)
                return;

            _baseTerrain.SetUnitOnTerrain(message.Unit);
        }

        public void ChangeTerrainIndicator(ChangeTerrainIndicatorMessage message)
        {
            if (message.XPos != _baseTerrain.XPos ||
                message.YPos != _baseTerrain.YPos)
                return;

            switch (message.Indicator)
            {
                case TerrainIndicator.AttackingArea:
                    if (!_indicatorSpriteRenderer.gameObject.activeInHierarchy)
                        SetTerrainIndicator(message.Indicator);
                    break;

                case TerrainIndicator.MovementArea:
                case TerrainIndicator.Fight:
                case TerrainIndicator.AllyOnMovementArea:
                    SetTerrainIndicator(message.Indicator);
                    break;

                default:
                    break;
            }

            SetIndicatorActive(true);
        }

        public void DeactivateAllTerrainIndicator(DeactivateAllTerrainIndicatorMessage message)
        {
            SetIndicatorActive(false);
        }

        public void DeactivateTerrainIndicator(DeactivateTerrainIndicatorMessage message)
        {
            if (message.XPos != _baseTerrain.XPos ||
                message.YPos != _baseTerrain.YPos)
                return;

            SetIndicatorActive(false);
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetTerrainSprite(Sprite terrainSprite)
        {
            _spriteRenderer.sprite = terrainSprite;
        }

        public void SetTerrain(int x, int y)
        {
            _baseTerrain = new BaseTerrain(x, y);
        }

        private void SetIndicatorActive(bool isActive)
        {
            _indicatorSpriteRenderer.gameObject.SetActive(isActive);
            // Set terrain to cant be used if terrain indicator is not active
            if(!isActive) 
                _baseTerrain.SetCanBeUsed(isActive);
        }

        private void SetTerrainIndicator(TerrainIndicator newIndicator)
        {
            // Find indicator sprite and color for new indicator
            TerrainIndicatorData indicatorData = _indicatorData.IndicatorData.Find(
                i => i.indicator == newIndicator);
            // Apply to the renderer
            _indicatorSpriteRenderer.sprite = indicatorData.indicatorSprite;
            _indicatorSpriteRenderer.color = indicatorData.indicatorColor;

            // Set terrain to can be used if terrain indicator is blue
            switch (newIndicator)
            {
                case TerrainIndicator.MovementArea:
                    _baseTerrain.SetCanBeUsed(true);
                    break;

                case TerrainIndicator.AttackingArea:
                case TerrainIndicator.Fight:
                case TerrainIndicator.AllyOnMovementArea:
                    _baseTerrain.SetCanBeUsed(false);
                    break;

                default: break;
            }
        }
    }
}
