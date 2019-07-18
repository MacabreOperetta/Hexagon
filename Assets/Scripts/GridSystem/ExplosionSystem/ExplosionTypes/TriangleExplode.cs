using UnityEngine;
using System.Collections.Generic;

public class TriangleExplode : ExplodeType 
{
	public override bool IsAnyExplosionInTheSystem{ get { return GridSystem.EverySelectableTriangleInGridSystem.CheckForGroupElementsHaveAGivenCountOfDifferentColor (1); } }

	public override List<IndexGroup> GiveEveryGroupThatGoingToExplode ()
	{
		return GridSystem.EverySelectableTriangleInGridSystem.FindAndGiveEveryGroupThatHaveAGivenCountOfColor (1, true);
	}
}
