; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "LibraryManagement"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "FPT"
#define MyAppURL "https://www.example.com/"
#define MyAppExeName "LibraryManagement.exe"
#define MyAppAssocName MyAppName + " File"
#define MyAppAssocExt ".myp"
#define MyAppAssocKey StringChange(MyAppAssocName, " ", "") + MyAppAssocExt

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{A0EE96FC-4558-4D81-8E5B-D72165E01C7C}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#MyAppName}
ChangesAssociations=yes
DisableProgramGroupPage=yes
; Remove the following line to run in administrative install mode (install for all users.)
PrivilegesRequired=lowest
OutputDir=C:\Users\Administrator\Desktop
OutputBaseFilename=LibraryManagement_Setup
SetupIconFile=C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\Resources\library-icon.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\{#MyAppExeName}"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\FireSharp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\FireSharp.Serialization.JsonNet.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\LibraryManagement.Controllers.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\LibraryManagement.Controllers.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\LibraryManagement.DAOs.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\LibraryManagement.DAOs.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\LibraryManagement.DTOs.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\LibraryManagement.DTOs.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\LibraryManagement.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\LibraryManagement.exe.config"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\LibraryManagement.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\LibraryManagement.Utils.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\LibraryManagement.Utils.pdb"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\Newtonsoft.Json.xml"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\RestSharp.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Administrator\Desktop\PRN292_Group2_SE1431__LibraryManagement\Source Code\LibraryManagement.Main\bin\Debug\RestSharp.xml"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Registry]
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocExt}\OpenWithProgids"; ValueType: string; ValueName: "{#MyAppAssocKey}"; ValueData: ""; Flags: uninsdeletevalue
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}"; ValueType: string; ValueName: ""; ValueData: "{#MyAppAssocName}"; Flags: uninsdeletekey
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\DefaultIcon"; ValueType: string; ValueName: ""; ValueData: "{app}\{#MyAppExeName},0"
Root: HKA; Subkey: "Software\Classes\{#MyAppAssocKey}\shell\open\command"; ValueType: string; ValueName: ""; ValueData: """{app}\{#MyAppExeName}"" ""%1"""
Root: HKA; Subkey: "Software\Classes\Applications\{#MyAppExeName}\SupportedTypes"; ValueType: string; ValueName: ".myp"; ValueData: ""

[Icons]
Name: "{autoprograms}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

