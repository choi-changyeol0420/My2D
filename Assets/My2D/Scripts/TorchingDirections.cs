using UnityEngine;

namespace My2D
{
    public class TorchingDirections : MonoBehaviour
    {
        #region Variables
        private CapsuleCollider2D torchingCollier;
        private Animator animator;

        [SerializeField]private ContactFilter2D contactFilter;
        [SerializeField] private float groundDistance = 0.05f;
        [SerializeField] private float ceilingDistance = 0.05f;
        [SerializeField] private float wallDistance = 0.2f;

        private RaycastHit2D[] groundhits = new RaycastHit2D[5];
        private RaycastHit2D[] ceilinghits = new RaycastHit2D[5];
        private RaycastHit2D[] wallhits = new RaycastHit2D[5];

        [SerializeField]private bool isGround;
        public bool IsGround
        {
            get { return isGround; }
            private set 
            {
                isGround = value;
                animator.SetBool(AnimationString.IsGround, value);
            }
        }
        private bool isWall;
        public bool IsWall
        {
            get { return isWall ; }
            private set
            {
                isWall = value;
                animator.SetBool(AnimationString.IsWall, value);
            }
        }
        private bool isCeiling;
        public bool IsCeiling
        {
            get { return isCeiling; }
            private set
            {
                isCeiling = value;
                animator.SetBool(AnimationString.IsCeiling, value);
            }
        }
        private Vector2 WalkDirection => (transform.localScale.x > 0) ? Vector2.right : Vector2.left;
        #endregion
        private void Awake()
        {
            //참조
            torchingCollier = GetComponent<CapsuleCollider2D>();
            animator = GetComponent<Animator>();
        }
        private void FixedUpdate()
        {
            IsGround = (torchingCollier.Cast(Vector2.down, contactFilter, groundhits, groundDistance) > 0);
            IsCeiling = (torchingCollier.Cast(Vector2.up, contactFilter, ceilinghits, ceilingDistance) > 0);
            IsWall = (torchingCollier.Cast(WalkDirection, contactFilter, wallhits, wallDistance) > 0);
        }
    }
}