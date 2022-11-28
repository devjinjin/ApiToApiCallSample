using FrontApiServer.CronJob;
using FrontApiServer.Extensions;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

//DB ConnectionString 연결
builder.Services.ConfigureSQLContext(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//반복 작업 처리
builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();
    var jobKey = new JobKey("DemoJob");
    q.AddJob<DemoJob>(opts => opts.WithIdentity(jobKey));
    //"0/59 * * * * ?" 59초 마다
    //"0 0/1 * * * ?" 1분마다
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("DemoJob-trigger")
        .WithCronSchedule("0 0/1 * * * ?"));

});

builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
//반복 작업 처리

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
