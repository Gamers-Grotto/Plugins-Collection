#if UNITY_EDITOR

using GamersGrotto.Core.Editor;
using UnityEditor;

namespace GamersGrotto.Game_Events.Editor {
    public class GameEventTemplater : BaseTemplater {
        private const string TEMPLATE_PATHS = "Assets/Plugins/GamersGrottoToolkit/Editor/Core/GameEvents/Templates";
        private const string ASSET_PATH_SUFFIX = "Game Events/";

        [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "Game Event", priority = 30)]
        public static void CreateGameEventMenuItem() {
            var filename = "GameEventTemplate.txt";
            CreateScriptAssetFromTemplateFile(TEMPLATE_PATHS, filename);
        }
    }
}
#endif