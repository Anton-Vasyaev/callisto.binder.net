
@set solDir=%1
@set outputDir=%2

@echo hello
@echo %solDir%

@set lib32Path=%solDir%native.example.lib\build_result\Windows\x32\Release\dll\nativeexample_x32.dll
@set lib64Path=%solDir%native.example.lib\build_result\Windows\x64\Release\dll\nativeexample_x64.dll

@echo %lib64Path%
@echo %outputDir%

del %outputDir%nativexample_x32.dll 2>nul
del %outputDir%nativexample_x64.dll 2>nul


@xcopy /s %lib32Path% %outputDir% 
@xcopy /s %lib64Path% %outputDir% 
