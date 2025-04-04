// Unity C# reference source
// Copyright (c) Unity Technologies. For terms of use, see
// https://unity3d.com/legal/licenses/Unity_Reference_Only_License

using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Connect;
using UnityEditor.PackageManager.UI;
using UnityEditor.UIElements;

namespace UnityEditor
{
    [EditorWindowTitle(title = "Asset Store", icon = "Asset Store")]
    internal class AssetStoreWindow : EditorWindow
    {
        public static AssetStoreWindow Init()
        {
            if (EditorPrefs.GetBool("AlwaysOpenAssetStoreInBrowser", false))
            {
                OpenAssetStoreInBrowser();
                return null;
            }
            else
            {
                AssetStoreWindow window = GetWindow<AssetStoreWindow>(typeof(SceneView));
                window.SetMinMaxSizes();
                window.Show();
                return window;
            }
        }

        [MenuItem("Window/Package Management/Asset Store", false, 2000)]
        public static void OpenAssetStoreInBrowser()
        {
            string assetStoreUrl = UnityConnect.instance.GetConfigurationURL(CloudConfigUrl.CloudAssetStoreUrl);
            assetStoreUrl += "?utm_source=unity-editor-window-menu&utm_medium=desktop-app";
            if (UnityConnect.instance.loggedIn)
                UnityConnect.instance.OpenAuthorizedURLInWebBrowser(assetStoreUrl);
            else Application.OpenURL(assetStoreUrl);
        }

        [MenuItem("Window/Package Management/My Assets", false, 1501)]
        public static void OpenMyAssetsInPackageManager()
        {
            PackageManagerWindow.OpenAndSelectPage(PackageManager.UI.Internal.MyAssetsPage.k_Id);
        }

        public void OnEnable()
        {
            this.antiAliasing = 4;
            titleContent = GetLocalizedTitleContent();
            var windowResource = EditorGUIUtility.Load("UXML/AssetStore/AssetStoreWindow.uxml") as VisualTreeAsset;
            if (windowResource != null)
            {
                var root = windowResource.CloneTree();

                var lightStyleSheet = EditorGUIUtility.Load(UIElementsEditorUtility.s_DefaultCommonLightStyleSheetPath) as StyleSheet;
                var assetStoreStyleSheet = EditorGUIUtility.Load("StyleSheets/AssetStore/AssetStoreWindow.uss") as StyleSheet;
                var styleSheet = CreateInstance<StyleSheet>();
                styleSheet.isDefaultStyleSheet = true;

                var resolver = new StyleSheets.StyleSheetResolver();
                resolver.AddStyleSheets(lightStyleSheet, assetStoreStyleSheet);
                resolver.ResolveTo(styleSheet);
                root.styleSheets.Add(styleSheet);

                rootVisualElement.Add(root);
                root.StretchToParentSize();

                visitWebsiteButton.clickable.clicked += OnVisitWebsiteButtonClicked;
                launchPackageManagerButton.clickable.clicked += OnLaunchPackageManagerButtonClicked;

                alwaysOpenInBrowserToggle.SetValueWithoutNotify(EditorPrefs.GetBool("AlwaysOpenAssetStoreInBrowser", false));

                alwaysOpenInBrowserToggle.RegisterValueChangedCallback(changeEvent =>
                {
                    EditorPrefs.SetBool("AlwaysOpenAssetStoreInBrowser", changeEvent.newValue);
                });
            }
        }

        public void OnDisable()
        {
            visitWebsiteButton.clickable.clicked -= OnVisitWebsiteButtonClicked;
            launchPackageManagerButton.clickable.clicked -= OnLaunchPackageManagerButtonClicked;
        }

        public static void OpenURL(string url)
        {
            string assetStoreUrl = $"{UnityConnect.instance.GetConfigurationURL(CloudConfigUrl.CloudAssetStoreUrl)}/packages/{url}";
            if (UnityEditor.Connect.UnityConnect.instance.loggedIn)
                UnityEditor.Connect.UnityConnect.instance.OpenAuthorizedURLInWebBrowser(assetStoreUrl);
            else Application.OpenURL(assetStoreUrl);
        }

        private void OnVisitWebsiteButtonClicked()
        {
            OpenAssetStoreInBrowser();
        }

        private void OnLaunchPackageManagerButtonClicked()
        {
            PackageManagerWindow.OpenAndSelectPackage(null);
        }

        private void SetMinMaxSizes()
        {
            this.minSize = new Vector2(455, 354);
            this.maxSize = new Vector2(4000, 4000);
        }

        private Button visitWebsiteButton { get { return rootVisualElement.Q<Button>("visitWebsiteButton"); } }
        private Button launchPackageManagerButton { get { return rootVisualElement.Q<Button>("launchPackageManagerButton"); } }
        private Toggle alwaysOpenInBrowserToggle { get { return rootVisualElement.Q<Toggle>("alwaysOpenInBrowserToggle"); } }
    }
}
