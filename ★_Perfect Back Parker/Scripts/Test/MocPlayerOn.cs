using ParkingGame.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// テスト用：プレイヤーとカメラの操作を可能にする
/// </summary>
public class MocPlayerOn : MonoBehaviour
{
    [SerializeField] private CarController _carController;
    [SerializeField] private CameraController _cameraController;


    // Start is called before the first frame update
    void Start()
    {
        _carController.CanControll = true;
        _cameraController.CanControll = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
