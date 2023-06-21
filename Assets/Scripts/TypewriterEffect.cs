using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TypewriterEffect : MonoBehaviour
{
    public float letterDelay = 0.1f;
    public List<string> texts;

    private TextMeshProUGUI textComponent;
    private Coroutine displayCoroutine;
    private int currentTextIndex = 0;

    public delegate void TextsFinishedHandler();
    public event TextsFinishedHandler OnTextsFinished;

    void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        displayCoroutine = StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText()
    {
        while (currentTextIndex < texts.Count)
        {
            string currentText = texts[currentTextIndex];
            textComponent.text = "";

            for (int i = 0; i < currentText.Length; i++)
            {
                textComponent.text += currentText[i];
                yield return new WaitForSeconds(letterDelay);
            }

            currentTextIndex++;
            yield return new WaitForSeconds(1f); // Aguarda um segundo entre os textos
        }

        // Dispara o evento OnTextsFinished quando todos os textos foram exibidos
        OnTextsFinished?.Invoke();
    }
}
