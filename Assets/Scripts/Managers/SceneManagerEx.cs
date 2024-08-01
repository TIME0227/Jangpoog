using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
   
   //오타 방지를 위해 열거형으로 SceneName 관리
   public enum SceneNames
   {

   }

   public string GetActiveScene()
   {
      return SceneManager.GetActiveScene().name;
   }
   
   //LoadScene
   /// <summary>
   /// sceneNames로 넘겨받은 씬을 불러온다. 매개변수가 없는 경우 현재 씬을 다시 로드한다.
   /// </summary>
   /// <param name="sceneName"></param>
   public void LoadScene(string sceneName = "")
   {
      if (string.IsNullOrEmpty(sceneName))
      {
         //Clear 로직 추가 예정
         SceneManager.LoadScene(GetActiveScene());
      }
      else
      {
         //Clear 로직 추가 예정
         SceneManager.LoadScene(sceneName);
      }
   }
   
   //LoadScene by SceneNames
   /// <summary>
   /// SceneNames 열거형으로 매개변수를 받아온 경우
   /// </summary>
   /// <param name="sceneName"></param>
   public void LoadScene(SceneNames sceneName)
   {
      //Clear 로직 추가 예정
      SceneManager.LoadScene(sceneName.ToString());
   }

}
