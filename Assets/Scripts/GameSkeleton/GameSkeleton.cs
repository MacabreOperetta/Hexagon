using UnityEngine;

[RequireComponent(typeof(GridSystem))]
[RequireComponent(typeof(SelectorManager))]
[RequireComponent(typeof(Stats))]
[RequireComponent(typeof(IInputManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(IParticleManager))]
[RequireComponent(typeof(AudioSource))]
public class GameSkeleton : MonoBehaviour 
{
	internal static GameSkeleton Instance;
	#region References for Injecting dependencies & starting and finishing game.
	internal static GridSystem gridSystem;
	internal static SelectorManager selectorManager;
	internal static Stats stats;
	internal static IInputManager inputManager;
	internal static UIManager UImanager;
	internal static IParticleManager particleManager;
    internal static AudioSource audoisource;
	#endregion

	public GameMode CurrentGameMode;

	void Awake(){
		gridSystem = GetComponent<GridSystem> ();
		selectorManager = GetComponent<SelectorManager> ();
		stats = GetComponent<Stats> ();
		inputManager = GetComponent<IInputManager> ();
		UImanager = GetComponent<UIManager> ();
		particleManager = GetComponent<IParticleManager> ();
        audoisource = GetComponent<AudioSource>();
		Instance = this;
	}

	void Start(){
		StartGame ();
        audoisource.Play();
        
	}
	public void StartGame(){
		stats.StartGame ();
		UImanager.StartGame ();
		Color[] cS = CurrentGameMode.Colors.ToArray ();
		particleManager.StartGame (cS);
		gridSystem.StartGame (CurrentGameMode.GridXLength, CurrentGameMode.GridYLength, CurrentGameMode.BombPieceInstantiateEveryXPoint.Value, cS);
		selectorManager.StartGame (gridSystem.oneSideScale, CurrentGameMode.GridElements);
		ExplosionSystem.StartGame (CurrentGameMode.ExplosionTypes, CurrentGameMode.GridYLength);
		GameSkeleton.inputManager.IsReadyForInput = true;
	}
	public void GameOver(){
		UImanager.GameOver ();
		GameSkeleton.inputManager.IsReadyForInput = false;
        audoisource.Stop();
	}

	public void GameOverChecks(){
		if (gridSystem.IsAnyOtherMoveExist () && !BombPieceManager.IsBombPieceExploded (Stats.movedCountSystem.moveCount)) {
			selectorManager.currentSelectorObject.SetActive (true);
			GameSkeleton.inputManager.IsReadyForInput = true;
		} 
	}
}
