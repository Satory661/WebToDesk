using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(); // Добавляем CORS

var app = builder.Build();

// Разрешаем всё для тестов
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapPost("/open-app", () =>
{
    var exePath = @"C:\Users\Aimer\Desktop\prjct\WebToDesk\src\desktop\Desktop.App\MyApp\bin\Debug\net10.0-windows\MyApp.exe";
    if (!File.Exists(exePath)) return Results.NotFound("EXE не найден");

    Process.Start(new ProcessStartInfo { FileName = exePath, UseShellExecute = true });
    return Results.Ok("Запущено");
});

// Явно заставляем слушать этот порт на всех интерфейсах
app.Run("http://localhost:5298");