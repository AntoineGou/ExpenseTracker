#addin nuget:?package=Cake.Docker&version=1.1.2

var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

var solution = "ExpenseTracker.sln";
var cs = "Server=127.0.0.1;Database=model;User Id=SA;Password=azerty123!;Encrypt=False;TrustServerCertificate=True";
var sql = "./dbMigration";

Task("SetupDev")
    .Does(() => 
{
    DockerComposeUp(new DockerComposeUpSettings
    {
        DetachedMode = true
    });

    Information("Sleeping.");
    System.Threading.Thread.Sleep(10_000);
    Information("Awakening.");

    ApplyDatabaseMigration();
});

void ApplyDatabaseMigration()
{
    Information("Applying database migrations.");
    var exitCode = StartProcess("dotnet", new ProcessSettings
    { 
        Arguments = $"evolve migrate sqlserver -c \"{cs}\" -l \"{sql}\""
    });

    if (exitCode != 0)
    {
        Error("Database migrations failed.");
        throw new Exception($"Evolve migration failed with return code {exitCode}");
    }

    Information("Successfully applied database migrations.");
}

Task("CleanDev")
    .Does(() =>
{
    DockerComposeDown();
});

RunTarget(target);
