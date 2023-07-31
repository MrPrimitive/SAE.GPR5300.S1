using System.Numerics;
using ImGuiNET;
using MSE.Engine.Core;
using MSE.Engine.Interfaces;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Settings;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace SAE.GPR5300.S1.Ui {
  public class UiMainMenu : IUiSceneManager {
    private ImGuiController _controller;

    public static UiMainMenu Instance => Lazy.Value;
    private static readonly Lazy<UiMainMenu> Lazy = new(() => new UiMainMenu());

    public UiMainMenu() {
      _controller = UiController.Instance.ImGuiController;
    }

    public void UpdateUi() {
      ImGui.SetNextWindowPos(new Vector2(
        ProgramSetting.Instance.GetScreenSize.X / 2 - 200,
        ProgramSetting.Instance.GetScreenSize.Y / 2 - 150));
      ImGui.SetNextWindowSize(new Vector2(400, 300));
      ImGui.Begin("Mein Menu", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoTitleBar);
      ImGui.Text($"Available Scenes: {SceneManager.Instance.Scenes.Count}");
      foreach (var scene in SceneManager.Instance.Scenes
                 .Where(scene => !scene.GetSceneName().Equals(SceneName.MainMenu))
                 .Where(scene => !SceneManager.Instance.GetActiveScene().GetSceneName().Equals(scene.GetSceneName()))
                 .Where(scene => ImGui.Button($"{scene.GetSceneName()}", new Vector2(385, 50)))) {
        SceneManager.Instance.LoadScene(scene);
      }

      ImGui.End();
    }

    public void RenderUi() {
      _controller.Render();
    }
  }
}