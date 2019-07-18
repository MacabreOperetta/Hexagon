using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HexagonPiece : GridPiece, IColored
{
	public int ColorIndexOfThisPiece { get; set; }

	public void ChangeColor(int colorIndex, Color c){
		ColorIndexOfThisPiece = colorIndex;
		GetComponent<SpriteRenderer> ().color = c;
	}

	public override void CorrectRotationWhenSelectorTurns ()
	{
		transform.rotation = Quaternion.Euler (0f, 0f, 90f);
	}
}