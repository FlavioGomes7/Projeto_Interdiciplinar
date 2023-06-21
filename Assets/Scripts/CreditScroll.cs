using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CreditScroll : MonoBehaviour
{
    public float animationDuration = 2f;
    public float pauseDuration = 1f;
    public Vector2 startPosition;
    public Vector2 endPosition;
    public List<string> cargos;
    public List<string> nomes;

    private List<GameObject> creditObjects;
    private int currentIndex = 0;

    void Start()
    {
        creditObjects = new List<GameObject>();
        CreateCredits();
        StartCoroutine(PlayCredits());
    }

    void CreateCredits()
    {
        for (int i = 0; i < cargos.Count; i++)
        {
            GameObject creditObject = new GameObject("Credit" + i);
            creditObject.transform.SetParent(transform);

            RectTransform rectTransform = creditObject.AddComponent<RectTransform>();
            rectTransform.localPosition = startPosition;
            rectTransform.sizeDelta = new Vector2(400f, 100f); // Personalize o tamanho do texto conforme necessário

            TextMeshProUGUI cargoText = creditObject.AddComponent<TextMeshProUGUI>();
            cargoText.text = cargos[i];
            cargoText.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Arial SDF"); // Personalize a fonte conforme necessário
            cargoText.fontSize = 32; // Personalize o tamanho da fonte conforme necessário
            cargoText.alignment = TextAlignmentOptions.Center; // Personalize o alinhamento do texto conforme necessário

            TextMeshProUGUI nomeText = creditObject.AddComponent<TextMeshProUGUI>();
            nomeText.text = nomes[i];
            nomeText.font = Resources.Load<TMP_FontAsset>("Fonts & Materials/Arial SDF"); // Personalize a fonte conforme necessário
            nomeText.fontSize = 24; // Personalize o tamanho da fonte conforme necessário
            nomeText.alignment = TextAlignmentOptions.Center; // Personalize o alinhamento do texto conforme necessário

            creditObjects.Add(creditObject);
            creditObject.SetActive(false);
        }
    }

    IEnumerator PlayCredits()
    {
        while (true)
        {
            GameObject currentCredit = creditObjects[currentIndex];
            currentCredit.SetActive(true);

            RectTransform rectTransform = currentCredit.GetComponent<RectTransform>();
            rectTransform.localPosition = startPosition;

            float timer = 0f;
            while (timer < animationDuration)
            {
                timer += Time.deltaTime;

                float t = timer / animationDuration;
                rectTransform.localPosition = Vector2.Lerp(startPosition, endPosition, t);

                yield return null;
            }

            currentCredit.SetActive(false);
            currentIndex = (currentIndex + 1) % creditObjects.Count;

            yield return new WaitForSeconds(pauseDuration);
        }
    }
}
