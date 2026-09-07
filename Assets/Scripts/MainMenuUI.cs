using Mono.Cecil.Cil;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class MainMenuUI : MonoBehaviour
{
    private UIDocument document;
    public GameObject mainMenu;
    private Button playButton;
    private VisualElement mainMenuVisual;
    private bool fading;
    private float fadeSpeed = 0.5f;
    private StyleFloat visibilityMainMenu;
    [SerializeField] private GameObject customCursor;
    private void OnEnable()
    {
        document = mainMenu.GetComponentInChildren<UIDocument>();
        playButton = document.rootVisualElement.Q("PlayButton") as UnityEngine.UIElements.Button;
        playButton.RegisterCallback<ClickEvent>(StartGame);
        mainMenuVisual = document.rootVisualElement.Q("Menu");
        fading = false;
        visibilityMainMenu = new StyleFloat(1.0f);
        customCursor.SetActive(true);
    }
    private void StartGame(ClickEvent evt)
    {
        fading = true;
        customCursor.SetActive(false);
    }

    private void Update()
    {
        if (mainMenu)
        {
            if (fading)
            {
                visibilityMainMenu = Mathf.MoveTowards(visibilityMainMenu.value, 0f, fadeSpeed * Time.deltaTime);
            }
            mainMenuVisual.style.opacity = visibilityMainMenu;
            if (visibilityMainMenu.value == 0f)
            {
                mainMenu.SetActive(false);
                MenuManager.Instance.SpawnEntities();
            }
        }
    }
}
