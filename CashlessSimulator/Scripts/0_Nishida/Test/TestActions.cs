using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// テスト用：開発用操作
/// </summary>
public class TestActions : MonoBehaviour
{
    void Update()
    {
        //Lキーでシーンをリロード
        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log($"開発用：シーンをリロードしました");
            SceneManager.LoadScene("NishidaScene");
        }
    }
}
