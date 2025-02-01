

internal class Program(){
    private static void Main(string[] args){
        var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorPages();
            builder.Services.AddCors(options => {
                options.AddPolicy("AllowAll",
                    policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            builder.Services.AddHttpClient();
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