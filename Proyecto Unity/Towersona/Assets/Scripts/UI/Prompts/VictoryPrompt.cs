using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

#pragma warning disable 649
public class VictoryPrompt : MonoBehaviour
{
    [SerializeField]
    private Image[] stars;

    public void ShowVictoryPrompt(int score)    
    {
        if(score < 1 || score > 3)
        {
            Debug.LogError("End score not valid. Should be 1, 2 or 3");
            return;            
        }

        gameObject.SetActive(true);

        //Starts animation
        Sequence starsSequence = DOTween.Sequence();
        starsSequence.AppendInterval(1f);

        for (int i = 0; i < score; i++)
        {
            Color original = stars[i].color;
            original.a = 1f;

            starsSequence.Append(stars[i].DOFade(1f, .1f));
            starsSequence.Join(stars[i].transform.DOScale(2, .2f));
            starsSequence.Join(stars[i].DOColor(Color.white, .2f));

            starsSequence.AppendInterval(.1f);
            
            
            starsSequence.Append(stars[i].transform.DOScale(1.1f, .7f));
            starsSequence.Join(stars[i].DOColor(original, .4f));

            starsSequence.AppendCallback(() => stars[i].transform.GetChild(0).gameObject.SetActive(true));

        }


    }
}
