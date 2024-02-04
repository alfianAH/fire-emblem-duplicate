using UnityEngine;

namespace FireEmblemDuplicate.Scene.Battle.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        public WeaponScriptableObject WeaponSO { get; private set; }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetWeaponSO(WeaponScriptableObject weaponSO)
        {
            WeaponSO = weaponSO;
            _spriteRenderer.sprite = WeaponSO.WeaponSprite;
        }
    }
}