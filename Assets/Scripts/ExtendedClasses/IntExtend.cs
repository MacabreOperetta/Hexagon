using UnityEngine;

public static class IntExtend 
{
	/// <summary>
	/// column of the ındex.
	/// </summary>
	/// <returns>The ındex.</returns>
	/// <param name="index">İndex.</param>
	/// <param name="yLength">Y length.</param>
	public static int GetColumnOfIndex(this int index, int yLength){
		return (index / yLength);
	}

	/// <summary>
	/// Row of the ındex.
	/// </summary>
	/// <returns>The of the ındex.</returns>
	/// <param name="index">İndex.</param>
	/// <param name="yLength">Y length.</param>
	public static int GetRowOfIndex(this int index, int yLength){
		return (index % yLength);
	}
}
