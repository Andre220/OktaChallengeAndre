using UnityEditor;

public class CreateCollisionScriptTemplate
{
    private const string path = "Assets/Scripts/CollisionHandlerTemplate.txt";

    [MenuItem(itemName: "Assets/Create/CollisionHandler", isValidateFunction: false, priority: 51)]
    public static void CreateScriptFromTemplate()
    {
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(path, "CollisionHandlerTemplate.cs");
    }
}
