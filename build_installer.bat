cd "C:\Users\jrocha\Documents\Visual Studio 2015\Projects\HdbPoet"
"C:\Program Files (x86)\Inno Setup 5\iscc.exe" hdbpoet.iss

::del .\output\poet_src.zip
::zip .\output\poet_src.zip *.cs *.resx *.config *.ico *.png *.csproj
pause