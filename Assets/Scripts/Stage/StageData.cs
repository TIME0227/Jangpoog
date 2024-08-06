using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Data", menuName = "Scriptable Object/Stage Data")]
public class StageData : ScriptableObject
{
   [Header("Camera Limit")] 
   [SerializeField] private float cameraLimitMinX;
   [SerializeField] private float cameraLimitMaxX;

   [Header("Player Limit")] 
   [SerializeField] private float playerLimitMinX;

   [SerializeField] private float playerLimitMaxX;

   
   [Header("Start Position")] 
   [SerializeField] private Vector2 playerPosition;

   [SerializeField] private Vector2 cameraPosition;


   public float CameraLimitMinX => cameraLimitMinX;
   public float CameraLimitMaxX => cameraLimitMaxX;
   public float PlayerLimitMinX => playerLimitMinX;
   public float PlayerLimitMaxX => playerLimitMaxX;
   public Vector2 PlayerPosition => playerPosition;
   public Vector2 CameraPosition => cameraPosition;

}
