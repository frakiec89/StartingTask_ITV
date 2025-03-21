using StartingTask_ITV.DB_SQlite.Services;
using StartingTask_ITV.Core.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

//��� ��� ������ �� ��
builder.Services.AddSingleton<IDeviceService, DeviceService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}


app.UseSwagger(); // �������  UseSwagger  � �����  
app.UseSwaggerUI();

app.UseMiddleware<MyBasicAuthentication>();

app.UseAuthorization();

app.MapControllers();

app.Run();
