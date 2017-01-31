!include SEOMacroscope-Version.nsh

OutFile "SEOMacroscope-Installer-${VERSION}.exe"

SetCompressor /SOLID lzma 
Unicode true

LicenseData ..\LICENSE

Name "SEO Macroscope ${VERSION}"

InstallDir $DESKTOP\QA
#InstallDir $PROGRAMFILES

RequestExecutionLevel user

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

#	Delete $INSTDIR\$SEOMacroscopeExe.exe

#	Delete $INSTDIR\BouncyCastle.Crypto.dll
#	Delete $INSTDIR\SEOMacroscopeWPF.exe.config
#	Delete $INSTDIR\MailKit.dll
#	Delete $INSTDIR\MailKit.xml
#	Delete $INSTDIR\MimeKit.dll
#	Delete $INSTDIR\MimeKit.xml

#	Delete $INSTDIR\$SEOMacroscopeUninstallExe.exe

#	RMDir $INSTDIR

	Delete $SMPROGRAMS\$SEOMacroscopeDir\$SEOMacroscopeName.lnk
	Delete $SMPROGRAMS\$SEOMacroscopeDir\$SEOMacroscopeUninstallName.lnk

#	RMDir $SMPROGRAMS\$SEOMacroscopeDir

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
