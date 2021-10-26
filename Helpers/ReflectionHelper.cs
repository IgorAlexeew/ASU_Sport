namespace ASUSport.Helpers
{
    public static class ReflectionHelper
    {
        public static object GetValue(object obj, string property)
        {
            return obj.GetType().GetProperty(property).GetValue(obj);
        }

        public static object InvokeFunction(object obj, string method, object?[]? parametres)
        {
            return obj.GetType().GetMethod(method).Invoke(obj, parametres);
        }
    }
}
