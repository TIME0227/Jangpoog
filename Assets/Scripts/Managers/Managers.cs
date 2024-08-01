using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    //Singleton
    static Managers s_instance; //유일성 보장

    static Managers Instance { get
    {
        Init();
        return s_instance;
    } } //유일한 매니져를 가져온다.

    
    #region Core

    private DataManager _data = new DataManager();
    private InputManager _input = new InputManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private UIManager _ui = new UIManager();
    
    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return s_instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene
    {
        get { return Instance._scene; }
    }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion
    
    private void Start()
    {
        Init();
    }

    private void Update()
    {
        _input.OnUpdate(); //마우스, 키보드 입력 체크를 Managers가 대표로 처리
    }

    //초기화
    static void Init()
    {
        if (s_instance == null)
        {
            //현재 @Managers가 씬에 있는지 확인
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                //씬에 없는 경우 @Managers를 새로 만듦
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();

            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();


            s_instance._data.Init();
        }

    }

    public static void Clear()
    {
        Input.Clear();
        //추가 예정
   }



    public void SceneTest()
    {
        Managers.Scene.LoadScene("SampleScene");
    }
}
