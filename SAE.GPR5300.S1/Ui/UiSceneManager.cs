using System.Numerics;
using ImGuiNET;
using MSE.Engine.Core;
using MSE.Engine.Interfaces;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Settings;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace SAE.GPR5300.S1.Ui {
  public class UiSceneManager : IUiSceneManager {
    private ImGuiController _controller;

    public static UiSceneManager Instance => Lazy.Value;
    private static readonly Lazy<UiSceneManager> Lazy = new(() => new UiSceneManager());

    public UiSceneManager() {
      _controller = UiController.Instance.ImGuiController;
    }

    public void UpdateUi() {
      ImGui.SetNextWindowPos(new Vector2(ProgramSetting.Instance.GetScreenSize.X - 400, 0));
      ImGui.Begin("SceneManager");
      ImGui.Text($"Current Scene: {SceneManager.Instance.GetActiveScene().GetSceneName()}");
      ImGui.Text($"Available Scenes: {SceneManager.Instance.Scenes.Count}");
      foreach (var scene in SceneManager.Instance.Scenes) {
        if (ImGui.Button($"Load Scene: {scene.GetSceneName()}", new Vector2(385, 50))) {
          SceneManager.Instance.LoadScene(scene);
        }
      }

      if (ImGui.Button($"CLOSE GAME", new Vector2(385, 50))) {
        Game.Instance.GameWindow.Close();
      }

      ImGui.SetWindowSize("SceneManager", new Vector2(400, ProgramSetting.Instance.GetScreenSize.Y));
      ImGui.Text("Load different Scenes form here");
      if (ImGui.Button("Switch to Fullscreen")) {
        Game.Instance.SetFullScreen();
      }
      ImGui.End();
    }

    public void RenderUi() {
      _controller.Render();
    }
  }
}