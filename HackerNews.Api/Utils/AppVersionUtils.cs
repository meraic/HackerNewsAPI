using System.Reflection;

namespace HackerNews.Api.Utils;

public static class AppVersionUtils
{
    public static string GetAssemblyVersion()
    {
        var version = Assembly.GetEntryAssembly().GetName().Version;
        const int versionFieldsCount = 3;

        return version.ToString(versionFieldsCount);
    }
}

