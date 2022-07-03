using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusPopUpsUI : MonoBehaviour
{
    CanvasGroup canvasGroup;

    [SerializeField] GameObject popUpFineFull;
    [SerializeField] GameObject popUpFineDamage;
    [SerializeField] GameObject popUpCaution;
    [SerializeField] GameObject popUpDanger;

    [Header("Fade options")]
    [SerializeField] float timeBeforeFadeOutBegins = 2f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void DisableAllPopUps()
    {
        popUpFineFull.SetActive(false);
        popUpFineDamage.SetActive(false);
        popUpCaution.SetActive(false);
        popUpDanger.SetActive(false);
    }

    public void DisplayHealthPopUp(int playerHealthPercent)
    {
        if (playerHealthPercent >= 100)
        {
            popUpFineFull.SetActive(true);
        }
        else if (playerHealthPercent <= 99 && playerHealthPercent >= 66)
        {
            popUpFineDamage.SetActive(true);
        }
        else if (playerHealthPercent <= 66 && playerHealthPercent >= 30)
        {
            popUpCaution.SetActive(true);
        }
        else if (playerHealthPercent <= 29)
        {
            popUpDanger.SetActive(true);
        }

        StartCoroutine(FadeInPopUp());
    }

    IEnumerator FadeInPopUp()
    {
        for (float fade = 0.05f; fade < 1; fade += 0.05f)
        {
            canvasGroup.alpha = fade;

            if (fade > 0.9f)
            {
                StartCoroutine(FadeOutPopUp());
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator FadeOutPopUp()
    {
        yield return new WaitForSeconds(timeBeforeFadeOutBegins);

        for (float fade = 1f; fade > 0; fade -=0.05f)
        {
            canvasGroup.alpha = fade;

            if (fade <= 0.05f)
            {
                DisableAllPopUps();
            }

            yield return new WaitForSeconds(0.05f);
        }
    }
}
