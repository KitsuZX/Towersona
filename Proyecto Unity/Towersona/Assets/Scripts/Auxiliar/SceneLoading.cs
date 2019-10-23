using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

#pragma warning disable 649
public class SceneLoading : MonoBehaviour
{
    [SerializeField] private Image loadBar;
	[SerializeField] private TextMeshProUGUI text;

	// Start is called before the first frame update
	void Start()
    {
        //Start async operation
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(SceneController.sceneToLoad);

        while (!asyncLoad.isDone)
        {
			text.text = "Loading progress: " + Mathf.FloorToInt(asyncLoad.progress * 100) + '%';
			loadBar.fillAmount = asyncLoad.progress;
			yield return new WaitForEndOfFrame();
        }
    }
}
