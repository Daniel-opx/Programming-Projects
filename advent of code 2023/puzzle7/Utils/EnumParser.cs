namespace Utils
{
    public static class EnumParser
    {
        public static T Parse<T>(string value) where T: Enum
        {
            return (T)Enum.Parse(typeof(T), value);
        }
    }
}
