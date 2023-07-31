using ImGuiNET;
using MSE.Engine.Interfaces;

namespace SAE.GPR5300.S1.Ui {
  public class UiBlinnPhongLightingScene : IUiInterface {
    private float _exponentBlinn = 31;
    private float _exponentPhong = 8;
    private bool _isBlinnChecked;
    private bool _isGammaChecked;

    public static Action<bool> LightingTechEvent;
    public static Action<bool> IsGammaEvent;
    public static Action<float> ExponentBlinnEvent;
    public static Action<float> ExponentPhongEvent;

    public void UpdateUi() {
      ImGui.Begin("Shader Setting", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove);
      if (ImGui.Checkbox("Blinn-Phong Light", ref _isBlinnChecked)) {
        LightingTechEvent.Invoke(_isBlinnChecked);
      }
      
      if (ImGui.Checkbox("Gamma", ref _isGammaChecked)) {
        IsGammaEvent.Invoke(_isGammaChecked);
      }

      if (ImGui.SliderFloat("ExponentBlinnEvent", ref _exponentBlinn, 0, 100)) {
        ExponentBlinnEvent.Invoke(_exponentBlinn);
      }

      if (ImGui.SliderFloat("ExponentPhongEvent", ref _exponentPhong, 0, 100)) {
        ExponentPhongEvent.Invoke(_exponentPhong);
      }

      ImGui.End();
    }

    public void RenderUi() {
      UiController.Instance.ImGuiController.Render();
    }
  }
}