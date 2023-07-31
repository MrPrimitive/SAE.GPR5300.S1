using Microsoft.Extensions.Configuration;
using SAE.GPR5300.S1.Settings;

namespace SAE.GPR5300.S1.Extensions {
  public static class ConfigurationExtension {
    public static ProgramConfig GetStartUpConfig(this IConfigurationBuilder builder) {
      var configuration = builder.Build();
      var startUpConfig = configuration.GetSection("startUpConfig").Get<ProgramConfig>();
      if (startUpConfig != null)
        return startUpConfig;
      throw new ApplicationException("Can not start application with no settings!");
    }

    public static IConfigurationBuilder GetConfig(this IConfigurationBuilder builder) {
      return builder.AddJsonFile("appsettings.json");
    }
  }
}