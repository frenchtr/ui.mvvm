using System;
using UnityEngine;

namespace TravisRFrench.UI.MVVM.Samples.Common
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class FlattenAttribute : PropertyAttribute
    {
        /// <summary>
        /// Child property path relative to the annotated property.
        /// Defaults to "Value".
        /// Examples: "Value", "m_Value", "Data.Value".
        /// </summary>
        public string RelativePath { get; }

        /// <summary>
        /// Optional label override.
        /// </summary>
        public string LabelOverride { get; }

        public FlattenAttribute(string relativePath = "Value", string labelOverride = null)
        {
            this.RelativePath = relativePath;
            this.LabelOverride = labelOverride;
        }
    }
}
