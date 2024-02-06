using System;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Unit
{
    [Serializable]
    public class BaseUnitStats
    {
        [SerializeField, Tooltip("Unit Base HP")] private float _baseHp;
        [SerializeField, Tooltip("Unit Base ATK")] private float _baseAtk;
        [SerializeField, Tooltip("Unit Base DEF")] private float _baseDef;
        [SerializeField, Tooltip("Unit Base RES")] private float _baseRes;
        [SerializeField, Tooltip("Unit Base LUK")] private int _baseLuk;

        public float BaseHP => _baseHp;
        public float BaseATK => _baseAtk;
        public float BaseDEF => _baseDef;
        public float BaseRES => _baseRes;
        public float BaseLUK => _baseLuk;
    }
}