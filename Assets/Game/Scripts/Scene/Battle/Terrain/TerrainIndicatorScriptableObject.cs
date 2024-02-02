using FireEmblemDuplicate.Scene.Battle.Terrain.Enum;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Terrain
{
    [Serializable]
    public class TerrainIndicatorData
    {
        public TerrainIndicator indicator;
        public Sprite indicatorSprite;
        public Color indicatorColor;
    }

    [CreateAssetMenu(fileName = "Terrain Indicator Data", menuName = "SO/Terrain/Indicator")]
    public class TerrainIndicatorScriptableObject : ScriptableObject
    {
        [SerializeField] private List<TerrainIndicatorData> _indicatorData;

        public List<TerrainIndicatorData> IndicatorData => _indicatorData;
    }
}