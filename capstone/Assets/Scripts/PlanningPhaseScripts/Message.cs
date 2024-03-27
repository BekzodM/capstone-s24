using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{

    [SerializeField] private float fadeDuration = 0.6f;
    private CanvasGroup canvasGroup;

    TextMeshProUGUI messageToPlayer;
    // Start is called before the first frame update

    private void Awake() {
        messageToPlayer= GetComponentInChildren<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    void Start()
    {
        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }

    public void FadeInPanel() {
        gameObject.SetActive(true);
        StartCoroutine(FadeInAndOutPanel(0f, 1f));
    }

    public void FadeOutPanel()
    {
        StartCoroutine(FadeInAndOutPanel(1f, 0f));
    }

    private IEnumerator FadeInAndOutPanel(float startAlpha, float targetAlpha)
    {
        // Fade in
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            canvasGroup.alpha = alpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        // Delay
        yield return new WaitForSeconds(3f);

        // Fade out
        elapsedTime = 0f;
        startAlpha = targetAlpha;
        targetAlpha = 1 - targetAlpha;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            canvasGroup.alpha = alpha;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        gameObject.SetActive(false);
    }

    public void SetMessageText(string message) {
        messageToPlayer.text = message;
        FadeInPanel();
    }
}
