using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム終了画面を表示するクラス
/// </summary>
public class EndTrigger : MonoBehaviour
{
    //参照
    [SerializeField] private FadeObject _canvas; //ゲーム終了画面のキャンバス
    [SerializeField] private GameObject _selectionVisualizer; //レイ
    [SerializeField] private GameObject _room; //コンビニ全体


    private async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //ゲーム終了画面の設定
            _canvas.transform.GetChild(0).GetComponent<Text>().text = "<color=red>シミュレーション終了</color>";
            _canvas.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = "タイトル画面へ戻る";
            _canvas.transform.GetChild(1).GetComponent<ButtonClick>().IsEnd = true;

            //フェードアウトしてゲーム終了画面を表示する
            _canvas.SwitchFade(fade: true);
            _selectionVisualizer.SetActive(true);
            await Task.Delay(TimeSpan.FromSeconds(0.5f));
            _room.SetActive(false);
        }
    }
}
