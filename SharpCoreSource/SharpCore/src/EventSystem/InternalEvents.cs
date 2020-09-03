namespace SharpCore.EventSystem
{
	/// <summary>
	/// MARKED FOR DELETION
	/// </summary>
	public static class InternalEvent
	{
		// DEBUG
		public static GameEvent RENDER_MATERIAL_TOGGLE = new GameEvent ();
		public static GameEvent SIMULATION_MATERIAL_TOGGLE = new GameEvent ();

		// Directory
		public static GameEvent <string>DIRSYS_FILEREAD = new GameEvent <string>();
		public static GameEvent <string>DIRSYS_FILEWRITE = new GameEvent <string>();

		// Logging
		// public static GameEvent <string, LogTypeConfigData.LogType> LOGSYS_LOG = 
		// 	new GameEvent<string, LogTypeConfigData.LogType>();
	}
}
