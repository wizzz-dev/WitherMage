using System;
using UnityEditor.ShaderGraph;
using UnityEditor.UI;
using UnityEngine;

using UnityEngine.UIElements;
public class GameOverMenu : MonoBehaviour
{
    private UIDocument document;
    [SerializeField] private GameObject gameOverObject;
    private VisualElement gameOverScreen;
    private VisualElement buttons;
    private Button restart;
    private Button quit;
    private Label deathReason;
    private StyleFloat screenVisibility = new StyleFloat(0f);
    private StyleFloat buttonVisibility = new StyleFloat(0f);
    private StyleFloat invisible = new StyleFloat(0f);
    private float fadeSpeed = 0.5f;

    void OnEnable()
    {
        //UI Toolkit Document
        document = gameOverObject.GetComponentInChildren<UIDocument>();


        //Visual elements to fade in upon active
        gameOverScreen = document.rootVisualElement.Q("Container");
        buttons = document.rootVisualElement.Q("Buttons");
        screenVisibility = invisible;
        buttonVisibility = invisible;
        gameOverScreen.style.opacity = screenVisibility;
        buttons.style.opacity = buttonVisibility;

        //buttons and callbacks
        restart = document.rootVisualElement.Q("RestartButton") as Button;
        quit = document.rootVisualElement.Q("QuitButton") as Button;
        restart.RegisterCallback<ClickEvent>(Restart);
        quit.RegisterCallback<ClickEvent>(Quit);

        //variable label holding reason for player death
        deathReason = document.rootVisualElement.Q("Death") as Label;
        deathReason.text = MenuManager.Instance.deathText;

        
    }

    private void Quit(ClickEvent evt)
    {
        MenuManager.Instance.QuitGame();
    }

    private void Restart(ClickEvent evt)
    {
        MenuManager.Instance.RestartGame();
    }

    void Update()
    {
        if(gameOverScreen.style.opacity.value!=1f)
        {
            screenVisibility = Mathf.MoveTowards(screenVisibility.value, 1f, fadeSpeed * Time.deltaTime);
            gameOverScreen.style.opacity = screenVisibility;
        } else
        {
            buttonVisibility = Mathf.MoveTowards(buttonVisibility.value, 1f, fadeSpeed * Time.deltaTime);
            buttons.style.opacity = buttonVisibility;
        }
    }
}
