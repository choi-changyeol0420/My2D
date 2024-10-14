using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        private Animator animator;
        private TorchingDirections torchingDirections;

        //플레이어 걷기 속도
        [SerializeField]private float movespeed = 4f;
        [SerializeField]private float runspeed = 8f;
        [SerializeField]private float airspeed = 2f;

        public float CurrentMoveSpeed
        {
            get 
            {
                if (IsMove && torchingDirections.IsWall)
                {
                    if (torchingDirections.IsGround == false)
                    {
                        if (isRun)
                        {
                            return runspeed;
                        }
                        else
                        {
                            return movespeed;
                        }

                    }
                    else
                    {
                        return airspeed;
                    }
                }
                else
                {
                    return 0;   //idle
                }
            }
        }

        //플레이어 이동과 관련된 입력값
        private Vector2 inputMove;

        //걷기
        [SerializeField] private bool isMove = false;
        public bool IsMove
        { 
            get { return isMove; } 
            set 
            {
                isMove = value;
                animator.SetBool(AnimationString.IsMove, value);
            } 
        }

        //뛰기
        [SerializeField] private bool isRun = false;
        public bool IsRun
        {
            get { return isRun; }
            set 
            { 
                isRun = value;
                animator.SetBool(AnimationString.IsRun, value);
            }
        }

        //좌우 반전
        [SerializeField] private bool isFacingRight = true;
        public bool IsFacingRight
        {
            get { return isFacingRight; }
            set 
            {
                //반전
                if (isFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
                isFacingRight = value; 
            }
        }
        //점프
        [SerializeField]private float jumpForce = 5f;
        #endregion

        private void Awake()
        {
            //참조
            rb2D = this.GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            //rb2D.velocity
        }
        private void FixedUpdate()
        {
            //플레이어 좌우 이동
            rb2D.velocity = new Vector2(inputMove.x * CurrentMoveSpeed, rb2D.velocity.y);

            //애니메이션 값
            animator.SetFloat(AnimationString.Yvelocity, rb2D.velocity.y);

        }
        //바라보는 방향 전환
        void SetFacingDirection(Vector2 moveInput)
        {
            if (moveInput.x < 0 && IsFacingRight == true)
            {
                //왼쪽으로 바라본다
                IsFacingRight = false;
                //transform.localScale = new Vector2(-1, 1);
            }
            if (moveInput.x > 0 && IsFacingRight == false)
            {
                //오른쪽으로 바라본다
                IsFacingRight = true;
                //transform.localScale = new Vector2(1, 1);
            }
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            inputMove = context.ReadValue<Vector2>();
            IsMove = (inputMove != Vector2.zero);
            SetFacingDirection(inputMove);
        }
        public void OnRun(InputAction.CallbackContext context)
        {
            //LeftShift 키를 누르기 시작하는 순간
            if (context.started)
            {
                IsRun = true;
            }
            else if (context.canceled)  //LeftShift 키에서 뗀 순간
            { 
                IsRun = false;
            }
            
        }
        public void OnJump(InputAction.CallbackContext context)
        {
            if (context.started && torchingDirections.IsGround)
            {
                animator.SetTrigger(AnimationString.JumpTrigger);
                rb2D.velocity = new Vector2(rb2D.velocity.x, jumpForce);
            }
        }
        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.started && torchingDirections.IsGround)
            {
                animator.SetTrigger(AnimationString.AttackTrigger);
            }
        }
    }
}