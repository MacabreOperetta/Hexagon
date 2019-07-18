using UnityEngine;

public class movingScoreText : MonoBehaviour 
{
	Vector3 targetPos;

	public void GoUp(int addedScore, Vector3 groupsMiddlePos){
		transform.position = groupsMiddlePos + Vector3.back;
		targetPos = transform.position + Vector3.up * 0.8f;
		GetComponent<TextMesh> ().text = addedScore.ToString ();
		gameObject.SetActive (true);
	}

	void FixedUpdate(){
		transform.position = Vector3.MoveTowards (transform.position, targetPos, 0.013f);
		if (Vector3.Distance (transform.position, targetPos) < 0.013f) {
			Destroy (gameObject);
		}
	}
}
