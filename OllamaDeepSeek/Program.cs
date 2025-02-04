internal class Program(){
    private static void Main(string[] args){
        var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpClient();
            builder.Services.AddRazorPages();
            builder.Services.AddCors(options => { options.AddPolicy("AllowAll",policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); });
            builder.WebHost.UseUrls("http://localhost:5001", "http://localhost:5002", "http://localhost:5003");

        var app = builder.Build();
        if (!app.Environment.IsDevelopment()){
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseCors("AllowAll");
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.MapStaticAssets();
        app.MapControllerRoute(name: "default", pattern: "{controller=Pages}/{action=Index}/{id?}");
        app.MapRazorPages().WithStaticAssets();
        app.Run();
    }
}