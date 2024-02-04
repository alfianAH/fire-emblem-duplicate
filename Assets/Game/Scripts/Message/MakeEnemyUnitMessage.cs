using FireEmblemDuplicate.Scene.Battle.Stage;
using System.Collections.Generic;

namespace FireEmblemDuplicate.Message
{
    public struct MakeEnemyUnitMessage
    {
        public List<StageUnitData> UnitDatas { get; private set; }

        public MakeEnemyUnitMessage(List<StageUnitData> unitDatas)
        {
            UnitDatas = unitDatas;
        }
    }
}
