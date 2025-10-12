using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using VContainer;


public class PlayerAnimationController : MonoBehaviour, ITrolleySystemFacade
{

    [SerializeField] private Animator _PlayerAnimator;  //トロッコのGameObjectをここで取得しなくてはならない
    //[SerializeField] private 

    [Inject] private SEManager _seManager;

    void Update()
    {
         //Debug.Log(_PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime);



    }


    public void MoveTrolley()
    {
        //MoveStartTriggerを発動
        _PlayerAnimator.SetTrigger("MoveStartTrigger");



    }
    public void StopTrolley()
    {
        // StopTrigger を発動
        _PlayerAnimator.SetTrigger("StopTrigger");


    }
    //ジャンプ中はダメ - ジャンプ中　AND アニメーションが終了していない
    //一度だけtrueを返す。その後はFalse
    //Moveからのみ遷移可能
    public void JumpTrolley()
    {
        AcceptJump();

       
    }
    public void DamageTrolley()
    {
        // DamageTrigger を発動
        _PlayerAnimator.SetTrigger("DamageTrigger"); 


    }
    public void FadeoutTrolley()
    {
        //フェードアウトアニメーションを再生
        _PlayerAnimator.SetTrigger("FadeoutTrigger");

        //トロッコオブジェクトをFadeout
        //FadeoutObject();



    }

    private async UniTask AcceptJump()
    {

        // JumpTrigger を発動 - 事前のAnimationがMoveの時のみ発動
        if(_PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Move") || _PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            _PlayerAnimator.SetBool("JumpBool",true);
            _seManager.PlaySE(SEManager.SE.Jump);

            //_PlayerAnimator.SetTrigger("JumpTrigger");

            await UniTask.WaitUntil(() => _PlayerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5);
        
        }
        
            _PlayerAnimator.SetBool("JumpBool",false);
           
     
        }
    }
