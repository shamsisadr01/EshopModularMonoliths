var builder = WebApplication.CreateBuilder(args);

// Add Service To Conatiner
builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddBasketModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration);


var app = builder.Build();

// Configure the Http Requset pipeline
app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();

app.Run();
