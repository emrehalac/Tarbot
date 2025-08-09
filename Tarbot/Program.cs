using Business.Abstract;
using Business.Concrete;
using Business.Handlers;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Twilio.Clients;
using Twilio.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TarbotDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

// Add Twilio configuration
var accountSid = builder.Configuration["Twilio:AccountSid"];
var authToken = builder.Configuration["Twilio:AuthToken"];

builder.Services.AddSingleton<ITwilioRestClient>(sp =>
    new Twilio.Clients.TwilioRestClient(accountSid, authToken)
);

// Add custom services
builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EfUserDal>();

builder.Services.AddScoped<ICowService, CowManager>();
builder.Services.AddScoped<ICowDal, EfCowDal>();



// Add message handlers and resolver
builder.Services.AddScoped<MessageHandlerResolver>();
builder.Services.AddScoped<IMessageHandler, WelcomeHandler>();
builder.Services.AddScoped<IMessageHandler, KvkkApprovalHandler>();
builder.Services.AddScoped<IMessageHandler, CowCountHandler>();
builder.Services.AddScoped<IMessageHandler, CowLabelHandler>();
builder.Services.AddScoped<IMessageHandler, CowStatusHandler>();
builder.Services.AddScoped<IMessageHandler, CowGestationWeekHandler>();
builder.Services.AddScoped<IMessageHandler, CowLoopCompletedHandler>();
builder.Services.AddScoped<IMessageHandler, CompletedHandler>();







// Yeni handler'lar buraya eklenecek

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
