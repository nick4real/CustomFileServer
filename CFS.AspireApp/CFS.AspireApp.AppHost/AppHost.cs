var builder = DistributedApplication.CreateBuilder(args);

var mongo = builder.AddMongoDB("CFS-MongoServer")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithMongoExpress();
var postgres = builder.AddPostgres("CFS-PostgresServer")
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin();

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
