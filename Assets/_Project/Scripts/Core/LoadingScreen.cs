using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
using System;
using Random = UnityEngine.Random;

public class LoadingScreen : MonoBehaviour
{
    [Header("UI References")]
    public Slider loadingBar;
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI tipText;

    [Header("Tips")]
    public string[] survivaltips =
    {
        "Stay moving — zombies are slow but relentless.",
        "Rescue friends to unlock new abilities.",
        "Watch your ammo — melee is silent and saves bullets.",
        "Barricades can buy you precious seconds.",
        "Listen for groans — zombies nearby make noise."
    };

    void Start()
    {
        // Show a random tip
        if (tipText)
            tipText.text = survivaltips[Random.Range(0, survivaltips.Length)];

        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        yield return new WaitForSeconds(0.1f); // small delay before starting

        AsyncOperation operation = SceneManager.LoadSceneAsync(LoadingScreenData.sceneToLoad);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            // Progress goes 0 to 0.9 before allowSceneActivation
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (loadingBar) loadingBar.value = progress;
            if (loadingText) loadingText.text = $"Loading... {(int)(progress * 100)}%";

            // When fully loaded, activate the scene
            if (operation.progress >= 0.9f)
            {
                if (loadingText) loadingText.text = "Press any key to continue";
                if (Input.anyKeyDown) operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}