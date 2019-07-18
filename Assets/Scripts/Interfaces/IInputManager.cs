using UnityEngine;
using System.Collections;

public interface IInputManager 
{
	bool IsReadyForInput{ get; set;}


	void Update();

	void readInput ();

	void onTouchBegin();

	void onTouchEnd();

	IEnumerator waitForSwipe();
}
