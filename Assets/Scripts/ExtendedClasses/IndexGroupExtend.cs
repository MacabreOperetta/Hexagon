using UnityEngine;
using System.Collections.Generic;

public static class IndexGroupExtend 
{
	
	/// <summary>
	/// Gives the color ındex of A multiple same coloreds ın the group.
	/// </summary>
	/// <returns>The color ındex of A multiple same coloreds ın the group.</returns>
	/// <param name="triangle">Triangle.</param>
	public static int GiveColorIndexOfAMultipleSameColoredsInTheGroup(this IndexGroup triangle){
		IndexGroup colorsIndexesToCheck = new IndexGroup ();

		for (int k = 0; k < triangle.Values.Count; k++) {
			//if color index not in the group, the add in it.
			if (!colorsIndexesToCheck.Values.Contains (GridSystem.GridMap [triangle.Values [k]].GetComponent<IColored> ().ColorIndexOfThisPiece)) {
				colorsIndexesToCheck.Values.Add (GridSystem.GridMap [triangle.Values [k]].GetComponent<IColored> ().ColorIndexOfThisPiece);
			} 
			//if it is in the group, then this is the multiple colored one, return its index.
			else {
				return GridSystem.GridMap [triangle.Values [k]].GetComponent<IColored> ().ColorIndexOfThisPiece;
			}
		}
		return -2;
	}

	/// <summary>
	/// Gives the grid ındex of A different colored ın the group.
	/// </summary>
	/// <returns>The grid ındex of A different colored ın the group.</returns>
	/// <param name="group">Group.</param>
	public static int GiveGridIndexOfADifferentColoredInTheGroup(this IndexGroup group){
		IndexGroup colorsIndexesToCheck = new IndexGroup (), triangleIndexesToRemove = new IndexGroup (group);

		for (int k = 0; k < group.Values.Count; k++) {
			//if color index not in the group, the add in it.
			if (!colorsIndexesToCheck.Values.Contains (GridSystem.GridMap [group.Values [k]].GetComponent<IColored> ().ColorIndexOfThisPiece)) {
				colorsIndexesToCheck.Values.Add (GridSystem.GridMap [group.Values [k]].GetComponent<IColored> ().ColorIndexOfThisPiece);
			} 
			//if it is in the group, then this is the multiple colored one.
			else {
				//color index of a multiple colored one.
				int colorIndex = GridSystem.GridMap [group.Values [k]].GetComponent<IColored> ().ColorIndexOfThisPiece;
				//remove the multiple colored one from the list.
				triangleIndexesToRemove.Values.Remove (group.Values [k]);
				for (int i = 0; i < 2; i++) {
					//check the other one with multiple colored.
					if (GridSystem.GridMap[triangleIndexesToRemove.Values [i]].GetComponent<IColored>().ColorIndexOfThisPiece == colorIndex) {
						//remove from the list.
						triangleIndexesToRemove.Values.RemoveAt (i);
						//return different colored one.
						return triangleIndexesToRemove.Values [0];
					}
				}
			}
		}
		return -2;
	}

	/// <summary>
	/// Determines if ıs list triangles have A same elements.
	/// </summary>
	/// <returns><c>true</c> if ıs list triangles have A same elements; otherwise, <c>false</c>.</returns>
	/// <param name="firstTriange">First triange.</param>
	/// <param name="secondTriangle">Second triangle.</param>
	public static bool IsListTrianglesHaveASameElements(this IndexGroup firstTriange, IndexGroup secondTriangle){
		for (int i = 0; i < firstTriange.Values.Count; i++) {
			if (!secondTriangle.Values.Contains (firstTriange.Values [i])) {
				return false;//Have a different Element
			}
		}
		return true;
	}

	/// <summary>
	/// Determines if ıs list triangles have A one or more same element.
	/// </summary>
	/// <returns><c>true</c> if ıs list triangles have A one or more same element;
	/// otherwise, <c>false</c>.</returns>
	/// <param name="firstTriange">First triange.</param>
	/// <param name="secondTriangle">Second triangle.</param>
	public static bool IsListTrianglesHaveAOneOrMoreSameElement(this IndexGroup firstTriange, IndexGroup secondTriangle){
		for (int i = 0; i < firstTriange.Values.Count; i++) {
			if (secondTriangle.Values.Contains (firstTriange.Values [i])) {
				return true;//Have a Same Element
			}
		}
		return false;
	}

	/// <summary>
	/// Merges the groups to one bigger with no repeatign element.
	/// </summary>
	/// <returns>The groups to one bigger with no repeatign element.</returns>
	/// <param name="triangleHolder">Triangle holder.</param>
	public static IndexGroup MergeGroupsToOneBiggerWithNoRepeatignElement(this List<IndexGroup> triangleHolder){
		IndexGroup biggerGroup = new IndexGroup (triangleHolder[0]);
		for (int i = 1; i < triangleHolder.Count; i++) {
			biggerGroup = MergeGroupsToOneBiggerWithNoRepeatignElement (triangleHolder [i], biggerGroup);
		}
		return biggerGroup;
	}

	public static IndexGroup MergeGroupsToOneBiggerWithNoRepeatignElement(this IndexGroup firstTriangle, IndexGroup secondTriangle){
		IndexGroup biggerGroup = new IndexGroup (firstTriangle);
		for (int i = 0; i < secondTriangle.Values.Count; i++) {
			if (!biggerGroup.Values.Contains (secondTriangle.Values [i])) {
				biggerGroup.Values.Add (secondTriangle.Values [i]);
			}
		}
		return biggerGroup;
	}


	public static bool IsHolderContainsThisList(this IndexGroup group, List<IndexGroup> groupHolder){
		for (int k = 0; k < groupHolder.Count; k++) {
			if (group.IsListTrianglesHaveASameElements (groupHolder [k])) {
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Checks the color of the for group elements have A given count of different.
	/// </summary>
	/// <returns><c>true</c>, if for group elements have A given count of different color was checked, <c>false</c> otherwise.</returns>
	/// <param name="triangles">Triangles.</param>
	/// <param name="colorCount">Color count.</param>
	public static bool CheckForGroupElementsHaveAGivenCountOfDifferentColor(this List<IndexGroup> triangles, int colorCount){
		for (int i = 0; i < triangles.Count; i++) {
			IndexGroup colorsIndexesToCheck = new IndexGroup ();

			for (int k = 0; k < triangles [i].Values.Count; k++) {
				//check if this added to color Indexes list. if it is not, then add it.
				if (!colorsIndexesToCheck.Values.Contains (GridSystem.GridMap [triangles [i].Values [k]].GetComponent<IColored> ().ColorIndexOfThisPiece)) {
					colorsIndexesToCheck.Values.Add (GridSystem.GridMap [triangles [i].Values [k]].GetComponent<IColored> ().ColorIndexOfThisPiece);
				}
			}
			//check if the color of the group elements have a given count of differen.
			if (colorsIndexesToCheck.Values.Count == colorCount) {
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Adds the this group pieces ın to the transform list.
	/// </summary>
	/// <returns>The this group pieces ın to the transform list.</returns>
	/// <param name="group">Group.</param>
	public static List<Transform> AddThisGroupPiecesInToTheTransformList (this IndexGroup group){
		List<Transform> selecteds = new List<Transform> ();
		for (int i = 0; i < group.Values.Count; i++) {
			Transform tempPieceTransform = GridSystem.GridMap [group.Values [i]].transform;
			selecteds.Add (tempPieceTransform);
		}
		return selecteds;
	}

	/// <summary>
	/// Gives the middle point of ındex group.
	/// </summary>
	/// <returns>The middle point of ındex group.</returns>
	/// <param name="selecteds">Selecteds.</param>
	public static Vector2 GiveMiddlePointOfIndexGroup(this IndexGroup selecteds){
		List<Vector2> forMiddlePointCalc = new List<Vector2>();
		for (int i = 0; i < selecteds.Values.Count; i++) {
			forMiddlePointCalc.Add (GridSystem.GridMap [selecteds.Values [i]].transform.position);
		}
		return forMiddlePointCalc.giveMiddePointOfVectors ();
	}

	public static List<IndexGroup> FindAndGiveEveryGroupThatHaveAGivenCountOfColor(this List<IndexGroup> groupHolder, int colorCount, bool ifItsHaveCommonElement_MergeToBiggerList){
		List<IndexGroup> givenAmountOfGroups = new List<IndexGroup> ();

		//in every possible triangle in the grid system
		for (int i = 0; i < groupHolder.Count; i++) {

			IndexGroup colorsIndexesToCheck = new IndexGroup ();
			//add triangle colors to a list.
			for (int k = 0; k < groupHolder [i].Values.Count; k++) {
				if (!colorsIndexesToCheck.Values.Contains (GridSystem.GridMap [groupHolder [i].Values [k]].GetComponent<IColored> ().ColorIndexOfThisPiece)) {
					colorsIndexesToCheck.Values.Add (GridSystem.GridMap [groupHolder [i].Values [k]].GetComponent<IColored> ().ColorIndexOfThisPiece);
				}
			}

			//check for list have given amount of color, if it is, add to explodingGroup's inside
			if (colorsIndexesToCheck.Values.Count == colorCount) {
				if (ifItsHaveCommonElement_MergeToBiggerList) {
					if (!groupHolder [i].IsHolderContainsThisList (givenAmountOfGroups)) {
						//ortak indexler içeren gruplar aynı renk ve birleşik gruplar olduklarından çoklu patlayacaklar
						//ondan bunları bir ortak grup olarak alıyoruz.
						bool oneChangedToABigger = false;
						for (int b = 0; b < givenAmountOfGroups.Count; b++) {
							if (givenAmountOfGroups [b].IsListTrianglesHaveAOneOrMoreSameElement (groupHolder [i])) {
								oneChangedToABigger = true;
								givenAmountOfGroups [b] = givenAmountOfGroups [b].MergeGroupsToOneBiggerWithNoRepeatignElement (groupHolder [i]);
								break;
							}
						}
						if (!oneChangedToABigger) {
							givenAmountOfGroups.Add (groupHolder [i]);
						}
					}
				} else {
					givenAmountOfGroups.Add (groupHolder [i]);
				}
				continue;
			}

		}
		return givenAmountOfGroups;
	}
}
