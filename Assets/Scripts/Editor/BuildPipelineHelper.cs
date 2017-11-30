using System.IO;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
    public class BuildPipelineHelper
    {
        protected static string BUILD_FOLDER = "Builds/LootBoxQuest"; //will be cleaned

        [MenuItem("Build/Build for Submission")]
        public static void RunBuilds()
        {
            string buildType = "Build for Submission";

            Debug.LogFormat("{0}: started {1}", buildType, System.DateTime.Now.ToLocalTime());

            FileUtil.DeleteFileOrDirectory(BUILD_FOLDER);

            var levels = new[] { "Assets/Scenes/main.unity" };

            BuildPipeline.BuildPlayer(levels, BUILD_FOLDER + "/lootquest.exe", BuildTarget.StandaloneWindows, BuildOptions.None);

            //TODO: zip, etc
            Directory.CreateDirectory(BUILD_FOLDER + "/licenses");

            foreach (var file in Directory.GetFiles("Assets/licenses", "*.txt"))
            {
                string filename = Path.GetFileName(file); ;
                FileUtil.CopyFileOrDirectory("Assets/licenses/" + filename, BUILD_FOLDER + "/licenses/" + filename);
            }

            Debug.LogFormat("{0}: completed {1}", buildType, System.DateTime.Now.ToLocalTime());
        }
    }
}
