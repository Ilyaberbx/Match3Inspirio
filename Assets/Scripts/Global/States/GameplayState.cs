namespace EndlessHeresy.Global.States
{
    public sealed class GameplayState : BaseLoadingState
    {
        protected override string GetSceneName() => SceneConstants.Gameplay;
    }
}