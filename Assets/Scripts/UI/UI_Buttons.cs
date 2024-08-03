using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Buttons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnClickNewGame()
    {
        Debug.Log("새 게임");
        SceneManager.LoadScene("Difficulty");

    }

    public void OnClickLoad()
    {
        Debug.Log("이어하기");
        
    }

    public void OnClickSettings()
    {
        Debug.Log("설정");
        SceneManager.LoadScene("Settings");
    }

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif        
        Application.Quit();
    }
    
    public void OnClickNormal()
    {
        Debug.Log("보통 난이도 게임");
    }

    public void OnClickHard()
    {
        Debug.Log("어려움 난이도 게임");
    }

    public void OnClickImpossible()
    {
        Debug.Log("불가능 레벨");
    }
    public void OnClickPlayerSettings()
    {
        Debug.Log("설정으로 이동");
    }

    public void OnClickChapters()
    {
        Debug.Log("챕터맵으로 이동");
        SceneManager.LoadScene("Chapters");
    }

    public void OnClickSaveAndExitToMain()
    {
        Debug.Log("세이브 후 메인 화면으로 이동");
        //여기에 파일 세이브 코드 넣기

        SceneManager.LoadScene("Main");

    }

    public void OnClickExit2Windows()
    {
        Debug.Log("세이브 후 윈도우 화면으로 이동 및 게임 종료");
        
        // 여기에다 게임 세이브 코드 작성
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif        
        Application.Quit();
    }
}
