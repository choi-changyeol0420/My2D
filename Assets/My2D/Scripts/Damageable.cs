using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    public class Damageable : MonoBehaviour
    {
        #region Variables
        private Animator animator;

        //체력
        [SerializeField] private float maxhealth = 100f;
        public float Maxhealth
        {
            get { return maxhealth; }
            private set { maxhealth = value; }
        }
        private float currenthealth;
        public float CurrentHealth
        {
            get { return currenthealth; }
            private set
            {
                currenthealth = value;
                //죽음 처리
                if (currenthealth <= 0)
                {
                    IsDeath = true;
                }
            }
        }
        private bool isDeath = false;
        public bool IsDeath
        {
            get { return isDeath; }
            private set
            {
                isDeath = value;
                //애니메이션
                animator.SetBool(AnimationString.IsDeath,value);
            }
        }

        //무적모드
        private bool isInvincible = false;
        [SerializeField] private float isinvincibleTimer = 3f;
        private float countdown = 0f;
        #endregion
        private void Awake()
        {
            //참조
            animator = GetComponent<Animator>();
        }
        private void Start()
        {
            //초기화
            CurrentHealth = Maxhealth;
            countdown = isinvincibleTimer;
        }
        private void Update()
        {
            //무적상태이면 무적 타이머를 돌린다
            if(isInvincible)
            {
                if( countdown <= 0 )
                {
                    isInvincible = false;
                    //타이머 초기화
                    countdown = isinvincibleTimer;
                }
                countdown -= Time.deltaTime;
            }
        }

        public void TakeDamage(float damage)
        {
            if (!IsDeath && !isInvincible)
            {
                //무적모드 초기화
                //isInvincible = true;
                

                CurrentHealth -= damage;
                Debug.Log($"{transform.name}의 현재 체력은 {CurrentHealth}");

                //애니메이션
                animator.SetTrigger(AnimationString.HitTrigger);
            }
        }
    }
}