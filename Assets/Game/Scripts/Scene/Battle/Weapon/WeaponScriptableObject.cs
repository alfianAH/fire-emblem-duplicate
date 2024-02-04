using FireEmblemDuplicate.Scene.Battle.Weapon.Enum;
using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Weapon
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "SO/Weapon")]
    public class WeaponScriptableObject : ScriptableObject
    {
        [SerializeField, Range(1, 5)] private int _range;
        [SerializeField] private Sprite _weaponSprite;
        [SerializeField] private WeaponDamageRange _damageRange;
        [SerializeField] private WeaponType _type;

        public int Range => _range;
        public Sprite WeaponSprite => _weaponSprite;
        public WeaponDamageRange DamageRange => _damageRange;
        public WeaponType Type => _type;
    }
}