using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour, IScore
{
	public int score { get; private set;}
	private int highScore;
	public Text ScoreText, HighScoreText;

	const int oneBlockPoint = 5;
	const string highScoreString = "highScore";

	internal GameObject movingTextPrefab;

	void Awake(){
		movingTextPrefab = Resources.Load ("Prefabs/movingScoreText") as GameObject;
		highScore = PlayerPrefs.GetInt (highScoreString, 0);
	}

	public void StartGame(){
		score = 0;
		scoreWriteToText ();
		highScoreWriteToText ();
	}

	public void GroupExplodedAddScoreAndShowPlayer(int groupCount, Vector3 groupsMiddlePos){
		int addedScore = oneBlockPoint * groupCount;
		score += addedScore;
		if (score > highScore) {
			highScore = score;
			highScoreWriteToText ();
		}
		scoreWriteToText ();
		showAddedScoreToThePlayer (addedScore, groupsMiddlePos);
	}

	void showAddedScoreToThePlayer(int addedScore, Vector3 groupsMiddlePos){
		Instantiate (movingTextPrefab).GetComponent<movingScoreText> ().GoUp (addedScore, groupsMiddlePos);
	}

	void scoreWriteToText(){
		ScoreText.text = score.ToString ();
	}

	void highScoreWriteToText(){
		PlayerPrefs.SetInt (highScoreString, highScore);
		HighScoreText.text = highScore.ToString ();
	}
}
