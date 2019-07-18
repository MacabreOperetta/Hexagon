using UnityEngine;

public interface IParticleManager 
{
	Color[] Colors{ get; }
	
	void StartGame(Color[] particleColors);

	void ShowParticle(GridPiece explodedPiece);
}
