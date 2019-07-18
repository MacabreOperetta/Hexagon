using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriangleSelector : DefaultSelector 
{
	#region Turn
	/// <summary>
	/// The turn the routine of this selector.
	/// </summary>
	/// <returns>The routine.</returns>
	/// <param name="angleDir">Angle dir.</param>
	public override IEnumerator TurnRoutine (int angleDir)
	{
		GameSkeleton.inputManager.IsReadyForInput = false;
		SelectorManager selectorManager = GameSkeleton.selectorManager;

		//initialize selecteds
		List<Transform> selecteds = selectorManager.SelectedPieceGroup.AddThisGroupPiecesInToTheTransformList ();
		initializeSelectedsForTurn (selecteds);

		//get start rotation euler
		Vector3 startEuler = selectorManager.currentSelectorObject.transform.rotation.eulerAngles;

		//this is the algorith for that when in 3 selecteds turned on clockwise, if 2 of them in left 1 of them is right, indexes changes clockwise 
		//but the in other situation indexes changes counter-clockwise
		//index turn direction also can be found with middle index minus lower index == 1 or bigger,
		int indexTurnDir = selecteds.CheckIfItsHaveAHighestXValue () == true ? angleDir : -angleDir;

		for (int i = 0; i < 3; i++) {
			//turn the selector
			for (int x = 0; x < 10; x++) {
				startEuler.z += (12f * angleDir);
				selectorManager.currentSelectorObject.transform.rotation = Quaternion.Euler (startEuler);
				yield return new WaitForFixedUpdate ();
			}
			//correct Selecteds rotation
			for (int b = 0; b < selecteds.Count; b++) {
				selecteds [b].GetComponent<GridPiece> ().CorrectRotationWhenSelectorTurns ();
			}
			//turn idexes according to indexTurnDirection
			TurnSelectedIndexesInGridSystem (indexTurnDir);
			//wait for smoothness
			yield return new WaitForSeconds (0.05f);
			//check for any explosion
			if (ExplosionSystem.IsAnyExplosionInTheSystem) {
				resetSelectedsAfterTurn (selecteds);
				selectorManager.currentSelectorObject.SetActive (false);
				Stats.movedCountSystem.ActionMaded ();
				yield return GameSkeleton.Instance.StartCoroutine (ExplosionSystem.Explode ());
				yield break;
			}
		}
		resetSelectedsAfterTurn (selecteds);

		GameSkeleton.inputManager.IsReadyForInput = true;
	}

	#region Initialize and reset selecteds in turn
	void initializeSelectedsForTurn(List<Transform> selecteds){
		for (int i = 0; i < selecteds.Count; i++) {
			selecteds [i].parent = GameSkeleton.selectorManager.currentSelectorObject.transform;
			selecteds [i].GetComponent<SpriteRenderer> ().sortingOrder = 1;
		}
	}

	void resetSelectedsAfterTurn(List<Transform> selecteds){
		for (int i = 0; i < selecteds.Count; i++) {
			selecteds [i].parent = GameSkeleton.gridSystem.transform;
			selecteds [i].GetComponent<SpriteRenderer> ().sortingOrder = 0;
		}
	}
	#endregion
	#endregion
	#region IndexChanges
	/// <summary>
	/// Turns the selected ındexes ın grid system.
	/// </summary>
	/// <param name="angleDir">Angle dir.</param>
	public override void TurnSelectedIndexesInGridSystem (int angleDir)
	{
		IndexGroup tempProcessList = new IndexGroup (GameSkeleton.selectorManager.SelectedPieceGroup);
		tempProcessList.Values.Sort ();
		GridPiece low = GridSystem.GridMap [tempProcessList.Values [0]],
		mid = GridSystem.GridMap [tempProcessList.Values [1]],
		high = GridSystem.GridMap [tempProcessList.Values [2]];
		if (angleDir == -1) {//clockwise
			GridSystem.GridMap [tempProcessList.Values [0]] = mid;
			GridSystem.GridMap [tempProcessList.Values [1]] = high;
			GridSystem.GridMap [tempProcessList.Values [2]] = low;
		} else {//counter-clockwise
			GridSystem.GridMap [tempProcessList.Values [0]] = high;
			GridSystem.GridMap [tempProcessList.Values [1]] = low;
			GridSystem.GridMap [tempProcessList.Values [2]] = mid;
		}
	}
	#endregion
	#region This Selector's rotation pattern.
	/// <summary>
	/// This Selector's rotation pattern According to selected indexes
	/// </summary>
	/// <param name="selectedIndexes">Selected ındexes.</param>
	public override void GiveThisSelectorARotation(IndexGroup selectedIndexes){
		//for rotation find the lowest y and find middle of the other two and create a angle of that.
		//Initialize list
		List<Transform> selecteds = new List<Transform>();
		for (int i = 0; i < selectedIndexes.Values.Count; i++) {
			selecteds.Add (GridSystem.GridMap [selectedIndexes.Values [i]].transform);
		}
		//find lowest
		int lowestYIndex = selecteds.findLowestYIndexOnAMapPieceList();
		//middle of the other two
		Vector2 mPointOf2 = Vector2.zero;
		for (int i = 0; i < selecteds.Count; i++) {
			if (i != lowestYIndex) {
				Vector2 tempPos = selecteds [i].position;
				mPointOf2 += tempPos;
			}
		}
		mPointOf2 /= (selecteds.Count - 1);
		//Create angle between lowestY piece and middle point of the other two
		float z = selecteds[lowestYIndex].TheAngleOfPositionRelativeToThisTransform(mPointOf2);
		transform.rotation = Quaternion.Euler (0f, 0f, z);
	}
	#endregion
}
