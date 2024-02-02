using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Terrain
{
    public class TerrainPoolController : MonoBehaviour
    {
        [SerializeField] private Vector2 _startPosition;
        private TerrainDataScriptableObject _terrainData;
        private float TERRAIN_SIZE = 0.7f;
        private Vector2 _terrainSize;

        private void Awake()
        {
            LoadTerrainData();
            SetTerrainSize();
            MakeTerrains(8, 9);
        }

        private void LoadTerrainData()
        {
            _terrainData = Resources.Load<TerrainDataScriptableObject>("SO/Terrain/Terrain Data");

            if (_terrainData == null)
                Debug.LogError("Can't find terrain data");
        }

        private void SetTerrainSize()
        {
            _terrainSize = new Vector2(TERRAIN_SIZE, TERRAIN_SIZE);
        }

        private void MakeTerrains(int xLen,  int yLen)
        {
            for(int i = 0; i < xLen; i++)
            {
                for (int j = 0; j < yLen; j++)
                {
                    MakeTerrain(i, j);
                }
            }
        }

        private void MakeTerrain(int xPos, int yPos)
        {
            Vector2 terrainPosition = new Vector2(xPos, yPos) * _terrainSize + _startPosition;
            BaseTerrainController duplicateBaseTerrain = Instantiate(_terrainData.Data[0].terrainControllerPrefab, transform);

            duplicateBaseTerrain.SetTerrainSprite(_terrainData.Data[0].terrainSprite);
            duplicateBaseTerrain.SetTerrain(xPos, yPos);
            duplicateBaseTerrain.SetPosition(terrainPosition);
        }
    }
}