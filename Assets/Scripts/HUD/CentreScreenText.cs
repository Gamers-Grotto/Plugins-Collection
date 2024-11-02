using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CentreScreenText : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private float displayDuration = 2f;
    [SerializeField] private float fadeDuration = 1f;

    private Queue<string> messages = new Queue<string>();
    private Coroutine displayCoroutine;

    private void Awake() => text.canvasRenderer.SetAlpha(0);

    public void AddMessage(string msg)
    {
        messages.Enqueue(msg);

        if (displayCoroutine == null)
            displayCoroutine = StartCoroutine(DisplayMessages());
    }

    private IEnumerator DisplayMessages()
    {
        while (messages.Count > 0)
        {
            text.text = messages.Dequeue();
            text.canvasRenderer.SetAlpha(1);

            yield return new WaitForSeconds(displayDuration);

            if (messages.Count == 0)
            {
                var elapsed = 0f;
                while (elapsed < fadeDuration)
                {
                    elapsed += Time.deltaTime;
                    text.canvasRenderer.SetAlpha(1 - (elapsed / fadeDuration));
                    yield return null;
                }
                text.canvasRenderer.SetAlpha(0);
            }
        }

        displayCoroutine = null;
    }
}