# Portable build notes

This repository should be built from source instead of relying on the legacy Visual Studio setup projects.

Why:
- `packages/` is intentionally not committed and must be restored.
- The old `.vdproj` installers still contain machine-local inputs.
- `TXTextControl` is a separately installed prerequisite for projects that use it.

Prerequisites on a new machine:
- Visual Studio Build Tools or Visual Studio with .NET Framework desktop build support
- .NET Framework 4.5 targeting pack
- `TX Text Control 28.0 .NET for Windows Forms` installed if you need `EXON.GradedEssay` or `EXON.MONITOR`

Build everything into portable output folders:

```powershell
powershell -ExecutionPolicy Bypass -File .\scripts\Publish-Portable.ps1
```

Outputs are written to:

```text
artifacts\publish\EXON.MONITOR\
artifacts\publish\EXON.GradedEssay\
artifacts\publish\QuanLyHoiDongThiVer2\
```

Notes:
- `EXON.GradedEssay` and `EXON.MONITOR` now resolve `TXTextControl` from `Program Files` by default instead of a developer-specific absolute path.
- If `TXTextControl` is installed in a custom folder, pass `-TxTextControlInstallDir "C:\path\to\Assembly"` to the script.
- The script restores NuGet packages automatically by downloading `nuget.exe` into `tools\` when needed.
