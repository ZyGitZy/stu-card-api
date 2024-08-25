namespace stu_card_api.Extentions
{
    public class MinioOptions
    {
        public string Endpoint { get; set; }

        public string AccessKey { get; set; }

        public string SecreKey { get; set; }

        public bool WithSSL { get; set; }

        public void Apply(MinioOptions source)
        {
            if (source == null) return;
            this.Endpoint = source.Endpoint;
            this.AccessKey = source.AccessKey;
            this.SecreKey = source.SecreKey;
            this.WithSSL = source.WithSSL;
        }
    }
}
