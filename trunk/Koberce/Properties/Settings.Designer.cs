﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Koberce.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".\\arena.db")]
        public string DbName {
            get {
                return ((string)(this["DbName"]));
            }
            set {
                this["DbName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AutoBackup {
            get {
                return ((bool)(this["AutoBackup"]));
            }
            set {
                this["AutoBackup"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute(".\\DbBackup")]
        public string BackupDirectory {
            get {
                return ((string)(this["BackupDirectory"]));
            }
            set {
                this["BackupDirectory"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ftp://ftp.drivehq.com/")]
        public string ScannerServer {
            get {
                return ((string)(this["ScannerServer"]));
            }
            set {
                this["ScannerServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://www.globalkoberce.sk/")]
        public string WebServer {
            get {
                return ((string)(this["WebServer"]));
            }
            set {
                this["WebServer"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("12")]
        public double PriceCoef {
            get {
                return ((double)(this["PriceCoef"]));
            }
            set {
                this["PriceCoef"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("pages/getproduct.html?id=XXX")]
        public string WebParam {
            get {
                return ((string)(this["WebParam"]));
            }
            set {
                this["WebParam"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("ptcomm.exe")]
        public string Ptcomm {
            get {
                return ((string)(this["Ptcomm"]));
            }
            set {
                this["Ptcomm"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("c:\\scanner\\upload")]
        public string PtcommDir {
            get {
                return ((string)(this["PtcommDir"]));
            }
            set {
                this["PtcommDir"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("/comport:1 /ptaddr:A /destdir:C:\\\\scanner\\\\Upload\\\\ /exit /upload:SK.TXT,FROMSK.T" +
            "XT,INVENTOR.TXT,SOLD.TXT")]
        public string PtcommCommand {
            get {
                return ((string)(this["PtcommCommand"]));
            }
            set {
                this["PtcommCommand"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("mizu")]
        public string FtpLogin {
            get {
                return ((string)(this["FtpLogin"]));
            }
            set {
                this["FtpLogin"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("nepoviem")]
        public string FtpPassword {
            get {
                return ((string)(this["FtpPassword"]));
            }
            set {
                this["FtpPassword"] = value;
            }
        }
    }
}
