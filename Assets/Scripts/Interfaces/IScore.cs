using UnityEngine;

public interface IScore 
{
	int score { get;}


	void StartGame();

	void GroupExplodedAddScoreAndShowPlayer(int groupCount, Vector3 groupsMiddlePos);
	
}
