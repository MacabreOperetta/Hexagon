using UnityEngine;

public interface IColored 
{
	/// <summary>
	/// The color ındex of this piece.
	/// </summary>
	/// <value>The color ındex of this piece.</value>
	int ColorIndexOfThisPiece { get; set; }

	/// <summary>
	/// Changes the color.
	/// </summary>
	/// <param name="colorIndex">Color ındex.</param>
	void ChangeColor (int colorIndex, Color c);
}
