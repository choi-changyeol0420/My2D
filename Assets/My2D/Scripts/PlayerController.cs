using UnityEngine;
using UnityEngine.InputSystem;

namespace My2D
{
    public class PlayerController : MonoBehaviour
    {
        #region Variables
        private Rigidbody2D rb2D;
        private Animator animator;
        private SpriteRenderer sRenderer;

        //플레이어 걷기 속도
        [SerializeField]private float movespeed = 4f;

        //플레이어 이동과 관련된 입력값
        private Vector2 inputMove;

        //점프
        private bool isjump = false;

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
            get { return IsFacingRight; }
            set 
            {
                //반전
                if (IsFacingRight != value)
                {
                    transform.localScale *= new Vector2(-1, 1);
                }
                isFacingRight = value; 
            }
        }
        #endregion

        private void Awake()
        {
            //참조
            rb2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            //rb2D.velocity
        }
        private void FixedUpdate()
        {
            //플레이어 좌우 이동
            rb2D.velocity = new Vector2(inputMove.x * movespeed, rb2D.velocity.y);
            if (Input.GetKey(KeyCode.Space))
            {
                isjump = true;
            }
            Jump();
            //rb2D.velocity = new Vector2(inputMove.x * movespeed, inputMove.y * movespeed);

        }
        //바라보는 방향 전환
        void SetFacingDirection(Vector2 moveInput)
        {
            if (moveInput.x < 0)
            {
                //왼쪽으로 바라본다
                //IsFacingRight = false;
                transform.localScale = new Vector2(-1, 1);
            }
            if (moveInput.x > 0)
            {
                //오른쪽으로 바라본다
                //IsFacingRight = true;
                transform.localScale = new Vector2(1, 1);
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
        void Jump()
        {
            if(!isjump) { return; }
            rb2D.velocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0f, 5f);
            rb2D.AddForce(jumpVelocity, ForceMode2D.Impulse);
            isjump = false;
        }
    }
}