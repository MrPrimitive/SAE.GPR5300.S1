using ImGuiNET;
using MSE.Engine.Interfaces;
using SAE.GPR5300.S1.Assets.Scenes;

namespace SAE.GPR5300.S1.Ui {
  public class UiSolarSystemSetting : IUiInterface {
    private float _solarSystemMultiplier = 1f;

    public static Action<float> SolarSystemMultiplierEvent;

    public UiSolarSystemSetting() {
    }

    public void UpdateUi() {
      ImGui.Begin("Solar System Settings");
      if (ImGui.SliderFloat("Speed Multiplier", ref _solarSystemMultiplier, 0.0f, 5.0f)) {
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