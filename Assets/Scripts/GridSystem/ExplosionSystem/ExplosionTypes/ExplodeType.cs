using UnityEngine;
using System.Collections.Generic;

public abstract class ExplodeType
{
	public virtual bool IsAnyExplosionInTheSystem{ get { throw new System.NotImplementedException (); } }

	public virtual List<IndexGroup> GiveEveryGroupThatGoingToExplode(){ throw new System.NotImplementedException (); }
}
