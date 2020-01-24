
@set solDir=%1
@set outputDir=%2


@echo hello
@echo %solDir%


@set lib32Path=%solDir%native.c.example.lib\build_result\Windows\x32\Release\dll\native_c_example_x32.dll
@set lib64Path=%solDir%native.c.example.lib\build_result\Windows\x64\Release\dll\native_c_example_x64.dll


del %outputDir%native_c_example_x32.dll 2>nul
del %outputDir%native_c_example_x64.dll 2>nul


@xcopy /s %lib32Path% %outputDir% /y /i
@xcopy /s %lib64Path% %outputDir% /y /i
