## Getting started

Database. Use this guide or db.sql file in project

1. Change parameter 'Data Source' in 'Default Connection' section in appsetting.json in this section

```
"ConnectionStrings": {
    "DefaultConnection": "Data Source=DESKTOP-LVDBVRP;Initial Catalog=ABPTT;Trusted_Connection=True;TrustServerCertificate=True"
  }
```

2. Uncommit 38-40 lines in Program.cs

```csharp
 using var scope = app.Services.CreateScope();
 var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
 migrationService.MigrateUp();
```

3. Run app
4. Commit 38-40 lines in Program.cs again

## How to test app

After run app use https://yourdomain/api/test/data?countDevices=YourCountUsers endpoint to

```
[ApiController]
[Route("api/test")]
public class GenerateTestData : Controller
{
    ....
    [HttpGet]
    [Route("data")]
    public async Task<ActionResult> GenerateData(int countDevices)
    {
        ....
    }
}
```
Then you can go to  https://yourdomain/stats/chart endpoint to
Result:
![plot](images/Screenshot%202023-10-04%20114928.png)