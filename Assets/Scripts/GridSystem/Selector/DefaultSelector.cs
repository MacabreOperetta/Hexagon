using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class DefaultSelector : MonoBehaviour 
{
	public virtual void TurnSelectedIndexesInGridSystem(int angleDir){	throw new System.NotImplementedException (); }

	public virtual IEnumerator TurnRoutine (int angleDir) {	throw new System.NotImplementedException (); }

	public virtual void GiveThisSelectorARotation(IndexGroup selectedIndexes){ throw new System.NotImplementedException (); }

	public void StartTurnRoutine(int angleDir){
		StartCoroutine (TurnRoutine (angleDir));
	}
}
