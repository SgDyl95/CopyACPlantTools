string copyFromFilePath = "C:\\AW Autodesk Content\\Plant3D 2022\\AutoCAD Plant 3D 2022 Content\\Template\\ACPlantTools";
var sourceDir = new DirectoryInfo(copyFromFilePath);
if (!sourceDir.Exists)
    throw new DirectoryNotFoundException("The source directory wasn't found, please ensure that your files are all synced!");
string userName = Environment.UserName;
string pasteToFilePathPreffix = "C:\\Users\\";
string pasteToFilePathSuffix = "\\AppData\\Local\\Autodesk\\AutoCAD Plant 3D\\CollaborationCache";
string p3DProjectFolders = pasteToFilePathPreffix + userName + pasteToFilePathSuffix;
string plantToolsFolder = "\\ACPlantTools";
string[] projectFolders = Directory.GetDirectories(p3DProjectFolders);
foreach (string projectFolder in projectFolders)
{
    string targetFolder = projectFolder + plantToolsFolder;
    if (!Directory.Exists(targetFolder)) { Directory.CreateDirectory(targetFolder); }
    Copy(copyFromFilePath, targetFolder);
}
Console.ReadLine();

static void Copy(string sourceDirectory, string targetDirectory)
{
    var diSource = new DirectoryInfo(sourceDirectory);
    var diTarget = new DirectoryInfo(targetDirectory);

    CopyAll(diSource, diTarget);
}

static void CopyAll(DirectoryInfo source, DirectoryInfo target)
{
    Directory.CreateDirectory(target.FullName);

    // Copy each file into the new directory.
    foreach (FileInfo fi in source.GetFiles())
    {
        Console.WriteLine(@"Copying {0}\{1}", target.FullName, fi.Name);
        fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
    }

    // Copy each subdirectory using recursion.
    foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
    {
        DirectoryInfo nextTargetSubDir =
            target.CreateSubdirectory(diSourceSubDir.Name);
        CopyAll(diSourceSubDir, nextTargetSubDir);
    }
}