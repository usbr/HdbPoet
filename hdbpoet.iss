#define AppName "HDB-POET"
#define bin "bin\poet\"
#define SrcApp ".\bin\poet\HDB-POET.exe"
#define FileVerStr GetFileVersion(SrcApp)
#define public StripBuild(str aVerStr) Copy(aVerStr, 1, RPos(".", aVerStr)-1)
#define AppVerStr StripBuild(FileVerStr)
[Setup]
AppId={{16D58642-80F4-4DB0-ABC4-3E40193CF82A}
AppName={#AppName}
AppVersion={#AppVerStr}
AppVerName={#AppName} {#AppVerStr}
AppPublisher=Reclamation
DefaultDirName={sd}\HDB-POET
DefaultGroupName=HDB-POET
OutputBaseFilename=setup_hdbpoet
Compression=lzma
SolidCompression=yes
VersionInfoVersion={#FileVerStr}
VersionInfoTextVersion={#AppVerStr}
VersionInfoProductName="HDB-POET"
VersionInfoCompany="Reclamation"
;TouchDate=2009-03-11
PrivilegesRequired=none
LicenseFile=.\license.txt

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked


[Files]                
Source: "{#bin}Aga.Controls.dll"; DestDir: "{app}"; Flags: ignoreversion   
Source: "{#bin}alglibnet2.dll"; DestDir: "{app}"; Flags: ignoreversion    
Source: "{#bin}BumpKit.dll"; DestDir: "{app}"; Flags: ignoreversion    
Source: "{#bin}Devart.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}Devart.Data.Universal.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}Devart.Data.Universal.Oracle.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}Devart.Data.Universal.OracleClient.dll"; DestDir: "{app}"; Flags: ignoreversion  
Source: "{#bin}DgvFilterPopup.dll"; DestDir: "{app}"; Flags: ignoreversion            
Source: "{#bin}Excel.dll"; DestDir: "{app}"; Flags: ignoreversion                 
Source: "{#bin}ICSharpCode.SharpZipLib.dll"; DestDir: "{app}"; Flags: ignoreversion      
Source: "{#bin}MathNet.Numerics.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}MySql.Data.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}Newtonsoft.Json.dll"; DestDir: "{app}"; Flags: ignoreversion          
Source: "{#bin}Npgsql.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}NPOI.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}NPOI.OOXML.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}NPOI.OpenXml4Net.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}NPOI.OpenXmlFormats.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}nunit.framework.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}Oracle.ManagedDataAccess.dll"; DestDir: "{app}"; Flags: ignoreversion  
Source: "{#bin}Reclamation.Core.dll"; DestDir: "{app}"; Flags: ignoreversion    
Source: "{#bin}Reclamation.TimeSeries.dll"; DestDir: "{app}"; Flags: ignoreversion    
Source: "{#bin}Reclamation.TimeSeries.Forms.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}RestSharp.dll"; DestDir: "{app}"; Flags: ignoreversion        
Source: "{#bin}System.Data.SQLite.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}System.Threading.Tasks.Extensions.dll"; DestDir: "{app}"; Flags: ignoreversion    
Source: "{#bin}TeeChart.dll"; DestDir: "{app}"; Flags: ignoreversion                                   
Source: "{#bin}ZedGraph.dll"; DestDir: "{app}"; Flags: ignoreversion                                   
Source: "{#bin}x64\SQLite.Interop.dll"; DestDir: "{app}\x64"; Flags: ignoreversion                        
Source: "{#bin}x86\SQLite.Interop.dll"; DestDir: "{app}\x86"; Flags: ignoreversion      

Source: "{#bin}HDB-POET.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}HDB-POET.exe.config"; DestDir: "{app}"; Flags: ignoreversion 
Source: "{#bin}report.xls"; DestDir: "{app}"; Flags: ignoreversion
Source: "{#bin}sqlFunctions.xml"; DestDir: "{app}"; Flags: ignoreversion       
;Source: "*.*"; Excludes: "*.zip,*.sln,*.pdf,*.hdb"; DestDir: "{app}\src";
;Source: "Acl\*.*"; Excludes: "*.zip,*.sln"; DestDir: "{app}\src";

;Source: "ThirdParty\dotnetfx35setup.exe"; DestDir: "{app}\tmp"; Check: not HaveDotNet35(); Flags: ignoreversion
;Source: "GettingStartedwithHDB-POET.pdf"; DestDir: "{app}"; Flags: ignoreversion


; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\HDB-POET"; Filename: "{app}\HDB-POET.exe"
Name: "{commondesktop}\HDB-POET"; Filename: "{app}\HDB-POET.exe"; Tasks: desktopicon

[Run]
;Filename: "{app}\tmp\dotnetfx35setup.exe";    Check: not HaveDotNet35()
;Filename: "{app}\tmp\unzip.exe"; Parameters: """{app}\tmp\ODAC1110621Xcopy.zip"" -d ""{app}\tmp\oracle"""; Check: not HaveOdp()
;Filename: "{app}\tmp\oracle\install.bat"; Parameters:"odp.net20 ..\..\oracle odac";  Check: not HaveOdp()
Filename: "{app}\HDB-POET.exe"; Description: "{cm:LaunchProgram,HDB-POET}"; Flags: nowait postinstall skipifsilent

[Code]

function HaveDotNet45: boolean;
var ResultDWord:Cardinal;
begin
result:=RegQueryDWordValue(HKEY_LOCAL_MACHINE, 'Software\Microsoft\NET Framework Setup\NDP\v4\Full', 'Release', ResultDWord);
if ResultDWord = 1 then
begin
result:= True;
end;
end;

function InitializeSetup(): Boolean;

begin
if not HaveDotNet45 then
  begin
 MsgBox('WARNING: You do not have the Microsoft .NET Framework 4.5 installed.  HDB-POET requires the .NET Framework 4.5 ', mbInformation, MB_OK);
 end ;
 result:= true;
 end;


function HaveOdp: boolean  ;
begin
result:= DirExists( ExpandConstant('{app}\oracle'));

end;
