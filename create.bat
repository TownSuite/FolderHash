
rm.exe -rf Temp
mkdir Temp
rm.exe -rf Build
mkdir Build


mkdir Temp\tools

copy "%CD%\FolderHash\bin\Release\FolderHash.exe" "%CD%\Temp\tools\FolderHash.exe" /Y
copy "%CD%\FolderHash.nuspec" "%CD%\Temp\FolderHash.nuspec" /Y
cd Temp
nuget pack "%CD%\FolderHash.nuspec" -OutputDirectory "%CD%\..\Build"
cd ..

