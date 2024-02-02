using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Terrain
{
    [Serializable]
    public class TerrainData
    {
        public string name;
        public Sprite terrainSprite;
        public BaseTerrainController terrainControllerPrefab;
    }

    [CreateAssetMenu(fileName = "Terrain Data", menuName = "SO/Terrain/Data")]
    public class TerrainDataScriptableObject : ScriptableObject
    {
        [SerializeField] private List<TerrainData> _data;

        public List<TerrainData> Data => _data;
    }
}