using FireEmblemDuplicate.Scene.Battle.Unit.Enum;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    [CreateAssetMenu(fileName = "Base Unit", menuName = "SO/Unit")]
    public class BaseUnitScriptableObject: ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;

        [Header("Type")]
        [SerializeField] private UnitAffinity _affinity;
        [SerializeField] private UnitSide _side;

        [Header("Stats")]
        [SerializeField, Tooltip("Unit Base HP")] private BaseUnitStats _baseUnitStats;

        public string Name => _name;
        public UnitSide Side => _side;
        public UnitAffinity Affinity => _affinity;
        public BaseUnitStats UnitStats => _baseUnitStats;
    }
}
