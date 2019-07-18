using UnityEngine;
using System.Collections;

public abstract class GridPiece : MonoBehaviour 
{
	public virtual void SelectGroupAccordingToPos(Vector3 Pos){
		SelectorManager selectorManager = GameSkeleton.selectorManager;
		selectorManager.SetSelectorObjectByPieceType (this.GetType ());
		selectorManager.SelectedPieceGroup = GameSkeleton.gridSystem.GiveClosestIndexesOfAGridPieceInGridSystem (this, Pos, 2);
		selectorManager.SelectObjectsWithCurrentSelector ();
	}

	public IEnumerator MoveToPosRoutine(Vector2 posToGo){
		float deltaPos = Vector3.Distance(transform.localPosition, posToGo) > 3.9f ? 0.2f : 0.08f;
		for (int i = 0; i < 80; i++) {
			transform.localPosition = Vector2.MoveTowards (transform.localPosition, posToGo, deltaPos);
			if (Vector3.Distance(transform.localPosition, posToGo) < 0.001f) {
				transform.localPosition = posToGo;
				yield break;
			}
			yield return new WaitForFixedUpdate ();
		}
	}

	public virtual void CorrectRotationWhenSelectorTurns(){
		transform.rotation = Quaternion.Euler (0f, 0f, 0f);
	}
}
