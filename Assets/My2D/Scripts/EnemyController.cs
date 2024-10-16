using UnityEngine;

namespace My2D
{
    public class EnemyController : MonoBehaviour
    {
        #region Variables
        private Animator animator;
        private Rigidbody2D rb2d;
        private TouchingDirections touchingDirections;

        public DetectionZone detectionZone;

        //이동
        [SerializeField]private float runspeed = 4f;
        //이동방향
        private Vector2 directionVector = Vector2.right;
        //이동 가능 방향
        public enum WalkableDirection {Left, Right }
        //현재 이동 방향
        private WalkableDirection walkdirection = WalkableDirection.Right;
        public WalkableDirection walkDirection
        {
            get { return walkdirection; }
            private set
            {
                transform.localScale *= new Vector2(-1, 1);
                if (value == WalkableDirection.Left)
                {
                    directionVector = Vector2.left;
                }
                else if (value == WalkableDirection.Right)
                { 
                    directionVector = Vector2.right;
                }
                walkdirection = value;
            }
        }
        //공격 타겟 설정
        [SerializeField]private bool hasTarget = false;
        public bool HasTarget
        {
            get { return hasTarget; }
            private set { 
                hasTarget = value;
                animator.SetBool(AnimationString.HasTarget,value);
            }
        }

        //이동 가능 상태/불가능 상태 - 이동 제한
        public bool CanMove
        {
            get { return animator.GetBool(AnimationString.CanMove); }
        }

        //감속 계수
        [SerializeField]private float stopRate = 0.2f;

        #endregion
        private void Awake()
        {
            animator = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            touchingDirections = GetComponent<TouchingDirections>();
        }
        private void Update()
        {
            //적 감지 충돌체의 리스트 갯수가 0보다 크면 적이 감지 된것이다
            //HasTarget = (detectionZone.detectedColliders.Count > 0);
        }
        private void FixedUpdate()
        {
            //땅에서 이동시 벽을 만나면 방향 전환
            if(touchingDirections.IsWall && touchingDirections.IsGround)
            {
                //방향전환 반전
                Flip();
            }
            //이동
            if (CanMove)
            {
                rb2d.velocity = new Vector2(directionVector.x * runspeed, rb2d.velocity.y);
            }
            else
            {
                //rb2d.velocity.x -> 0 : Lerp 멈춤
                rb2d.velocity = new Vector2(Mathf.Lerp(rb2d.velocity.x,0,stopRate), rb2d.velocity.y);
            }
        }
        //방향전환 반전
        private void Flip()
        {
            if(walkDirection == WalkableDirection.Left)
            {
                walkDirection = WalkableDirection.Right;
            }
            else if (walkDirection == WalkableDirection.Right)
            {
                walkDirection = WalkableDirection.Left;
            }
            else
            {
                Debug.Log("Error Flip Direction");
            }
        }
    }
}