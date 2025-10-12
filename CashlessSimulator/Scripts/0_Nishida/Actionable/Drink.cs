using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ドリンクのアクション
/// </summary>
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CrabbableObject))]
public class Drink : MonoBehaviour, IActionable
{
    public static bool CanTake = true;


    public void UserAction()
    {
        Debug.Log($"選択：ドリンク");
        if (CanTake)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
