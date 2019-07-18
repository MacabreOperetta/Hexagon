using UnityEngine;
using System.Collections.Generic;

public static class BombPieceManager 
{
	static List<BombPiece> liveBombsCheck = new List<BombPiece> ();

	public static void AddBombPieceToManage(BombPiece bombPiece, int startedNumber){
		// give the started move count
		bombPiece.startedNumberOfMove = startedNumber;
		bombPiece.countDownAmount = Random.Range (5, 7);
		liveBombsCheck.Add (bombPiece);
		bombPiece.WriteLeftNumberOfMovesCountToText (bombPiece.numberToReach - startedNumber);
	}

	public static void RemoveBombPieceFromManaging(BombPiece bombPiece){
		if (liveBombsCheck.Contains(bombPiece)) {
			liveBombsCheck.Remove (bombPiece);
		}
	}

	public static bool IsBombPieceExploded(int currentNumber){
		// for every bomb piece is a live
		for (int i = 0; i < liveBombsCheck.Count; i++) {
			// write to screen.
			liveBombsCheck [i].WriteLeftNumberOfMovesCountToText (liveBombsCheck [i].numberToReach - currentNumber);
			//check if its reached the zero.
			if ((liveBombsCheck [i].numberToReach) <= currentNumber) {
				GameSkeleton.gridSystem.destroyEveryPieceInMap (true);
				GameSkeleton.Instance.GameOver ();
				return true;
			}
		}
		return false;
	}
}
