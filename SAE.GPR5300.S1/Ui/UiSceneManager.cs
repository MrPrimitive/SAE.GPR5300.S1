using System.Numerics;
using ImGuiNET;
using MSE.Engine.Core;
using MSE.Engine.Interfaces;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Settings;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace SAE.GPR5300.S1.Ui {
  public class UiSceneManager : IUserSceneManager {
    private ImGuiController _controller;

    public static UiSceneManager Instance => Lazy.Value;
    private static readonly Lazy<UiSceneManager> Lazy = new(() => new());

    private UiSceneManager() {
      _controller = UiController.Instance.ImGuiController;
    }

    public void UpdateUi() {
      ImGui.SetNextWindowPos(new Vector2(ProgramSetting.Instance.GetScreenSize.X - 300, 0));

      ImGui.Begin("SceneManager");
      ImGui.Text($"Current Scene: {SceneManager.Instance.GetActiveScene().GetSceneName()}");
      ImGui.Text($"Available Scenes: {SceneManager.Instance.Scenes.Count}");
      foreach (var scene in SceneManager.Instance.Scenes
                 .Where(scene => !SceneManager.Instance.GetActiveScene().GetSceneName().Equals(scene.GetSceneName()))
                 .Where(scene => ImGui.Button($"{scene.GetSceneName()}", new Vector2(285, 20)))) {
        SceneManager.Instance.LoadScene(scene);
      }

      if (ImGui.Button($"CLOSE GAME", new Vector2(285, 50))) {
        Game.Instance.GameWindow.Close();
      }

      ImGui.SetWindowSize("SceneManager", new Vector2(400, ProgramSetting.Instance.GetScreenSize.Y));
      ImGui.Text("Load different Scenes form here");
      if (ImGui.Button("Switch to Fullscreen")) {
        ProgramSetting.Instance.SetFullScreen();
      }

      ImGui.Spacing();
      ImGui.Spacing();
      ImGui.Text("Key: I");
      ImGui.TextWrapped("Switch between insert mode and cam mode");
      ImGui.Spacing();
      ImGui.Text("Key: W / A / S / D");
      ImGui.TextWrapped("Move In world space");
      ImGui.Spacing();
      ImGui.Text("Key: SHIFT");
      ImGui.TextWrapped("Increase move speed");
      
      ImGui.End();
    }

    public void RenderUi() {
      _controller.Render();
    }
  }
}