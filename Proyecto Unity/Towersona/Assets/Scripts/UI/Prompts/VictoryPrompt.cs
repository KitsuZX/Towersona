using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

#pragma warning disable 649
public class VictoryPrompt : MonoBehaviour
{
    [SerializeField]
    private Image[] stars;

	[SerializeField]
	private Button nextLevelButton;

    private void Awake()
    {
        gameObject.transform.localScale *= 0;
    }

	private void OnEnable()
	{
		if(LevelManager.Instance.LevelIndex == SaveSystem.levelsData.Length)
		{
			nextLevelButton.interactable = false;
		}
	}

	public void ShowVictoryPrompt(int score)    
    {
        if(score < 1 || score > 3)
        {
            Debug.LogError("End score not valid. Should be 1, 2 or 3");
            return;            
        }

        gameObject.SetActive(true);

		int prevScore = 0;		
		
		prevScore = SaveSystem.levelsData[LevelManager.Instance.LevelIndex].score;		

        //Starts animation
        Sequence starsSequence = DOTween.Sequence();
        starsSequence.Append(gameObject.transform.DOScale(Vector3.one * 1.2f, 0.5f));       
        starsSequence.Append(gameObject.transform.DOScale(Vector3.one, 0.1f));       
        starsSequence.AppendInterval(1f);

		//Poner las estrellas que ya se han conseguido
		for (int i = 0; i < prevScore; i++)
		{
			Color c = stars[i].color;
			c.a = 1f;

			stars[i].color = c;
			stars[i].transform.localScale = Vector3.one * 1.1f;
		}

		//Animaciones
        for (int i = prevScore; i < score; i++)
        {
            Color original = stars[i].color;
            original.a = 1f;

            starsSequence.Append(stars[i].DOFade(1f, .1f));
            starsSequence.Join(stars[i].transform.DOScale(2, .2f));
            starsSequence.Join(stars[i].DOColor(Color.white, .2f));

            starsSequence.AppendInterval(.1f);            
            
            starsSequence.Append(stars[i].transform.DOScale(1.1f, .7f));
            starsSequence.Join(stars[i].DOColor(original, .7f));           

        }
    }

	public void NextLevel() {
		SceneController.LoadScene("Level_" + (LevelManager.Instance.LevelNumber + 1).ToString());
	}

	public void ReplayLevel()
	{
		SceneController.LoadScene("Level_" + LevelManager.Instance.LevelNumber);
	}
}
