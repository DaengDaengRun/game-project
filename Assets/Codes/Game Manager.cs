using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;
   public Player player;

   void Awake()
   {
        instance = this;
   }
}
