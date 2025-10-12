using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 食べ物のアクション
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CrabbableObject))]
public class Food : MonoBehaviour, IActionable
{
    public static bool CanTake = true;


    public void UserAction()
    {
        Debug.Log($"選択：食べ物");
        if (CanTake)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
