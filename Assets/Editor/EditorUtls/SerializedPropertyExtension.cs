using System;
using UnityEditor;

public static class SerializedPropertyExtension {

    public static TValue GetValue<TValue>(this SerializedProperty property,Func<TValue> _factory = null )
        where TValue : class
    {
        if (property == null)
            return null;
        var result = property.objectReferenceValue as object as TValue;
        if (result == null && _factory != null)
        {
            result = _factory();
        }
        return result;
    }

    //public static TValue SetValue<TValue>(this SerializedProperty property, 
    //    Func<TValue> _factory = null)
    //    where TValue : class
    //{
    //    var 
    //}

}
