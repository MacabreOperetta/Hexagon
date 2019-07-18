using UnityEngine;
using System.Collections.Generic;

public static class ListExtend 
{
	#region List<Transform>
	/// <summary>
	/// Finds the lowest position.y element index on A map piece list.
	/// </summary>
	/// <returns>The lowest Y ındex on A map piece list.</returns>
	/// <param name="selecteds">Selecteds.</param>
	public static int findLowestYIndexOnAMapPieceList(this List<Transform> selecteds){
		float lowestY = 100f;
		int lowestYIndex = 0;
		for (int i = 0; i < selecteds.Count; i++) {
			//check if its the lower on our lowest value
			if (selecteds[i].position.y < lowestY) {
				//if it is, take new lowest value and its index for return.
				lowestY = selecteds[i].position.y;
				lowestYIndex = i;
			}
		}
		return lowestYIndex;
	}

	/// <summary>
	/// Checks the lowest position.y element have a highest position.x value.
	/// </summary>
	/// <returns><c>true</c>,  if lowest y element have a highest X value was checked, <c>false</c> otherwise.</returns>
	/// <param name="selecteds">Selecteds.</param>
	public static bool CheckIfItsHaveAHighestXValue(this List<Transform> selecteds){
		int lowestYIndex = selecteds.findLowestYIndexOnAMapPieceList ();
		for (int i = 0; i < 3; i++) {
			if ((selecteds [lowestYIndex].position.x + 0.01f) < selecteds [i].position.x) {
				return false;//lowest y not have a highest x value.
			}
		}
		return true;
	}
	#endregion
	#region List<Vector2>
	/// <summary>
	/// Gives the midde point of vectors.
	/// </summary>
	/// <returns>The midde point of vectors.</returns>
	/// <param name="vectorsToProcess">Vectors to process.</param>
	public static Vector2 giveMiddePointOfVectors (this List<Vector2> vectorsToProcess){
		Vector2 mPoint = Vector2.zero;
		int count = vectorsToProcess.Count;
		//add all vectors to new vector.
		for (int i = 0; i < count; i++) {
			mPoint += vectorsToProcess [i];
		}
		//divede by the added count
		mPoint.x /= count;
		mPoint.y /= count;
		return mPoint;
	}
	#endregion
	#region List<Vector3>
	/// <summary>
	/// Gives the midde point of vectors.
	/// </summary>
	/// <returns>The midde point of vectors.</returns>
	/// <param name="vectorsToProcess">Vectors to process.</param>
	public static Vector3 giveMiddePointOfVectors (this List<Vector3> vectorsToProcess){
		Vector3 mPoint = Vector3.zero;
		int count = vectorsToProcess.Count;
		//add all vectors to new vector.
		for (int i = 0; i < count; i++) {
			mPoint += vectorsToProcess [i];
		}
		//divede by the added count
		mPoint.x /= count;
		mPoint.y /= count;
		mPoint.z /= count;
		return mPoint;
	}
	#endregion
}