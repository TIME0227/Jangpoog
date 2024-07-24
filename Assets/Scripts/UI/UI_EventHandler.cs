using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public Action<PointerEventData> OnDragHandler = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null) OnClickHandler.Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (OnDragHandler != null) OnDragHandler.Invoke(eventData);
    }
}

/*delegate 사용 예시
필요한 UI EventHandler 처리가 필요한 캔버스의 UI 오브젝트에 이 스크립트를 컴포넌트로 넣어준다.
UI 자동화를 통해 딕셔너리에 컴포넌트가 자동으로 타입별로 저장되기 때문에, 캔버스를 컨트롤 하는 스크립트에서 필요한 컴포넌트를 가진 오브젝트를 찾고
해당 오브젝트에서 UI_EventHandler 컴포넌트를 찾은 후, 액션을 구독하면 된다.
아래 코드와 같이 처리하면 된다.

//UI_Button.cs
GameObject go = GetImage((int)Images.ItemIcon).gameobject;
UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
evt.OnDragHandler += ((PointEventData data) => {go.transform.position = data.position});

*/
    
    
    
