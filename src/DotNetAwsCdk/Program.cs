using Amazon.CDK;
using CdkWorkshop;

namespace DotNetAwsCdk
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();
            new DotNetAwsCdkStack(app, "DotNetAwsCdkStack");
            new WorkshopPipelineStack(app, "WorkshopPipelineStack");


            app.Synth();
        }
    }
}
