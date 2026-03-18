//Swashbuckle.AspNetCore
//Microsoft.OpenApi
using ApiGateway.Policies;
using ApiGateway.Services;


var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Read service URLs from config
var serviceUrls = builder.Configuration.GetSection("ServiceUrls");

// Register HttpClient proxies with DI
builder.Services.AddHttpClient<OrderServiceProxy>(client =>
{
    client.BaseAddress = new Uri(serviceUrls["OrderService"]);
})
.AddPolicyHandler(ResiliencyPolicies.GetRetryPolicy())
.AddPolicyHandler(ResiliencyPolicies.GetCircuitBreakerPolicy())
.AddPolicyHandler(ResiliencyPolicies.GetTimeoutPolicy());


builder.Services.AddHttpClient<CartServiceProxy>(client =>
{
    client.BaseAddress = new Uri(serviceUrls["CartService"]);
});

//builder.Services.AddHttpClient<PaymentServiceProxy>(client =>
//{
//    client.BaseAddress = new Uri(serviceUrls["PaymentService"]);
//});
builder.Services.AddHttpClient<PaymentServiceProxy>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:PaymentService"]);
})
.AddPolicyHandler(ResiliencyPolicies.GetRetryPolicy())
.AddPolicyHandler(ResiliencyPolicies.GetCircuitBreakerPolicy())
.AddPolicyHandler(ResiliencyPolicies.GetTimeoutPolicy());


//builder.Services.AddHttpClient<InventoryServiceProxy>(client =>
//{
//    client.BaseAddress = new Uri(serviceUrls["InventoryService"]);
//});
builder.Services.AddHttpClient<InventoryServiceProxy>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:InventoryService"]);
})
.AddPolicyHandler(ResiliencyPolicies.GetRetryPolicy())
.AddPolicyHandler(ResiliencyPolicies.GetCircuitBreakerPolicy())
.AddPolicyHandler(ResiliencyPolicies.GetTimeoutPolicy());


builder.Services.AddHttpClient<ShippingServiceProxy>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ServiceUrls:ShippingService"]);
})
.AddPolicyHandler(ResiliencyPolicies.GetRetryPolicy())
.AddPolicyHandler(ResiliencyPolicies.GetCircuitBreakerPolicy())
.AddPolicyHandler(ResiliencyPolicies.GetTimeoutPolicy());

//builder.Services.AddHttpClient<ShippingServiceProxy>(client =>
//{
//    client.BaseAddress = new Uri(serviceUrls["ShippingService"]);
//});

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "ECommerce API Gateway",
        Version = "v1",
        Description = "API Gateway for ECommerce Microservices",
        Contact = new Microsoft.OpenApi.OpenApiContact
        {
            Name = "Your Team",
            Email = "bg@gmail.com"
        }
    });

    

   
});
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API Gateway v1");
        c.RoutePrefix = string.Empty; // Swagger UI at root (http://localhost:5000)
    });
}
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();


//using ApiGateway.Services;

//var builder = WebApplication.CreateBuilder(args);

//// Add controllers
//builder.Services.AddControllers();

//// Register HttpClient-based service proxies
//builder.Services.AddHttpClient<OrderServiceProxy>(client =>
//{
//    client.BaseAddress = new Uri("http://orderservice"); // Replace with actual service URL
//});

//builder.Services.AddHttpClient<CartServiceProxy>(client =>
//{
//    client.BaseAddress = new Uri("http://cartservice");
//});

//builder.Services.AddHttpClient<PaymentServiceProxy>(client =>
//{
//    client.BaseAddress = new Uri("http://paymentservice");
//});

//builder.Services.AddHttpClient<InventoryServiceProxy>(client =>
//{
//    client.BaseAddress = new Uri("http://inventoryservice");
//});

//builder.Services.AddHttpClient<ShippingServiceProxy>(client =>
//{
//    client.BaseAddress = new Uri("http://shippingservice");
//});

//// Swagger/OpenAPI
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Middleware
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseRouting();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();
