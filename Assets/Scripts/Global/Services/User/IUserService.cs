using Better.Saves.Runtime.Data;

namespace EndlessHeresy.Global.Services.User
{
    public interface IUserService
    {
        public SavesProperty<int> LastLevelIndex { get; }
    }
}