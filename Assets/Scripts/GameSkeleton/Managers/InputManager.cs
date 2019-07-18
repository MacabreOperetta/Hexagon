using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour, IInputManager 
{
	public bool IsReadyForInput { get; set;}
	static bool isTurned;

	public void Update(){
		if (IsReadyForInput) {
			readInput ();
		}
	}

	#region Input process
	public void readInput(){
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			onTouchBegin ();
		} else if (Input.GetKeyUp(KeyCode.Mouse0)) {
			if (isTurned) {	isTurned = false; return;	}
			onTouchEnd ();
		}
	}

	public void onTouchBegin(){
		isTurned = false;
		startSwipeControl ();
	}

	public void onTouchEnd(){
		stopSwipeControl ();
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		RaycastHit2D rayHit2d = Physics2D.Raycast (mousePos, Vector2.zero);
		if (rayHit2d.collider != null) {
			rayHit2d.collider.GetComponent<GridPiece> ().SelectGroupAccordingToPos (mousePos);
		} else {
			GameSkeleton.selectorManager.ResetSelectAction ();
		}
	}
	#endregion

	#region Swipe controls
	public void startSwipeControl(){
		if (GameSkeleton.selectorManager.currentSelectorObject == null) {
			return;
		}
		stopSwipeControl ();
		swipeControlRoutine = StartCoroutine (waitForSwipe ());
	}

	public void stopSwipeControl(){
		if (swipeControlRoutine != null) {
			StopCoroutine (swipeControlRoutine);
			swipeControlRoutine = null;
		}
	}

	Coroutine swipeControlRoutine;
	public IEnumerator waitForSwipe(){
		SelectorManager selectorManager = GameSkeleton.selectorManager;
		float firstAngle = selectorManager.GiveAngleToThePositionRelativeToCurrentSelector ();
		yield return new WaitUntil (() => firstAngle.GetDifferenceBetweenTwoAngle(selectorManager.GiveAngleToThePositionRelativeToCurrentSelector ()) > 15f);
		float secondAngle = selectorManager.currentSelectorObject.transform.TheAngleOfPositionRelativeToThisTransform (Camera.main.ScreenToWorldPoint (Input.mousePosition));

		isTurned = true;
		bool isAngleFlipped = Mathf.Abs (firstAngle - secondAngle) > 270f ? true : false;

		if (isAngleFlipped) {
			if ((secondAngle > firstAngle)){
				selectorManager.currentSelectorObject.GetComponent<DefaultSelector> ().StartTurnRoutine (-1);
			} else {
				selectorManager.currentSelectorObject.GetComponent<DefaultSelector> ().StartTurnRoutine (+1);
			}
		} else {
			if ((secondAngle < firstAngle)){
				selectorManager.currentSelectorObject.GetComponent<DefaultSelector> ().StartTurnRoutine (-1);
			} else {
				selectorManager.currentSelectorObject.GetComponent<DefaultSelector> ().StartTurnRoutine (+1);
			}
		}
	}
	#endregion
}
