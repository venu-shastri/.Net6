# Migrating to .NET 6

The basics of migrating an existing desktop application to .NET 6 is straightforward, of course depending on the complexity of the application

Migration comes in six steps

1. Run the portability analyzer.
   1. [The .NET Portability Analyzer - .NET | Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/standard/analyzers/portability-analyzer)
   2. This extension will scan your code base and generate a report that gives an overview of how portable your code is and what assemblies are compatible with which version of .NET. 
   3. All this analyzer does is checking if the framework APIs you use and and the references you have are compatible with the
      selected target framework. It does not compile, run, or test your application in any way.

2. Migrate to PackageReference.
   1. Moving away from the old packages.config file to PackageReference : In a .NET Framework application, all references to external packages are declared in the  packages.config file
   2. Migration to PackageReference is straightforward in Visual Studio; right-click references and select Migrate packages.
      config to PackageReference.
   3. ![image-20220607205015952](E:\.net 6\MigratePackageToPackageReference)
   4. Visual Studio will uninstall all NuGet packages from the project; remove the packages.config and reinstall the NuGet packages in PackageReference
   5. Build your application after this step to make sure all packages are restored from the NuGet package feed.
      
3. Update to .NET 4.8.
   1. update your application to the latest version of the classic .NET Framework, .NET 4.8 (Upgrading to .NET 4.8 first
      will bring you closer to “the new .NET” that started with .NET Core)
   2. ![image-20220607205440302](E:\.net 6\UpdateTo4.8)
   3. If you don’t see the .NET Framework 4.8 option, you need to download and install the .NET Framework 4.8 SDK from this link https://dotnet.microsoft.com/en-us/download/visual-studio-sdks . Make sure your application still compiles and runs
      after this step.
4. Switch the desktop project to .NET 6.
   1. Create a new .NET 6 application of the same type (WPF, WinForms,Class Library, etc.) as the project you’re trying to migrate.
   2. Copy the csproj of the new project to the location of the old project.
   3. Rename the new csproj to have the same name as the csproj of the old application.
   4. Open in Visual Studio, recreate the references, and add the NuGet packages again.
5. Switch any class libraries to .NET 6.
6. Run the application and test thoroughly.



### Upgrade Assistant

---

- Microsoft has created a command line tool called Upgrade Assistant
  - https://github.com/dotnet/upgrade-assistant
  - The upgrade assistant is a wizard that guides you through the process step by step, allowing you to skip whatever step you want or see more details.



