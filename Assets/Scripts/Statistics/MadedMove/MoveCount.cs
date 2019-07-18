using UnityEngine;
using UnityEngine.UI;

public class MoveCount : MonoBehaviour, IMoveCount 
{
	public int moveCount{ get; private set; }

	public Text moveText;

	public void StartGame(){
		moveCount = 0;
		writeToText ();
	}

	public void ActionMaded(){
		moveCount++;
		writeToText ();
	}

	void writeToText(){
		moveText.text = moveCount.ToString ();
	}
}
