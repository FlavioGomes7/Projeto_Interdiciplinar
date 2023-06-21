using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditScroll : MonoBehaviour
{
    public Animator creditAnimator;
    public GameObject typewriterUI;
    public Button returnButton;

    private TypewriterEffect typewriterEffect;

    private void Start()
    {
        returnButton.interactable = false;
        typewriterEffect = typewriterUI.GetComponent<TypewriterEffect>();
        returnButton.onClick.AddListener(ReturnToMainMenu);
        typewriterEffect.OnTextsFinished += StartCreditAnimation;
    }

    private void OnDestroy()
    {
        typewriterEffect.OnTextsFinished -= StartCreditAnimation;
    }

    private void StartCreditAnimation()
    {
        returnButton.interactable = true;
        typewriterUI.SetActive(false);
        creditAnimator.SetBool("StartCredits", true);
    }

    public void ReturnToMainMenu()
    {
        // Implemente o código para retornar à tela inicial do jogo aqui
        SceneManager.LoadScene(0);
    }
}
