using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnter : MonoBehaviour
{
    public GameObject TutorialObject;
    public GameObject PlayerUI;
    public GameObject Player;
    public GameObject MoveExplain;
    public GameObject ItemExlplain;

    public int EnterCnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        TutorialObject.SetActive(true);
        PlayerUI.SetActive(false);
        Player.SetActive(false);
        MoveExplain.SetActive(true);
        ItemExlplain.SetActive(false);

        EnterCnt = 0;

    }

    // Update is called once per frame
    void Update()
    {   
        // 엔터 키 누르면 튜토리얼 사라지고 게임 시작
        if (Input.GetKeyDown(KeyCode.Return))
        {

            EnterCnt++;

            if(EnterCnt == 1)
            {
                MoveExplain.SetActive(false);
                ItemExlplain.SetActive(true);
            }
            else if(EnterCnt >= 2)
            {
                TutorialObject.SetActive(false);
                PlayerUI.SetActive(true);
                Player.SetActive(true);
            }


        }
    }
}
