using ImGuiNET;
using MSE.Engine.Interfaces;
using SAE.GPR5300.S1.Assets.Scenes;

namespace SAE.GPR5300.S1.Ui {
  public class UiSolarSystemSetting : IUiInterface {
    private float _solarSystemMultiplier = 1f;
    private SolarSystemScene _solarSystemScene;

    public static Action<float> SolarSystemMultiplierEvent;

    public UiSolarSystemSetting(SolarSystemScene solarSystemScene) {
      _solarSystemScene = solarSystemScene;
    }

    public void UpdateUi() {
      ImGui.Begin("Solar System Settings");
      if (ImGui.SliderFloat("Speed Multpliaktor", ref _solarSystemMultiplier, 0.0f, 1.0f)) {
        SolarSystemMultiplierEvent.Invoke(_solarSystemMultiplier);
      }

      if (ImGui.Button("Reset")) {
        _solarSystemMultiplier = 1f;
        SolarSystemMultiplierEvent.Invoke(_solarSystemMultiplier);
      }

      ImGui.End();
    }

    public void RenderUi() {
      UiController.Instance.ImGuiController.Render();
    }
  }
}