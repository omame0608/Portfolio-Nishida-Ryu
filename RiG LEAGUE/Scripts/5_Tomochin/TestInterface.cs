using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInterface : MonoBehaviour
{
    
    //[SerializeField] private Animator _PlayerAnimator;  //トロッコのAnimatorControllerを取得
    [SerializeField] private PlayerAnimationController _PlayerAnimationController;

    void Start()
    {

        // animatorコンポーネントを取得
        //_PlayerAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {

            //デバッグ
            Debug.Log("Spaceキーを押した");

            //ジャンプのTriggerを発動
           _PlayerAnimationController.JumpTrolley();

        }

        if(Input.GetKeyDown(KeyCode.D))
        {
          

            //デバッグ
            Debug.Log("Dキーを押した");

            //ダメージのTriggerを発動
           _PlayerAnimationController.DamageTrolley();


        }

        if(Input.GetKeyDown(KeyCode.S))
        {
          

            //デバッグ
            Debug.Log("Sキーを押した");

            //ダメージのTriggerを発動
           _PlayerAnimationController.StopTrolley();


        }

        if(Input.GetKeyDown(KeyCode.M))
        {

            Debug.Log("Mキーを押した");

            //移動開始
            _PlayerAnimationController.MoveTrolley();


        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Fキーを押した");

            _PlayerAnimationController.FadeoutTrolley();

        }
        
    }
}
