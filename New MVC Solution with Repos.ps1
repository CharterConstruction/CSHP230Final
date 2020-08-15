<#
	• Create src folder
	• Create Solution within 
	• Create WebSite Project w/ MVC
	• Create Business Class Library Project
	• Create Repository Class Library Project
		○ Install-Package Microsoft.EntityFrameworkCore.Tools -Version 2.1.14
	• Create Database Class Library Project
		○ Install-Package Microsoft.EntityFrameworkCore.Tools -Version 2.1.14
		○ Install-Package Microsoft.EntityFrameworkCore.SqlServer -Version 2.1.14
	• Scaffold to target db
		○ Scaffold-DbContext 'Data Source=(local);Initial Catalog=DatabaseName;integrated security=True' Microsoft.EntityFrameworkCore.SqlServer
    •    Adds all to the solution file
    •    Adds references - web to business, business to repo, repo to db
    •    Installs EF and EF tools to the db project
    •    Installs EF tools to repo project
    •    Installs Automapper
    •    Scaffolds to the db, model classes placed into model folder
    •    DbContext class moved to root of namespace, namespace is updated and adds a using directive for the Models namespace
    •    Loops through and repository classes with it’s own interface, model and automapper extension methods for converting between db models and repository models
    •    Loops through and business manager classes with it’s own interface, model and automapper extension methods for converting between repo models and business models


Create DatabaseAccessor class in Database project
#>

#dotnet tool install --global dotnet-ef


$solutionName = "School"
$server = "localhost"
$database = "mini-cstructor"


$solutionDir = "$PSScriptRoot\WebSiteProject" #"$env:repo\Dev\ScriptMVCsolutionDev\$solutionName"




$srcDir  = "$solutionDir\src"

if(!(Test-Path $solutionDir)){ New-Item $solutionDir -ItemType Directory -Force }

if(!(Test-Path $srcDir)){ New-Item $srcDir -ItemType Directory -Force }

Set-Location $solutionDir
dotnet new sln



pause


$GetSlnFile = { param($dir) Get-ChildItem -Path $dir -Filter "*.sln" | Select -First 1 }
$slnFile  = icm $GetSlnFile -ArgumentList $solutionDir

Move-Item -Path $slnFile.FullName -Destination $srcDir

Set-Location $srcDir


$slnFile  = icm $GetSlnFile -ArgumentList $srcDir
$DatabaseNamespace = $solutionName + ".Database"
$RepositoryNamespace = $solutionName + ".Repository"
$BusinessNamespace = $solutionName + ".Business"
$MVCproject = $solutionName + ".Web"

# Create projects
dotnet new classlib -n $DatabaseNamespace -f netcoreapp2.1
dotnet new classlib -n $RepositoryNamespace -f netcoreapp2.1
dotnet new classlib -n $BusinessNamespace  -f netcoreapp2.1
dotnet new mvc -n $MVCproject -f netcoreapp2.1

# Tests if we want
#dotnet new mstest -o MyWebApp.DataStoreTest


$solutionFile = $slnFile.FullName
$DbProjectFile = $srcDir + "\$DatabaseNamespace\$DatabaseNamespace.csproj"
$RepoProjectFile = $srcDir + "\$RepositoryNamespace\$RepositoryNamespace.csproj"
$BusinessProjectFile = $srcDir + "\$BusinessNamespace\$BusinessNamespace.csproj"
$MVCprojectFile = $srcDir + "\$MVCproject\$MVCproject.csproj"

$projects = @( $DbProjectFile, $RepoProjectFile, $BusinessProjectFile, $MVCprojectFile )


$AllProjectsExist = @()
foreach($proj in $projects)
{
    $AllProjectsExist += Test-Path $proj
}



if(!$AllProjectsExist.Contains($false))
{
    #add to solution
    foreach($proj in $projects)
    {
        dotnet sln $solutionFile add $proj
    }

    # Add references
    dotnet add $MVCprojectFile reference $BusinessProjectFile
    dotnet add $BusinessProjectFile reference $RepoProjectFile
    dotnet add $RepoProjectFile reference $DbProjectFile
    
}

Set-Location $srcDir
dotnet add $MVCprojectFile package AutoMapper

Set-Location $("$srcDir\$DatabaseNamespace")

dotnet add $("$srcDir\$DatabaseNamespace") package Microsoft.EntityFrameworkCore.Tools -v 2.1.14
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 2.1.14
#dotnet build

#$scaffoldCommand = "Scaffold-DbContext 'Data Source=$server;Initial Catalog=$database;integrated security=True' Microsoft.EntityFrameworkCore.SqlServer"

dotnet ef dbcontext scaffold "Server=$server;Database=$database;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models

#dotnet ef dbcontext scaffold $scaffoldCommand Microsoft.EntityFrameworkCore.SqlServer -o Models


Set-Location $("$srcDir\$RepositoryNamespace")
dotnet add package Microsoft.EntityFrameworkCore.Tools -v 2.1.14
dotnet add package AutoMapper



$dbNameCleaned = $database.replace("-","");

$dbContextFileName = $dbNameCleaned + "Context.cs"
$DbContextFile = Get-ChildItem -Path $("$srcDir\$DatabaseNamespace\Models") -Filter $dbContextFileName


$DbContextDefinition = Get-Content $DbContextFile.FullName

cls


$DbModelNamespace = $DatabaseNamespace + ".Models"
$DbContextFixed = $DbContextDefinition.Replace(".Models","").Replace("public partial class","using Models;

    public partial class")

$DbContextFixed | Out-File $DbContextFile -Force




if(!$DbContextFile){ break; }

Move-Item $DbContextFile.FullName -Destination $("$srcDir\$DatabaseNamespace")

#Remove Class1
Remove-Item -Path $("$srcDir\$DatabaseNamespace\Class1.cs") 
Remove-Item -Path $("$srcDir\$RepositoryNamespace\Class1.cs") 
Remove-Item -Path $("$srcDir\$BusinessNamespace\Class1.cs") 


#Copy models directory to repository project
$DatabaseModelDir = "$srcDir\$DatabaseNamespace\Models"
$RepositoryDir = "$srcDir\$RepositoryNamespace"
$BusinessDir = "$srcDir\$BusinessNamespace"
#Copy-Item -Path $DatabaseModelDir -Destination $RepositoryDir -Recurse


#Create the repository DatabaseManager class
$dbContext = $database + "Context"
$DatabaseAccessorDefinition = "using $DatabaseNamespace;

namespace $RepositoryNamespace
{
    public class DatabaseAccessor
    {
        static DatabaseAccessor()
        {
            Instance = new $dbContext();
        }

        public static $dbContext Instance { get; private set; }
    }
}"
$DatabaseAccessorFile = $RepositoryDir + "\DatabaseAccessor.cs"
$DatabaseAccessorDefinition |Out-File $DatabaseAccessorFile

#Loop through each database model class 
foreach($model in $(Get-ChildItem -Path $DatabaseModelDir -Filter "*.cs"))
{
    
    $class = $model.name.Replace(".cs","")   
    
    if($class.Substring($class.Length-1) -eq "y")
    {
        $classPlural = $class.Replace("y","") + "ies"
    }
    else
    {
        $classPlural = $class + "s"
    }



    $definition = Get-Content $model.FullName
        
    $properties = $definition.Split("`n")|%{ 
        if($_ -like "*public*" -and $_ -notlike "*namespace*" -and $_ -notlike "*partial*")
        {
            $_
        } 
    }


$RepositoryInterface = "I" + $class + "Repository"
$RepositoryInterfaceDefinition = "public interface $RepositoryInterface
{
    List<$class> $classPlural { get; }
    $class $class(int id);
}"


$Props = $properties -join ("`n")
$ModelDefinition = "public class $class
{
$Props
}"

$RepositoryName = $class + "Repository"


$RepositoryClassDefinition = "public class $RepositoryName : $RepositoryInterface
{
    public List<$class> $classPlural
    {
        get
        {
            return DatabaseAccessor.Instance.$class.Select(t=>t.ToRepositoryModel()).ToList();                                               
        }
    }


    public $class $class(int id)
    {
        return null;
    } 
}"


$RepoModelNamespace = $RepositoryNamespace

$DbModelClass = $DbModelNamespace + "." + $class
$RepoModelClass = $RepoModelNamespace + "." + $class


$RepositoryMapperName = $class + "RepositoryMapper"
$RepositoryMapperDefinition = "public static class $RepositoryMapperName
{    
    public static AutoMapper.MapperConfiguration mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.CreateMap<$DbModelClass, $RepoModelClass>().ReverseMap());
    public static AutoMapper.IMapper mapper = mapperConfig.CreateMapper();


    public static $RepoModelClass ToRepositoryModel(this $DbModelClass businessObject)
    {
        return mapper.Map<$RepoModelClass>(businessObject);
    }


    public static $DbModelClass ToDbModel(this $RepoModelClass repositoryObject)
    {
        return mapper.Map<$DbModelClass>(repositoryObject);
    }

}"


$CombinedRepositoryDefinition = "

using System;
using System.Collections.Generic;
using System.Linq;

namespace $RepoModelNamespace
{
    $($RepositoryInterfaceDefinition.Replace("`n","`n`t"))

    $($ModelDefinition.Replace("`n","`n`t"))

    $($RepositoryClassDefinition.Replace("`n","`n`t"))

    $($RepositoryMapperDefinition.Replace("`n","`n`t"))

}"


cls

$repositoryFile = $RepositoryDir + "\$RepositoryName.cs"
$CombinedRepositoryDefinition|Out-File -FilePath $repositoryFile -Force



<######################################################################################################################################################
Generate Solution.Business.Manager class
######################################################################################################################################################>
$managerInterface = "I" + $class + "Manager"
$managerClass = $class + "Manager"

$RepositoryNameCamelCase = $RepositoryName.Substring(0,1).ToLower() + $RepositoryName.Substring(1,$($RepositoryName.Length-1))



$ManagerClassDefinition = "public class $managerClass : $managerInterface
{
    private readonly $RepositoryInterface $RepositoryNameCamelCase;

    public $managerClass($RepositoryInterface $RepositoryNameCamelCase)
    {
        this.$RepositoryNameCamelCase = $RepositoryNameCamelCase;
    }


    public List<$class> $classPlural
    {
        get
        {
            return $RepositoryNameCamelCase.$classPlural.Select(t=>t.ToBusinessModel()).ToList();                                               
        }
    }


    public $class $class(int id)
    {
        return null;
    } 
}"



$DbModelNamespace = $DatabaseNamespace + ".Models"
cls
$managerDefinition = "using System;
using System.Collections.Generic;
using System.Linq;

namespace $BusinessNamespace
{
    using $RepositoryNamespace;

    $($RepositoryInterfaceDefinition.Replace("`n","`n`t") -replace($RepositoryInterface,$managerInterface)) 

    $($ModelDefinition.Replace("`n","`n`t"))

    $($ManagerClassDefinition.Replace("`n","`n`t"))  

    $($RepositoryMapperDefinition.Replace("`n","`n`t") `
        -replace $RepositoryNamespace,$BusinessNamespace `
        -replace $DbModelNamespace,$RepositoryNamespace `
        -replace "ToRepositoryModel","ToBusinessModel" `
        -replace "ToDbModel","ToRepositoryModel" `
    )

   

}"


    
$managerFile = $BusinessDir + "\$managerClass.cs"
$managerDefinition|Out-File -FilePath $managerFile






<#
Generate Model and ViewModel classes
#>



$WebDir = "$srcDir\$MVCproject"
$WebControllerDir =  "$WebDir\Controllers"
$WebModelDir = "$WebDir\Models"
$WebViewDir = "$WebDir\Views"
$WebViewModelDir = "$WebDir\ViewModels"


@($WebDir,$WebControllerDir,$WebModelDir,$WebViewDir,$WebViewModelDir)|%{
    If(!$(Test-Path $_))
    {
        New-Item $_ -ItemType Directory
    }
}

$WebNamespace = $MVCproject
$WebModelNamespace = "$MVCproject.Models"
$WebControllerNamespace = "$MVCproject.Controllers"
$WebViewNamespace = "$MVCproject.Views"

$DbModelNamespace = $DatabaseNamespace + ".Models"


$ViewModelName = $class + "ViewModel"
$ViewModelInterface = "I" + $ViewModelName

$managerInterface
$ManagerNameCamelCase =  $managerClass.Substring(0,1).ToLower() + $managerClass.Substring(1,$($managerClass.Length-1))


# CREATE THE MODEL FILE
$webModelDefinition = "using System;
using System.Collections.Generic;
using System.Linq;

namespace $WebNamespace.Models
{    
    $($ModelDefinition.Replace("`n","`n`t"))

    $($RepositoryMapperDefinition.Replace("`n","`n`t") `
        -replace $($class + "RepositoryMapper"),$($class + "WebMapper") `
        -replace $($class + "BusinessMapper"),$($class + "WebMapper") `
        -replace $RepositoryNamespace,$WebNamespace`
        -replace $DbModelNamespace,$BusinessNamespace `
        -replace "ToRepositoryModel","ToWebModel" `
        -replace "ToDbModel","ToBusinessModel" 
    )
}"


$modelFile = $WebModelDir + "\$class.cs"
$webModelDefinition|Out-File -FilePath $modelFile



#CREATE THE VIEW MODEL FILE

$ViewModelDefinition = $ManagerClassDefinition -replace($managerClass,$ViewModelName) `
    -replace($managerInterface,$ViewModelInterface) `
    -replace($RepositoryInterface,$managerInterface) `
    -replace($RepositoryNameCamelCase,$ManagerNameCamelCase) `
    -replace("ToBusinessModel","ToWebModel")



$webViewModelDefinition = "using System;
using System.Collections.Generic;
using System.Linq;

namespace $WebNamespace.Models
{    
    using $BusinessNamespace;
    
    $($RepositoryInterfaceDefinition.Replace("`n","`n`t") -replace($RepositoryInterface,$ViewModelInterface)) 
    
    $ViewModelDefinition   

}"



$viewModelFile = "$WebDir\ViewModels\$ViewModelName.cs"
$webViewModelDefinition|Out-File -FilePath $viewModelFile

}







