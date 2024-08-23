using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnter : MonoBehaviour
{
    public GameObject TutorialObject;
    public GameObject PlayerUI;

    // Start is called before the first frame update
    void Start()
    {
        TutorialObject.SetActive(true);
        PlayerUI.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {   
        // 엔터 키 누르면 튜토리얼 사라지고 게임 시작
        if (Input.GetKeyDown(KeyCode.Return))
        {
            TutorialObject.SetActive(false);
            PlayerUI.SetActive(true);
        }
    }
}
