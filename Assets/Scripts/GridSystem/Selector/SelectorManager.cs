using UnityEngine;
using System.Collections.Generic;

public class SelectorManager : MonoBehaviour 
{
	internal GameObject currentSelectorObject;
	List<GameObject> selectorObjects;

	internal IndexGroup SelectedPieceGroup;

	List<MapElementInfo> mapElements;

	#region Initialize selectors
	public void StartGame(float oneSideScale, List<MapElementInfo> _mapElements){
		mapElements = _mapElements;
		SelectedPieceGroup = new IndexGroup ();
		selectorObjects = new List<GameObject>();
		for (int i = 0; i < mapElements.Count; i++) {
			selectorObjects.Add (Instantiate (mapElements [i].SelectorPrefab));
			selectorObjects [i].transform.localScale = new Vector3 (oneSideScale, oneSideScale, 1f);
			selectorObjects [i].SetActive (false);
		}
	}
	#endregion
	#region Selection
	public void SelectObjectsWithCurrentSelector(){
		Vector2 mPoint = SelectedPieceGroup.GiveMiddlePointOfIndexGroup ();

		currentSelectorObject.transform.position = mPoint;
		currentSelectorObject.GetComponent<DefaultSelector> ().GiveThisSelectorARotation (SelectedPieceGroup);
		currentSelectorObject.SetActive (true);
	}

	public void SetSelectorObjectByPieceType(System.Type piece){
		ResetSelectAction ();
		currentSelectorObject = selectorObjects [mapElements.FindIndex (x => x.GridPieces.Exists (y => y.GetType () == piece))];
	}

	public void ResetSelectAction(){
		SelectedPieceGroup.Values.Clear ();
		if (currentSelectorObject) {
			currentSelectorObject.SetActive (false);
			currentSelectorObject = null;
		}
	}
	#endregion
	#region func
	public float GiveAngleToThePositionRelativeToCurrentSelector(){
		return currentSelectorObject.transform.TheAngleOfPositionRelativeToThisTransform (Camera.main.ScreenToWorldPoint (Input.mousePosition));
	}
	#endregion
}
