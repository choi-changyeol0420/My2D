using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Projectile : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2d;

        //화살 이동
        [SerializeField]private Vector2 moveSpeed = new Vector2(5f,0f);

        //데미지
        [SerializeField]private float damamge = 10f;
        [SerializeField] private Vector2 knockback = new Vector2(0f, 0f);

        //데미지 이펙트
        public GameObject impactPrefab;
        #endregion
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }
        private void Start()
        {
            rb2d.velocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null )
            {
                //knocback의 방향 설정
                Vector2 deliveredknockback = (transform.localScale.x > 0) ? knockback : new Vector2(-knockback.x, knockback.y);
                damageable.TakeDamage(damamge, deliveredknockback);

                //데미지 이펙트
                GameObject impactGo = Instantiate(impactPrefab, collision.transform.position, Quaternion.identity);
                Destroy(impactGo, 0.5f);

                //화살 킬
                Destroy(gameObject);
            }
        }
    }
}