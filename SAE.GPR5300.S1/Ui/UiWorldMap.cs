using System.Numerics;
using ImGuiNET;
using MSE.Engine.Interfaces;

namespace SAE.GPR5300.S1.Ui {
  public class UiWorldMap : IUserInterface {
    public static Action? UseNormalMapEvent;

    public UiWorldMap() {
    }

    public void UpdateUi() {
      ImGui.SetNextWindowPos(new Vector2(0, 0));
      ImGui.SetNextWindowSize(new Vector2(300, 60));
      ImGui.Begin("World Map Setting");
      if (ImGui.Button("Use Normal Map", new Vector2(285, 20))) {
        UseNormalMapEvent?.Invoke();
      }

      ImGui.End();
    }
  }
}