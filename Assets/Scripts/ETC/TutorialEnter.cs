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


    private bool isTutorialEnd = false;
    // Start is called before the first frame update
    void Start()
    {

        if (PlayerPrefs.HasKey(Define.SaveKey.tutorialDone.ToString()) &&
            PlayerPrefs.GetInt(Define.SaveKey.tutorialDone.ToString()) == 1)
        {
            TutorialObject.SetActive(false);
            MoveExplain.SetActive(false);
            ItemExlplain.SetActive(false);
            PlayerUI.SetActive(true);
            Player.SetActive(true);
            EnterCnt = 2;

        }
        else
        {
            TutorialObject.SetActive(true);
            PlayerUI.SetActive(false);
            Player.SetActive(false);
            MoveExplain.SetActive(true);
            ItemExlplain.SetActive(false);

            EnterCnt = 0;
        }
        

    }

    // Update is called once per frame
    void Update()
    {   
        // ���� Ű ������ Ʃ�丮�� ������� ���� ����
        if (!isTutorialEnd && Input.GetKeyDown(KeyCode.Return))
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

                
                PlayerPrefs.SetInt(Define.SaveKey.tutorialDone.ToString(), 1);
                isTutorialEnd = true;
            }


        }
    }
    
}
