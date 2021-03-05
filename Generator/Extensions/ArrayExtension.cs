﻿using Repository.Model;

namespace Generator
{
    internal static class ArrayExtension
    {
        public static string GetMarshallAttribute(this Array? array)
        {               
            string attribute = "";
            if (array?.Length is { } length)
            {
                attribute = $"[MarshalAs(UnmanagedType.LPArray, SizeParamIndex={length})]";
            }

            return attribute;
        }
    }
}
