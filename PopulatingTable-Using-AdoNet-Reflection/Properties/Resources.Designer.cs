﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PopulatingTable_Using_AdoNet_Reflection.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("PopulatingTable_Using_AdoNet_Reflection.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to data source = .\SQLEXPRESS; initial catalog = cardb2; integrated security = SSPI;.
        /// </summary>
        internal static string ConnectionStringHome {
            get {
                return ResourceManager.GetString("ConnectionStringHome", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to data source = STHQ0125-18; initial catalog = cardb2; User Id= &apos;admin&apos;; Password= &apos;admin&apos;;.
        /// </summary>
        internal static string ConnectionStringStep {
            get {
                return ResourceManager.GetString("ConnectionStringStep", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to data source = .\SQLEXPRESS; integrated security = SSPI;.
        /// </summary>
        internal static string MasterConnectionString {
            get {
                return ResourceManager.GetString("MasterConnectionString", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = &apos;cardb2&apos;)
        ///	BEGIN
        ///	CREATE DATABASE cardb;
        ///	END
        ///	USE cardb;
        ///IF NOT EXISTS (SELECT * FROM sysobjects WHERE name=&apos;cars&apos; and xtype=&apos;U&apos;)
        ///BEGIN
        ///    CREATE TABLE cars(id INT NOT NULL IDENTITY(1,1) PRIMARY KEY, Vendor NVARCHAR(255), Model NVARCHAR(255), Engine DECIMAL(2,1), Year INT);
        ///END
        ///IF NOT EXISTS(SELECT * FROM cars)
        ///BEGIN
        ///insert into cars (Vendor, Model, Year, Engine) values (&apos;Land Rover&apos;, &apos;Range Rover&apos;, 2015, 1.4);
        ///insert into cars (Vendor, Mo [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SchemaQuery {
            get {
                return ResourceManager.GetString("SchemaQuery", resourceCulture);
            }
        }
    }
}
