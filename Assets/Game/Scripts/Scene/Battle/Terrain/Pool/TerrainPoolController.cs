using FireEmblemDuplicate.Core.Singleton;
using FireEmblemDuplicate.Message;
using FireEmblemDuplicate.Scene.Battle.Stage;
using SuperMaxim.Messaging;
using System.Collections.Generic;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Terrain.Pool
{
    public class TerrainPoolController : Singleton<TerrainPoolController>
    {
        [SerializeField] private Vector2 _startPosition;
        private TerrainDataScriptableObject _terrainData;
        private float TERRAIN_SIZE = 0.7f;
        private Vector2 _terrainSize;

        public List<BaseTerrainController> TerrainPool { get; private set; } = new List<BaseTerrainController>();

        private void Awake()
        {
            LoadTerrainData();
            SetTerrainSize();
        }

        public void MakeTerrains(MakeTerrainMessage message)
        {
            for(int i = 0; i < message.TerrainPool.Width; i++)
            {
                for (int j = 0; j < message.TerrainPool.Height; j++)
                {
                    StageTerrainData terrainData = message.TerrainPool.Terrains.Find(t => t.XAxis == i && t.YAxis == j);

                    if (terrainData == null) continue;
                    
                    string terrainType = terrainData.Type;
                    MakeTerrain(i, j, terrainType);
                }
            }

            Messenger.Default.Publish(new MakePlayerUnitMessage(
                StageController.Instance.Stage.Data.PlayerUnits));
            Messenger.Default.Publish(new MakeEnemyUnitMessage(
                StageController.Instance.Stage.Data.EnemyUnits));
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

        private void MakeTerrain(int xPos, int yPos, string terrainType)
        {
            TerrainData terrainData = _terrainData.Data.Find(t => t.name == terrainType);

            if(terrainData == null)
            {
                Debug.LogError($"Can't find terrain with name {terrainType} in terrain data");
                return;
            }

            Vector2 terrainPosition = new Vector2(xPos, yPos) * _terrainSize + _startPosition;
            BaseTerrainController duplicateBaseTerrain = Instantiate(terrainData.terrainControllerPrefab, transform);

            duplicateBaseTerrain.gameObject.name = $"Terrain ({xPos}, {yPos})";
            duplicateBaseTerrain.SetTerrainSprite(terrainData.terrainSprite);
            duplicateBaseTerrain.SetTerrain(xPos, yPos);
            duplicateBaseTerrain.SetPosition(terrainPosition);

            TerrainPool.Add(duplicateBaseTerrain);
        }
    }
}