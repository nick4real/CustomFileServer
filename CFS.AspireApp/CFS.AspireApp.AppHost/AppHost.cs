var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("CFS-MongoServer")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithMongoExpress(c =>
    {
        c.WithContainerName("MongoExpress");
        c.WithLifetime(ContainerLifetime.Persistent);
    });
var postgres = builder.AddPostgres("CFS-PostgresServer")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin(c =>
    {
        c.WithContainerName("PgAdmin");
        c.WithLifetime(ContainerLifetime.Persistent);
    });

var mongoDb = mongo.AddDatabase("CFS-MongoDB");
var postgresDb = postgres.AddDatabase("CFS-PostgresDB");

var api = builder.AddProject<Projects.CFS_API>("cfs-api")
    .WithReference(mongoDb)
    .WithReference(postgresDb)
    .WaitFor(mongoDb)
    .WaitFor(postgresDb);

var web = builder.AddProject<Projects.CFS_BlazorWebApp>("cfs-blazorwebapp")
    .WithReference(api);

builder.Build().Run();
