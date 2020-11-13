using System.Reflection;

namespace GamemodeManager
{
    public static class ReflectionHelpers
    {
        private const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                                           | BindingFlags.Static | BindingFlags.InvokeMethod;
        
        public static object GetInstanceField(this object obj, string fieldName)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, flags);
            return field?.GetValue(obj);
        }
        
        public static void SetInstanceField(this object obj, string fieldName, object value)
        {
            FieldInfo field = obj.GetType().GetField(fieldName, flags);
            field?.SetValue(obj, value);
        }
        
        public static void InvokeInstanceMethod(this object obj, string methodName, object[] param)
        {
            MethodInfo info = obj.GetType().GetMethod(methodName, flags);
            info?.Invoke(obj, param);
        }
    }
}