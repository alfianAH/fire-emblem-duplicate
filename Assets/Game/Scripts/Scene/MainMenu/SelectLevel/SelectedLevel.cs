using FireEmblemDuplicate.Core.Singleton;

namespace FireEmblemDuplicate.Scene.MainMenu.SelectLevel
{
    public class SelectedLevel : Singleton<SelectedLevel>
    {
        public string LevelName { get; private set; }

        public void SetLevelName(string levelName)
        {
            LevelName = levelName;
        }
    }
}
