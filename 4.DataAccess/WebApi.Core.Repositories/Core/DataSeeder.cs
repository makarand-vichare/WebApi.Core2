namespace WebApi.Core.Repositories.Core
{
    //public class DataSeeder : System.Data.Entity.DropCreateDatabaseIfModelChanges<DataContext>
    //{

    //    protected override void Seed(DataContext context)
    //    {
    //        InsertCLients(context);
    //        base.Seed(context);
    //    }

    //    private void InsertCLients(DataContext context)
    //    {
    //        context.Clients.Add(new Client()
    //        {
    //            Id = 0,
    //            ClientId = "webApp",
    //            Secret = AppMethods.GetHash("abc@123"),
    //            Name = "Web Application",
    //            ApplicationType = (int)ApplicationTypes.JavaScript,
    //            Active = true,
    //            RefreshTokenLifeTime = 7200,
    //            AllowedOrigin = "http://localhost:50992"
    //        });

    //        context.Clients.Add(new EntityModels.Identity.Client()
    //        {
    //            Id = 0,
    //            ClientId = "consoleApp",
    //            Secret = AppMethods.GetHash("123@abc"),
    //            Name = "Console Application",
    //            ApplicationType = (int)ApplicationTypes.NativeConfidential,
    //            Active = true,
    //            RefreshTokenLifeTime = 14400,
    //            AllowedOrigin = "*"
    //        });

    //        context.SaveChanges();
    //    }

    //}
}
