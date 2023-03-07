namespace PlatformService.Data;

public class ConnectionService
{
    public static string connstring = "";
    public static void Set(IConfiguration config)
    {
        connstring = config.GetConnectionString("PlatformsConn");
    }
}
