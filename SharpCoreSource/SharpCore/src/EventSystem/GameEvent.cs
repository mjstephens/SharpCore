// From https://gist.github.com/michaeln-bsg/5602860605577c755ebf

using System;

namespace SharpCore.EventSystem
{
	public struct GameEvent 
	{
		public event Action OnRaised;
		public bool Raise () 
		{
			if (OnRaised != null) 
			{
				OnRaised ();
				return true;
			}
			return false;
		}
	}

	public struct GameEvent <T> 
	{
		public event Action <T> OnRaised;
		public bool Raise (T arg = default) 
		{
			if (OnRaised != null) 
			{
				OnRaised (arg);
				return true;
			}
			return false;
		}
	}

	public struct GameEvent <T1, T2> 
	{
		public event Action <T1, T2> OnRaised;
		public bool Raise (T1 arg1, T2 arg2) 
		{
			if (OnRaised != null) 
			{
				OnRaised (arg1, arg2);
				return true;
			}
			
			return false;
		}
	}
	//
	// public struct GameEvent <T1, T2, T3> 
	// {
	// 	public event Action <T1, T2, T3> Event;
	// 	public bool FireEvent (T1 arg1, T2 arg2, T3 arg3) 
	// 	{
	// 		if (Event != null) 
	// 		{
	// 			Event (arg1, arg2, arg3);
	// 			return true;
	// 		}
	// 		else
	// 			return false;
	// 	}
	// }
	//
	// public struct GameEvent <T1, T2, T3, T4> 
	// {
	// 	public event Action <T1, T2, T3, T4> Event;
	// 	public bool FireEvent (T1 arg1, T2 arg2, T3 arg3, T4 arg4) 
	// 	{
	// 		if (Event != null) 
	// 		{
	// 			Event (arg1, arg2, arg3, arg4);
	// 			return true;
	// 		}
	// 		else
	// 			return false;
	// 	}
	// }
}