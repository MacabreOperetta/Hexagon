using UnityEngine;

public class BombPiece : HexagonPiece 
{
	internal int startedNumberOfMove, countDownAmount;
	internal int numberToReach { get { return startedNumberOfMove + countDownAmount; } }

	public void WriteLeftNumberOfMovesCountToText(int leftNumberOfMoves){
		GetComponentInChildren<TextMesh> ().text = leftNumberOfMoves.ToString ();
	}

	void OnEnable(){
		BombPieceManager.AddBombPieceToManage (this, Stats.movedCountSystem.moveCount);
	}

	void OnDisable(){
		BombPieceManager.RemoveBombPieceFromManaging (this);
	}

	public override void CorrectRotationWhenSelectorTurns ()
	{
		transform.rotation = Quaternion.Euler (0f, 0f, 0f);
	}
}
