using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScrollSystem : MonoBehaviour ,IFieldScrollSystemFacade
{
    //[Header("スクロール速度設定")]
    public float targetSpeed;
    public float acceleration;

    //[Header("Fieldの設定")]
    public GameObject fieldPrefab;
    /*
    public GameObject rail1Prefab;
    public GameObject rail2Prefab;
    public GameObject rail3Prefab;
    public GameObject rail4Prefab;
    public GameObject rail5Prefab;
    public GameObject rail6Prefab;
    public GameObject rail7Prefab;
    public GameObject rail8Prefab;*/
    public float spawnInterval; //targetspeed10のとき、spawnIntervalは0.4くらいがいいかも
    public float spawnZ;
    
    public static float currentSpeed;
    private bool isScrolling = false;

    private Coroutine scrollCoroutine;
    private Coroutine spawnCoroutine;
    public void StartFieldScroll(bool useAcceleration)
    {
        if (scrollCoroutine != null) StopCoroutine(scrollCoroutine);
        scrollCoroutine = StartCoroutine(ScrollCoroutine(targetSpeed, useAcceleration));
        isScrolling = true;

        // 生成も開始
        if (spawnCoroutine == null)
            spawnCoroutine = StartCoroutine(SpawnFields());
        
    }

    public void StopFieldScroll(bool useAcceleration)
    {
        if (scrollCoroutine != null) StopCoroutine(scrollCoroutine);
        scrollCoroutine = StartCoroutine(ScrollCoroutine(0f, useAcceleration));
        isScrolling = false;

        // 生成を止める
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private IEnumerator ScrollCoroutine(float target, bool useAcceleration)
    {
        if (!useAcceleration)
        {
            currentSpeed = target;
            yield break;
        }

        while (Mathf.Abs(currentSpeed - target) > 0.01f)
        {
            currentSpeed = Mathf.MoveTowards(currentSpeed, target, acceleration * Time.deltaTime);
            yield return null;
        }

        currentSpeed = target;
    }

    private IEnumerator SpawnFields()
    {
        while (true)
        {
            var field = Instantiate(fieldPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
            /*
            var rail1 = Instantiate(rail1Prefab, new Vector3(rail1Prefab.transform.position.x, rail1Prefab.transform.position.y, spawnZ), Quaternion.identity);
            var rail2 = Instantiate(rail2Prefab, new Vector3(rail2Prefab.transform.position.x, rail2Prefab.transform.position.y, spawnZ), Quaternion.identity);
            var rail3 = Instantiate(rail3Prefab, new Vector3(rail3Prefab.transform.position.x, rail3Prefab.transform.position.y, spawnZ), Quaternion.identity);
            var rail4 = Instantiate(rail4Prefab, new Vector3(rail4Prefab.transform.position.x, rail4Prefab.transform.position.y, spawnZ), Quaternion.identity);
            var rail5 = Instantiate(rail5Prefab, new Vector3(rail5Prefab.transform.position.x, rail5Prefab.transform.position.y, spawnZ), Quaternion.identity);
            var rail6 = Instantiate(rail6Prefab, new Vector3(rail6Prefab.transform.position.x, rail6Prefab.transform.position.y, spawnZ), Quaternion.identity);
            var rail7 = Instantiate(rail7Prefab, new Vector3(rail7Prefab.transform.position.x, rail7Prefab.transform.position.y, spawnZ), Quaternion.identity);
            var rail8 = Instantiate(rail8Prefab, new Vector3(rail8Prefab.transform.position.x, rail8Prefab.transform.position.y, spawnZ), Quaternion.identity);
            */
            yield return new WaitForSeconds(spawnInterval);
        }
    }


    void Start()
    {
        //StartCoroutine(SpawnFields());
    }

    // Update is called once per frame
    void Update()
    {


    }
}
