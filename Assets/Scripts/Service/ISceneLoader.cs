using System;

namespace Service
{
	public interface ISceneLoader : IService
	{
		public void Load(string name, Action callback);
	}
}