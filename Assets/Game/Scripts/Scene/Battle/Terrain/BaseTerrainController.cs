using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Terrain.Enum;
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

        public void ChangeTerrainIndicator(ChangeTerrainIndicatorMessage message)
        {
            if (message.XPos != _baseTerrain.XPos ||
                message.YPos != _baseTerrain.YPos)
                return;

            SetTerrainIndicator(message.Indicator);
            SetIndicatorActive(true);
        }

        public void DeactivateTerrainIndicator(DeactivateTerrainIndicatorMessage message)
        {
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
        }
    }
}
