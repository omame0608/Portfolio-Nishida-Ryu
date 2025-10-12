using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private GameObject quizSystemFacade;

    private IQuiz TempQuizSet;

    // Start is called before the first frame update
    void Start()
    {
        //TempQuizSet = quizSystemFacade.GetComponent<QuizSystemFacade>().GetQuizWithID(5678);
        //Debug.Log(TempQuizSet.JudgeAnswer(6));


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
