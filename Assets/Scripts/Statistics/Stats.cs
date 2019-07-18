using UnityEngine;

[RequireComponent(typeof(IScore))]
[RequireComponent(typeof(IMoveCount))]
public class Stats : MonoBehaviour 
{
	public static IScore scoreSystem;
	public static IMoveCount movedCountSystem;

	void Awake(){
		scoreSystem = GetComponent<IScore> ();
		movedCountSystem = GetComponent<IMoveCount> ();
	}

	public void StartGame(){
		scoreSystem.StartGame ();
		movedCountSystem.StartGame ();
	}
}
