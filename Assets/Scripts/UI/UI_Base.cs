using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    public abstract void Init();
    private void Start()
    {
        Init();
    }


    /// <summary>
    /// [UI 자동화] 타입별로 해당 컴포넌트 목록을 딕셔너리 형태로 저장
    /// </summary>
    /// <param name="type"></param>
    /// <typeparam name="T"></typeparam>
    protected void Bind<T>(Type type) where T : UnityEngine.Object 
    {
        //==========!!! 오브젝트 이름과 Enum 목록들의 이름이 동일해야함!!!!==========//
        string[] names = Enum.GetNames(type); //넘겨받은 enum Type의 목록들을 추출
        //예를 들어, enum Texts {HpText, ScoreText} 가 있다고 가정하면, Enum.GetNames(Texts)는 ["HpText", "ScoreText"]를 반환한다.
        
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; //빈 배열, 사이즈만 크기에 맞게 생성
        _objects.Add(typeof(T), objects); //키, 값 딕셔너리에 저장
        
        //즉, 위의 Texts를 예로 들자면, 
        //딕셔너리에 "Text"를 키로 하여, Enum 타입이 Text인 오브젝트들(HpText, ScoreText)의 <Text> Component가 배열 형태로 저장돤다.

        for (int i = 0; i < names.Length; i++) // 빈 배열에 컴포넌트 넣어주기
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    /// <summary>
    /// ui event auto binding
    /// </summary>
    /// <param name="go">이벤트를 연결할 게임 오브젝트</param>
    /// <param name="action">연동할 함수. 콜백 액션</param>
    /// <param name="type">구독할 이벤트 타입</param>
    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);
        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }
    

}