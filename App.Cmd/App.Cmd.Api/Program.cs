using App.Cmd.Api.Commands;
using App.Cmd.Domain.Aggregates;
using App.Cmd.Infrastructure.Config;
using App.Cmd.Infrastructure.Dispatchers;
using App.Cmd.Infrastructure.Handlers;
using App.Cmd.Infrastructure.Producers;
using App.Cmd.Infrastructure.Repositories;
using App.Cmd.Infrastructure.Stores;
using App.Common.Events;
using Confluent.Kafka;
using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Handlers;
using CQRS.Core.Infrastructure;
using CQRS.Core.Producers;
using MongoDB.Bson.Serialization;

var builder = WebApplication.CreateBuilder(args);

BsonClassMap.RegisterClassMap<BaseEvent>();
BsonClassMap.RegisterClassMap<MapCreatedEvent>();
BsonClassMap.RegisterClassMap<PointcloudAddedEvent>();

// Add services to the container.
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<MapAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();

// register command handler methods
var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
var dispatcher = new CommandDispatcher();

dispatcher.RegisterHandler<NewMapCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<AddPointcloudCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<DeleteMapCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<RestoreReadDbCommand>(commandHandler.HandleAsync);
builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);


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

app.UseAuthorization();

app.MapControllers();

app.Run();
