﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PackAndGoPlugin.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PackAndGoPlugin.Properties.Resources", typeof(Resources).Assembly);
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
        
        /// <summary>
        ///   Looks up a localized string similar to Pack and Go.
        /// </summary>
        internal static string ApplicationName {
            get {
                return ResourceManager.GetString("ApplicationName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Source directory does not exist or could not be found:.
        /// </summary>
        internal static string ErrorTxt_SourceNotFound {
            get {
                return ResourceManager.GetString("ErrorTxt_SourceNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Create ZIP file.
        /// </summary>
        internal static string MenuEntry_CreateZip {
            get {
                return ResourceManager.GetString("MenuEntry_CreateZip", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Create ZIP file with EB GUIDE Monitor.
        /// </summary>
        internal static string MenuEntry_CreateZipWithMonitor {
            get {
                return ResourceManager.GetString("MenuEntry_CreateZipWithMonitor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pack and Go.
        /// </summary>
        internal static string MenuEntry_PackAndGo {
            get {
                return ResourceManager.GetString("MenuEntry_PackAndGo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Select save location for Pack and Go.
        /// </summary>
        internal static string SaveDialogTitle {
            get {
                return ResourceManager.GetString("SaveDialogTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Byte[].
        /// </summary>
        internal static byte[] SimulationBatFile {
            get {
                object obj = ResourceManager.GetObject("SimulationBatFile", resourceCulture);
                return ((byte[])(obj));
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Pack and go successfully created.
        /// </summary>
        internal static string SuccessMsg {
            get {
                return ResourceManager.GetString("SuccessMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to .zip.
        /// </summary>
        internal static string ZipExtension {
            get {
                return ResourceManager.GetString("ZipExtension", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ZIP File.
        /// </summary>
        internal static string ZipFile {
            get {
                return ResourceManager.GetString("ZipFile", resourceCulture);
            }
        }
    }
}
