using StartingTask_ITV.DB_SQlite.Services;
using StartingTask_ITV.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var configuration = builder.Configuration;

string fileName = configuration["ConnectionStrings:FileName"]; // �������  ����������  ���  SQliete

//��� ��� ������ �� ��
builder.Services.AddSingleton<IDeviceService>(provider =>
{
    return new DeviceService($"fileName={fileName}");
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseSwagger(); // todo �������  UseSwagger  � �����  
app.UseSwaggerUI();


// ��� �����������  
app.UseMiddleware<MyBasicAuthentication>();

app.UseAuthorization();

app.MapControllers();

app.Run();
