using UnityEngine;
using UnityEngine.Events;

namespace My2D
{
    public class Damageable : MonoBehaviour
    {
        #region Variables
        private Animator animator;
        

        //데미지 입을때 등록된 함수 호출
        public UnityAction<float, Vector2> hitAction;

        //체력
        [SerializeField] private float maxhealth = 100f;
        public float Maxhealth
        {
            get { return maxhealth; }
            private set { maxhealth = value; }
        }
        [SerializeField]private float currenthealth;
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

        //
        public bool LockVelocity
        {
            get
            {
                return animator.GetBool(AnimationString.LockVelocity);
            }
            private set
            {
                animator.SetBool (AnimationString.LockVelocity,value);
            }
        }
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

        public void TakeDamage(float damage, Vector2 knockback)
        {
            if (!IsDeath && !isInvincible)
            {
                //무적모드 초기화
                isInvincible = true;

                float beforeHealth = CurrentHealth;

                CurrentHealth -= damage;
                Debug.Log($"{transform.name}의 현재 체력은 {CurrentHealth}");
                LockVelocity = true;
                animator.SetTrigger(AnimationString.HitTrigger);    //애니메이션

                //데미지 효과
                /*if(hitAction != null )
                {
                    hitAction.Invoke(damage, knocback);
                }*/

                //실제 힐 hp값
                float realDamage = beforeHealth - CurrentHealth;
                hitAction?.Invoke(damage,knockback);
                CharacterEvents.characterDamage?.Invoke(gameObject, realDamage);
            }
            
        }
        public bool Heal(float amount)
        {
            if(CurrentHealth >= Maxhealth)
            {
                return false;
            }
            //힐 전의 hp
            float beforeHealth = CurrentHealth;
            CurrentHealth += amount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0, Maxhealth);
            //실제 힐 hp값
            float realHealth = CurrentHealth - beforeHealth;

            CharacterEvents.characterHealed?.Invoke(gameObject,realHealth);
            return true;
        }
    }
}