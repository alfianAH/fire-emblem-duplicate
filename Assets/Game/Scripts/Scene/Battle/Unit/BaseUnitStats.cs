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
        [SerializeField, Tooltip("Unit Base LUK"), Range(0f, 1f)] private float _baseLuk;

        public float BaseHP
        {
            set { _baseHp = value; }
            get { return _baseHp; }
        }
        public float BaseATK
        {
            set { _baseAtk = value; }
            get { return _baseAtk; }
        }
        public float BaseDEF
        {
            set { _baseDef = value; }
            get { return _baseDef; }
        }
        public float BaseRES
        {
            set { _baseRes = value; }
            get { return _baseRes; }
        }
        public float BaseLUK
        {
            set { _baseRes = value; }
            get { return _baseRes; }
        }
    }
}