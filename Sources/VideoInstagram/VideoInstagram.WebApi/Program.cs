using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VideoInstagram.DataLayer.Context;
using VideoInstagram.DataLayer.Mappings;
using VideoInstagram.DataLayer.Repositories;
using VideoInstagram.WebApi.Mapping;
using VideoInstagram.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

var mapperConfiguration = new MapperConfiguration((IMapperConfigurationExpression configurationExpression) =>
{
    configurationExpression.AddProfile<EntityMapping>();
    configurationExpression.AddProfile<DtoMapping>();
});

builder.Services.AddSingleton(() => new Mapper(mapperConfiguration));

builder.Services.AddSingleton<IDataContextFactory, DataContextFactory>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IVideoMetaDataRepository, VideoMetadataRepository>();
builder.Services.AddSingleton<BusService>();

//builder.Services.AddDbContext<VideoInstagramDbContext>(options =>
//    options.UseSqlServer("Server=(localdb);Database=VideoInstagram;Trusted_Connection=True;MultipleActiveResultSets=true"));

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