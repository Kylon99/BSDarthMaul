using CustomUI.GameplaySettings;
using CustomUI.Utilities;

namespace BSDarthMaul
{
    public class DarthMaulUI
    {
        public static void CreateUI()
        {
            var darthMaulIcon = UIUtilities.LoadSpriteFromResources("BSDarthMaul.BSDarthMaul.Resources.DarthMaul.png");

            var darthMaulMenu = GameplaySettingsUI.CreateSubmenuOption(GameplaySettingsPanels.ModifiersRight, "Darth Maul Settings", "MainMenu", "DarthMaulMenu1", "Darth Maul Plugin Options", darthMaulIcon);

            var darthMaulToggle = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersRight, "Darth Maul", "DarthMaulMenu1", "Enable Darth Maul Mode", darthMaulIcon);
            darthMaulToggle.GetValue = Plugin.IsDarthModeOn;
            darthMaulToggle.OnToggle += (value) => { Plugin.IsDarthModeOn = value; };

            var oneHandedToggle = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersRight, "One Handed", "DarthMaulMenu1", "One Handed Darth Maul");
            oneHandedToggle.GetValue = Plugin.IsOneHanded;
            oneHandedToggle.OnToggle += (value) => { Plugin.IsOneHanded = value; };

            var autoDetectToggle = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersRight, "Auto Detect", "DarthMaulMenu1", "Auto Detect seperation/attachment of sabers. No Fail Only");
            autoDetectToggle.GetValue = Plugin.IsAutoDetect;
            autoDetectToggle.OnToggle += (value) => { Plugin.IsAutoDetect = value; };

            var leftHandToggle = GameplaySettingsUI.CreateToggleOption(GameplaySettingsPanels.ModifiersRight, "Left Main Controller", "DarthMaulMenu1", "Whether the left controller is the main controller.");
            leftHandToggle.GetValue = Plugin.MainController == Plugin.ControllerType.LEFT ? true : false; ;
            leftHandToggle.OnToggle += (value) => { Plugin.MainController = value == true ? Plugin.ControllerType.LEFT : Plugin.ControllerType.RIGHT; };


        }






    }
}
