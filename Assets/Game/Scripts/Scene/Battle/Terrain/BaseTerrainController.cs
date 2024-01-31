using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Terrain
{
    public class BaseTerrainController : MonoBehaviour
    {
        [SerializeField] private Sprite _terrainSprite;
        private SpriteRenderer _spriteRenderer;
        private BaseTerrain _baseTerrain;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            SetSprite();
        }

        public void SetPosition(Vector2 position)
        {
            transform.position = position;
        }

        public void SetTerrain(int x, int y)
        {
            _baseTerrain = new BaseTerrain(x, y);
        }

        private void SetSprite()
        {
            _spriteRenderer.sprite = _terrainSprite;
        }
    }
}
