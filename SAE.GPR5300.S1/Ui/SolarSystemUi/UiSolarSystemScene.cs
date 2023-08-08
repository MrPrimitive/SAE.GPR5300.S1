using System.Numerics;
using ImGuiNET;
using MSE.Engine.Extensions;
using MSE.Engine.Interfaces;

namespace SAE.GPR5300.S1.Ui.SolarSystemUi {
  public class UiSolarSystemScene : IUserInterface {
    private float _solarSystemMultiplier = 0f;
    private List<IUserInterface> _childUis = new();

    private uint _dockSpaceId;

    public UiSolarSystemScene() {
      _dockSpaceId = ImGui.GetID("dockspace1");
      _childUis.Add(new UiSolarSystemSetting(_dockSpaceId));
      _childUis.Add(new UiEarth(_dockSpaceId));
    }

    private bool isTab1Open = true;
    private bool isTab2Open = true;

    public void UpdateUi() {
      ImGui.SetNextWindowPos(new Vector2(0,0));
      ImGui.SetNextWindowSize(new Vector2(300,140));
      ImGui.Begin("Solar System Scene",  ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoResize);
      ImGui.DockSpace(_dockSpaceId);
      _childUis.Update();
      ImGui.End();
    }

    public void RenderUi() {
      UiController.Instance.ImGuiController.Render();
    }
  }
}