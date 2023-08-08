using ImGuiNET;
using MSE.Engine.Interfaces;

namespace SAE.GPR5300.S1.Ui {
  public class UiBlinnPhongLightingScene : IUserInterface {
    private float _exponentBlinn = 31;
    private float _exponentPhong = 8;
    private bool _isBlinnChecked;
    private bool _isGammaChecked;

    public static Action<bool> IsBlinnEvent;
    public static Action<bool> IsGammaEvent;
    public static Action<float> ExponentBlinnEvent;
    public static Action<float> ExponentPhongEvent;

    public void UpdateUi() {
      ImGui.Begin("Shader Setting", ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoMove);
      if (ImGui.Checkbox("Us Blinn Light", ref _isBlinnChecked)) {
        IsBlinnEvent.Invoke(_isBlinnChecked);
      }
      
      if (ImGui.Checkbox("Us Gamma", ref _isGammaChecked)) {
        IsGammaEvent.Invoke(_isGammaChecked);
      }

      if (ImGui.SliderFloat("Exponent Blinn", ref _exponentBlinn, 0, 100)) {
        ExponentBlinnEvent.Invoke(_exponentBlinn);
      }

      if (ImGui.SliderFloat("Exponent Phong", ref _exponentPhong, 0, 100)) {
        ExponentPhongEvent.Invoke(_exponentPhong);
      }

      ImGui.End();
    }
  }
}