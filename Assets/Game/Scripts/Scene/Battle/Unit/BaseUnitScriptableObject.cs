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
        [SerializeField, Tooltip("Unit Base HP")] private float _baseHp;
        [SerializeField, Tooltip("Unit Base ATK")] private float _baseAtk;
        [SerializeField, Tooltip("Unit Base DEF")] private float _baseDef;
        [SerializeField, Tooltip("Unit Base RES")] private float _baseRes;
        [SerializeField, Tooltip("Unit Base LUK")] private int _baseLuk;

        public string Name => _name;
        public UnitSide Side => _side;
        public UnitAffinity Affinity => _affinity;
        public float BaseHP => _baseHp;
        public float BaseATK => _baseAtk;
        public float BaseDEF => _baseDef;
        public float BaseRES => _baseRes;
        public float BaseLUK => _baseLuk;
    }
}
