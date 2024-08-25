using Minio;

namespace stu_card_api.Extentions
{
    public static class MinioExtention
    {

        public static void AddMinio(this IServiceCollection services, IConfiguration configuration, string selectName = "MinioOptions")
        {
            if (!string.IsNullOrWhiteSpace(selectName))
            {
                configuration = configuration.GetSection(selectName);
            }

            var option = configuration.Get<MinioOptions>() ?? new MinioOptions();

            services.AddMinio(opt =>
            {
                opt.WithEndpoint(option.Endpoint)
                .WithSSL(option.WithSSL)
                .WithCredentials(option.AccessKey, option.SecreKey).Build();
            });
        }
    }
}
