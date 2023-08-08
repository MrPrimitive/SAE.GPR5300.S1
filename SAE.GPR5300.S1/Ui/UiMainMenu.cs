using System.Numerics;
using ImGuiNET;
using MSE.Engine.Core;
using MSE.Engine.Interfaces;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Settings;

namespace SAE.GPR5300.S1.Ui {
  public class UiMainMenu : IUserSceneManager {
    public UiMainMenu() {
    }

    public void UpdateUi() {
      ImGui.SetNextWindowPos(new Vector2(
        ProgramSetting.Instance.GetScreenSize.X / 2 - 200,
        ProgramSetting.Instance.GetScreenSize.Y / 2 - 200));
      ImGui.SetNextWindowSize(new Vector2(400, 400));
      ImGui.Begin("Mein Menu", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar);
      foreach (var scene in SceneManager.Instance.Scenes.Where(scene => !scene.GetSceneName().Equals(SceneName.MainMenu))) {
        if (ImGui.Button($"{scene.GetSceneName()}", new Vector2(385, 30)))
          SceneManager.Instance.LoadScene(scene);
        ImGui.TextWrapped($"{scene.GetDescription()}");
      }

      ImGui.Spacing();
      ImGui.Spacing();
      ImGui.TextWrapped(
        "There is an issue if you load the \"Reflection - Scene\" after the \"World Map - Scene\" the scene will be black. Could not solve the problem very strange behavior.");

      ImGui.End();
    }
  }
}