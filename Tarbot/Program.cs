using DataAccess.Concrete.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Twilio.Clients;
using Twilio.Http;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TarbotDBContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<TarbotDBContext>(options =>
    options.UseNpgsql("Host=db.rtnrgvyfiltjxxfxwflu.supabase.co;Port:5432,Database=postgres;Username=postgres;Password=EmreHalac35;SSL Mode=Require;Trust Server Certificate=true"));



// Add services to the container.

// Add Twilio configuration
builder.Services.AddHttpClient<ITwilioRestClient, TwilioRestClient>(httpClient =>
{
    httpClient.DefaultRequestHeaders.Add("Tarbot-Twilio-Client", "tarbot-chatbot-1.0");
});

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
