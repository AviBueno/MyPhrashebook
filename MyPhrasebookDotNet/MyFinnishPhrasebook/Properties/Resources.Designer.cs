﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyFinnishPhrasebookNamespace.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MyFinnishPhrasebookNamespace.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static System.Drawing.Icon AppIcon {
            get {
                object obj = ResourceManager.GetObject("AppIcon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap Contains {
            get {
                object obj = ResourceManager.GetObject("Contains", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap EndsWith {
            get {
                object obj = ResourceManager.GetObject("EndsWith", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        internal static System.Drawing.Bitmap ExactMatch {
            get {
                object obj = ResourceManager.GetObject("ExactMatch", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (_english like &apos;%{0}%&apos;) OR (_language like &apos;%{0}%&apos;).
        /// </summary>
        internal static string QFContains {
            get {
                return ResourceManager.GetString("QFContains", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (_english like &apos;%{0}&apos;) OR (_language like &apos;%{0}&apos;).
        /// </summary>
        internal static string QFEndsWith {
            get {
                return ResourceManager.GetString("QFEndsWith", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (_english like &apos;{0}&apos;) OR (_language like &apos;{0}&apos;).
        /// </summary>
        internal static string QFExactMatch {
            get {
                return ResourceManager.GetString("QFExactMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (_english like &apos;{0}%&apos;) OR (_language like &apos;{0}%&apos;).
        /// </summary>
        internal static string QFStartsWith {
            get {
                return ResourceManager.GetString("QFStartsWith", resourceCulture);
            }
        }
        
        internal static System.Drawing.Bitmap StartsWith {
            get {
                object obj = ResourceManager.GetObject("StartsWith", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
