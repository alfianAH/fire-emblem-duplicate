using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Terrain
{
    public class TerrainPoolController : MonoBehaviour
    {
        [SerializeField] private Vector2 _startPosition;
        private BaseTerrainController _terrainControllerPrefab;
        private Vector2 _terrainSize;

        private void Awake()
        {
            LoadTerrainPrefab();
            CalculateTerrainSize();
            MakeTerrain(8, 9);
        }

        private void LoadTerrainPrefab()
        {
            _terrainControllerPrefab = Resources.Load<BaseTerrainController>("Prefabs/Terrain/Plain Terrain");

            if (_terrainControllerPrefab == null)
                Debug.LogError("Can't find terrain prefab");
        }

        private void CalculateTerrainSize()
        {
            SpriteRenderer terrainSprite = _terrainControllerPrefab.GetComponent<SpriteRenderer>();
            _terrainSize = terrainSprite.transform.localScale;
        }

        private void MakeTerrain(int xLen,  int yLen)
        {
            for(int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    Vector2 terrainPosition = new Vector2(i, j) * _terrainSize + _startPosition;
                    BaseTerrainController duplicateBaseTerrain = Instantiate(_terrainControllerPrefab, transform);
                    duplicateBaseTerrain.SetTerrain(i, j);
                    duplicateBaseTerrain.SetPosition(terrainPosition);
                }
            }
        }
    }
}