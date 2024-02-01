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

        [Header("Stats")]
        [SerializeField, Tooltip("Unit Base HP")] private float _baseHp;
        [SerializeField, Tooltip("Unit Base ATK")] private float _baseAtk;
        [SerializeField, Tooltip("Unit Base DEF")] private float _baseDef;
        [SerializeField, Tooltip("Unit Base RES")] private float _baseRes;
        [SerializeField, Tooltip("Unit Base LUK")] private int _baseLuk;

        private string Name => _name;
        private UnitAffinity Affinity => _affinity;
        private float BaseHP => _baseHp;
        private float BaseATK => _baseAtk;
        private float BaseDEF => _baseDef;
        private float BaseRES => _baseRes;
        private float BaseLUK => _baseLuk;
    }
}
