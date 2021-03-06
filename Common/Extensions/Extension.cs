namespace Common.Extensions
{
    public static class Extension
    {
        public static void SetPropertyValue(this object obj, string propName, object value)
        {
            obj.GetType().GetProperty(propName)?.SetValue(obj, value, null);
        }

        public static object GetPropertyValue(this object obj, string propName)
        {
            return obj.GetType().GetProperty(propName).GetValue(obj, null);
        }
    }
}
