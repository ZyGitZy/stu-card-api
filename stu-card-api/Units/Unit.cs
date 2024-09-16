namespace stu_card_api.Units
{
    public static class Units
    {
        static Dictionary<string, string> mimeTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
       {".png", "image/png"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".gif", "image/gif"},
        {".webp","image/webp" },
        {".csv", "text/csv"}
    };

        public static string GetFileSuffix(string value)
        {
            return mimeTypes.FirstOrDefault(f => f.Value == value).Key;
        }

        public static string GetFilePrefixByValue(string value)
        {
            return mimeTypes.FirstOrDefault(f => value.Contains(f.Key)).Value;
        }

    }
}
