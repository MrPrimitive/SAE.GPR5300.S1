using MSE.Engine.Interfaces;

namespace MSE.Engine.Extensions {
  public static class UiExtension {

    public static void Update(this List<IUserInterface> uis) {
      uis.ForEach(ui => ui.UpdateUi());
    }
  }
}