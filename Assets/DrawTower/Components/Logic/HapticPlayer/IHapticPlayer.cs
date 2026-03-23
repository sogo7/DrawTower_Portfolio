namespace DrawTower.Logic
{
	public interface IHapticPlayer
	{
		void Play();
		void SetEnabled(bool enabled);
		bool IsEnabled();
	}
}

