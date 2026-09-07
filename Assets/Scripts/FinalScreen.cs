using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FinalScreen : MonoBehaviour
{
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject buttonAndLabel;
    [SerializeField] private GameObject finalScreen;
    [SerializeField] private float scrollSpeed = 1f;
    [SerializeField] private float scrollSpeedBoost = 3.5f;
    [SerializeField] private InputAction scrollFaster;
    private float scrolledPast = 48f;//change according to scroll heioght after credits added
    [SerializeField] private float positionScrollable;
    private RectTransform scrollTransform;
    [SerializeField] private RectTransform finalScreenTransform;
    [SerializeField] private GameObject images;

    void OnEnable()
    {
        images.SetActive(true);
        scrollFaster.Enable();
        if (!credits.activeSelf)
        {
            credits.SetActive(true);
            buttonAndLabel.SetActive(true);
            Debug.Log("Bang!");
        }
        scrollTransform = credits.GetComponent<RectTransform>();
        finalScreenTransform = finalScreen.GetComponent<RectTransform>();
        finalScreenTransform.localScale = new Vector3(0.01f,0.01f,0.01f);
    }

    public void GitHubLink()
    {
        Application.OpenURL("https://github.com/wizz-js-dev");
    }

    public void YoutubeLink()
    {
        Application.OpenURL("https://www.youtube.com/@wizzgreen");
    }

    void OnDisable()
    {
        scrollFaster.Disable();
    }
    
    public void LastScreen()
    {
        if (credits)
        {
            credits.SetActive(false);
            buttonAndLabel.SetActive(false);
            finalScreen.SetActive(true);
            images.SetActive(false);
        }
    }

    void Update()
    {
        positionScrollable = scrollTransform.position.y;
        if (credits)
        {
            credits.transform.Translate(Vector3.up * scrollSpeed * scrollSpeedBoost * Time.deltaTime);
            if (scrollFaster.IsPressed())
            {
                scrollSpeedBoost = 3.5f;
            }
            else
            {
                scrollSpeedBoost = 1f;
            }
            if (positionScrollable > scrolledPast)
            {
                LastScreen();
            }
        }
        if(finalScreen.activeSelf)
        {
            finalScreenTransform.localScale = Vector3.MoveTowards(finalScreenTransform.localScale, Vector3.one, 1.5f * Time.deltaTime);
        }
    }
    
    
   

}
