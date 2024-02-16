namespace DB
{
    public static class Constants
    {
#if DEBUG
        public const string SERVER_TYPE = "Local";
#elif RELEASE
        public const string SERVER_TYPE = "Local";
#elif AZURE
        public const string SERVER_TYPE = "Azure";
#endif
    }

    public enum Role
    {
        normal = 0,
        translate = 1,
    }
}
