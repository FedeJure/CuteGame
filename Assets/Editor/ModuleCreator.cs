using UnityEditor;
using System.IO;
using UnityEngine;

namespace Editor
{
    public class ModuleCreator : MonoBehaviour
    {
        [MenuItem("Custom/Create New Module")]
        static void Init()
        {
            ModuleCreatorWindow window = ScriptableObject.CreateInstance<ModuleCreatorWindow>();
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
            window.ShowPopup();
        }
    }

    internal class ModuleCreatorWindow : EditorWindow
    {
        private string moduleName = "";
        void OnGUI()
        {
            EditorGUILayout.TextArea("Module name: ");
            moduleName = EditorGUILayout.TextField(moduleName, EditorStyles.textArea);
            
            if (GUILayout.Button("Create"))
            {
                Debug.LogWarning(moduleName);
                if (AssetDatabase.GetSubFolders($"Assets/Modules/{moduleName}").Length > 0)
                {
                    Debug.LogWarning($"Module '{moduleName}' already exists");
                    return;
                }
                CreateModule(moduleName);
                Close();
            }
            if (GUILayout.Button("Close"))
            {
                Close();
            }
        }

        private void CreateModule(string moduleName)
        {
            AssetDatabase.CreateFolder("Assets/Modules", moduleName);
            AssetDatabase.CreateFolder($"Assets/Modules/{moduleName}", "Scripts");
            AssetDatabase.CreateFolder($"Assets/Modules/{moduleName}/Scripts", "Core");
            AssetDatabase.CreateFolder($"Assets/Modules/{moduleName}/Scripts/Core", "Domain");
            AssetDatabase.CreateFolder($"Assets/Modules/{moduleName}/Scripts/Core", "Actions");
            AssetDatabase.CreateFolder($"Assets/Modules/{moduleName}/Scripts", "Infrastructure");
            AssetDatabase.CreateFolder($"Assets/Modules/{moduleName}/Scripts", "Presentation");
            AssetDatabase.CreateFolder($"Assets/Modules/{moduleName}/Scripts", "UnityDelivery");
            AssetDatabase.CreateFolder($"Assets/Modules/{moduleName}", "Tests");
            AssetDatabase.CreateFolder($"Assets/Modules/{moduleName}/Tests", "Core");
            AssetDatabase.CreateFolder($"Assets/Modules/{moduleName}/Tests", "Presentation");
            AssetDatabase.SaveAssets();

            var assemblyText = "{\"name\": \""+moduleName+"\", \"references\": [], \"includePlatforms\": [], \"excludePlatforms\": [], \"allowUnsafeCode\": false, \"overrideReferences\": false, \"precompiledReferences\": [], \"autoReferenced\": true, \"defineConstraints\": [], \"versionDefines\": [], \"noEngineReferences\": false}";
            var editorAssemblyText = "{\"name\": \""+moduleName+".Tests\", \"references\": [], \"includePlatforms\": [\"Editor\"], \"excludePlatforms\": [], \"allowUnsafeCode\": false, \"overrideReferences\": false, \"precompiledReferences\": [], \"autoReferenced\": true, \"defineConstraints\": [], \"versionDefines\": [], \"noEngineReferences\": false}";
            File.WriteAllText(Application.dataPath + $"/Modules/{moduleName}/{moduleName}.asmdef", assemblyText);
            File.WriteAllText(Application.dataPath + $"/Modules/{moduleName}/Tests/{moduleName}.Test.asmdef", editorAssemblyText);
            AssetDatabase.Refresh();
        }
    }
}
