using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private GameObject player;
    private PlayerController pController;
    [SerializeField] private GameObject[] enemies;
    
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        
    }
    public void ResetPositionsAndValues()
    {
        pController = player.GetComponent<PlayerController>();
        pController.currentHealth = pController.maxHealth;
        pController.healthbar.UpdateHealthBar(pController.maxHealth,pController.currentHealth);
        TransformReset(player, pController.startPosition,pController.startRotation);
        for(int i=0;i<enemies.Length; i++)
        {
            WitherSpriteController controller = enemies[i].GetComponent<WitherSpriteController>();
            controller.currentHealth = controller.maxHealth;
            controller.healthbar.UpdateHealthBar(controller.maxHealth, controller.currentHealth);
            controller.gameState = "patrol";
            TransformReset(enemies[i], controller.startPosition, controller.startRotation);
        } 
    }
    private void TransformReset(GameObject obj, Vector3 pos,Quaternion rot)
    {
        obj.transform.position = pos;
        obj.transform.rotation = rot;
    }
}
