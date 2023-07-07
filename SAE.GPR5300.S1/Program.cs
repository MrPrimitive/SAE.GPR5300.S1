using Microsoft.Extensions.Configuration;
using SAE.GPR5300.S1.Core;
using SAE.GPR5300.S1.Settings;

namespace SAE.GPR5300.S1;

public static class Program {
  private static bool _validStartUp;

  public static void Main() {
    var startUpConfig = GetConfig();
    Game.Instance
      .SetConfig(startUpConfig)
      .Run();
  }

  private static StartUpConfig GetConfig() {
    var builder = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json");
    var configuration = builder.Build();
    return configuration.GetSection("startUpConfig").Get<StartUpConfig>();
  }
}