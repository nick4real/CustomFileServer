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

#pragma warning disable ASPIREJAVASCRIPT001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.
var react = builder.AddViteApp("cfs-reactapp", "./../../cfs.reactwebapp", "dev")
    .PublishAsStaticWebsite(apiPath: "/file", apiTarget: api)
    .WithExternalHttpEndpoints();
#pragma warning restore ASPIREJAVASCRIPT001 // Type is for evaluation purposes only and is subject to change or removal in future updates. Suppress this diagnostic to proceed.

builder.Build().Run();
