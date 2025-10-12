using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class DoTest : MonoBehaviour
    {
        // Start is called before the first frame update
        async void Start()
        {
            var i = GetComponent<Image>();
            await UniTask.Delay(1000);
            Debug.Log($"bar!!!!");
            //await DOTween.To(() => i.fillAmount, x => i.fillAmount = x, 1f, 1f).SetEase(Ease.Linear);
            Debug.Log($"gagragq");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


