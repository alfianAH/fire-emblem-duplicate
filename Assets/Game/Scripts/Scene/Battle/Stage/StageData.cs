using System;
using System.Collections.Generic;

namespace FireEmblemDuplicate.Scene.Battle.Stage
{
    [Serializable]
    public class StageUnitPosition
    {
        public int XAxis;
        public int YAxis;
    }

    [Serializable]
    public class StageUnitData
    {
        public string UnitSO;
        public string UnitPrefab;
        public string UnitWeapon;
        public StageUnitPosition UnitPosition;
    }

    [Serializable]
    public class StageTerrainData
    {
        public int XAxis;
        public int YAxis;
        public string Type;
    }

    [Serializable]
    public class StageTerrainPoolData
    {
        public int Width;
        public int Height;
        public List<StageTerrainData> Terrains;
    }

    [Serializable]
    public class StageData
    {
        public string Name;
        public StageTerrainPoolData TerrainPool;
        public List<StageUnitData> PlayerUnits;
        public List<StageUnitData> EnemyUnits;
    }
}
