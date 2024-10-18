using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace My2D
{
    //게임 스프라이트 오브젝트를 페이드아웃 후 킬
    public class FadeReMoveBehaviour : StateMachineBehaviour
    {
        #region Variables
        private SpriteRenderer spriteRenderer;
        private GameObject removeObject;
        private Color startcolor;

        //fade 효과
        public float fadeTimer = 1f;
        private float countdown = 0f;

        public float delayTimer = 2f;
        private float delayCountdown = 0f;
        #endregion

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            spriteRenderer = animator.GetComponent<SpriteRenderer>();
            startcolor = spriteRenderer.color;
            removeObject = animator.gameObject;

            //초기화
            countdown = fadeTimer;
        }

        // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            //delatTime 만큼 딜레이
            if(delayCountdown < delayTimer)
            {
                delayCountdown += Time.deltaTime;
                return;
            }
            //페이드 효과
            countdown -= Time.deltaTime;
            float newAlpha = startcolor.a * (countdown / fadeTimer);
            spriteRenderer.color = new Color (startcolor.r, startcolor.g, startcolor.b, newAlpha);
            if (countdown <= 0f)
            {
                Destroy (removeObject);
            }
            
        }
    }
}