using SAE.GPR5300.S1.Core;
using Silk.NET.OpenGL.Extensions.ImGui;

namespace SAE.GPR5300.S1.Ui {
  public class UiController {
    public static UiController Instance => Lazy.Value;
    private static readonly Lazy<UiController> Lazy = new(() => new UiController());

    public ImGuiController ImGuiController { get; private set; }

    private UiController() {
      ImGuiController = new ImGuiController(
        Game.Instance.Gl, 
        Game.Instance.GameWindow, 
        Input.Instance.InputContext);
    }
  }
}