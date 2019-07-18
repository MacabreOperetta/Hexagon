using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public GameObject GameOverPanel;

	public void StartGame(){
		GameOverPanel.SetActive (false);
	}

	public void GameOver(){
		StartCoroutine (delayGameOverPanel ());
	}

	IEnumerator delayGameOverPanel(){
		yield return new WaitForSeconds (1f);
		GameOverPanel.SetActive (true);
	}
}
