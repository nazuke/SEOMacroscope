!include "..\version.nsh"
#!include "MUI2.nsh"
	
OutFile "SEOMacroscope-Installer-${VERSION}.exe"

SetCompressor /SOLID lzma 
Unicode true

LicenseData ..\LICENSE

Name "SEO Macroscope ${VERSION}"

#InstallDir $DESKTOP\QA
InstallDir $PROGRAMFILES

RequestExecutionLevel none
#RequestExecutionLevel user
#RequestExecutionLevel highest
#RequestExecutionLevel admin

# ICONS AND SPLASH SCREENS --------------------------------------------------- #

#!define MUI_ICON "..\BlenderProjects\MacroscopeIcon-64x64.ico"
#!define MUI_HEADERIMAGE
#!define MUI_HEADERIMAGE_BITMAP "path\to\InstallerLogo.bmp"
#!define MUI_HEADERIMAGE_RIGHT

# VARIABLES ------------------------------------------------------------------ #

Var SEOMacroscopeDir
Var SEOMacroscopeExe
Var SEOMacroscopeName
Var SEOMacroscopeUninstallExe
Var SEOMacroscopeUninstallName

# PAGES ---------------------------------------------------------------------- #

Page license
#Page components
Page directory
Page instfiles
UninstPage uninstConfirm
UninstPage instfiles

# SECTION: INSTALL ----------------------------------------------------------- #

Section

	Call setVariables

	SetOutPath $INSTDIR\$SEOMacroscopeDir

	File ..\\bin\Release\*.*

	WriteUninstaller $INSTDIR\$SEOMacroscopeDir\$SEOMacroscopeUninstallExe.exe

	CreateDirectory $SMPROGRAMS\$SEOMacroscopeDir

	CreateShortCut $SMPROGRAMS\$SEOMacroscopeDir\$SEOMacroscopeName.lnk $INSTDIR\$SEOMacroscopeDir\$SEOMacroscopeExe.exe
	CreateShortCut $SMPROGRAMS\$SEOMacroscopeDir\$SEOMacroscopeUninstallName.lnk $INSTDIR\$SEOMacroscopeDir\$SEOMacroscopeUninstallExe.exe

SectionEnd

# SECTION: UNINSTALL --------------------------------------------------------- #

Section uninstall

	Call un.setVariables

	###	BEGIN: Program Files

	Delete $INSTDIR\LICENSE

	Delete $INSTDIR\PdfSharp.Charting.resources.dll
	Delete $INSTDIR\PdfSharp.resources.dll
	RMDir $INSTDIR\de

	Delete $INSTDIR\ClosedXML.dll
	Delete $INSTDIR\ClosedXML.pdb
		
	Delete $INSTDIR\DocumentFormat.OpenXml.dll
	
	Delete $INSTDIR\ExCSS.dll

	Delete $INSTDIR\HtmlAgilityPack.dll
	Delete $INSTDIR\HtmlAgilityPack.pdb
	Delete $INSTDIR\HtmlAgilityPack.xml

	Delete $INSTDIR\NUnit.Framework.dll

	Delete $INSTDIR\PdfSharp.Charting.dll
	Delete $INSTDIR\PdfSharp.Charting.xml
	Delete $INSTDIR\PdfSharp.dll
	Delete $INSTDIR\PdfSharp.xml

	Delete $INSTDIR\RobotsTxt.dll
	Delete $INSTDIR\RobotsTxt.xml
	
	Delete $INSTDIR\$SEOMacroscopeExe.exe
	Delete $INSTDIR\$SEOMacroscopeExe.exe.config

	Delete $INSTDIR\$SEOMacroscopeUninstallExe.exe

	RMDir $INSTDIR

	###	END: Program Files

	###	BEGIN: Start Menu Shortcuts

	Delete $SMPROGRAMS\$SEOMacroscopeDir\$SEOMacroscopeName.lnk
	Delete $SMPROGRAMS\$SEOMacroscopeDir\$SEOMacroscopeUninstallName.lnk
	RMDir $SMPROGRAMS\$SEOMacroscopeDir

	###	END: Start Menu Shortcuts

SectionEnd

# ---------------------------------------------------------------------------- #

Function setVariables
	StrCpy $SEOMacroscopeDir "SEOMacroscope"
	StrCpy $SEOMacroscopeExe "SEOMacroscope"
	StrCpy $SEOMacroscopeName "SEO Macroscope"
	StrCpy $SEOMacroscopeUninstallExe "Uninstall-SEOMacroscope"
	StrCpy $SEOMacroscopeUninstallName "Uninstall SEOMacroscope"
FunctionEnd

Function un.setVariables
	StrCpy $SEOMacroscopeDir "SEOMacroscope"
	StrCpy $SEOMacroscopeExe "SEOMacroscope"
	StrCpy $SEOMacroscopeName "SEO Macroscope"
	StrCpy $SEOMacroscopeUninstallExe "Uninstall-SEOMacroscope"
	StrCpy $SEOMacroscopeUninstallName "Uninstall SEOMacroscope"
FunctionEnd

# ---------------------------------------------------------------------------- #
