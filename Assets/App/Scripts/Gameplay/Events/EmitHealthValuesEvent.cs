namespace Assets.App.Scripts.Gameplay.Events
{
	public struct EmitHealthValuesEvent : IGameEvent
	{
		public float FullHealthValue;
		public float NewHealthValue;
	}
}